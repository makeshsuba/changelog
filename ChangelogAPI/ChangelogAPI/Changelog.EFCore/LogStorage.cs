using AutoMapper;
using Changelog.Abstraction.Abstractions;
using Changelog.Abstraction.DTOs;
using Changelog.EFCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Changelog.EFCore
{
    public class LogStorage : ILogsStorage
    {
        private readonly ChangelogContext _context;
        private readonly IMapper _mapper;

        public LogStorage(ChangelogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<LogsContextDTO> AddLog(LogsContextDTO inputLog)
        {
            var log = _mapper.Map<LogsModel>(inputLog);
            log.CreatedDate = DateTime.Now.Date;
            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return _mapper.Map<LogsContextDTO>(log);
        }

        public async Task<bool> DeleteLog(int logId)
        {
            if (_context.Logs.Any(a => a.Id == logId))
            {
                _context.Logs.Remove(await _context.Logs.FirstOrDefaultAsync(a => a.Id == logId));
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            return false;
        }

        public async Task<LogsContextDTO> GetLog(int logId)
        {
            if (_context.Logs.Any(a => a.Id == logId))
            {
                var result = await _context.Logs.FirstOrDefaultAsync(a => a.Id == logId);
                return _mapper.Map<LogsContextDTO>(result);
            }
            return null;
        }

        public async Task<ResultLogsDTO> GetLogs(LogsListContextDTO input)
        {
            var logsList = await _context.Logs.Select(a => a).Where(a => a.UserName == input.UserName)
                .Join(_context.LogTypes, logs => logs.LogTypeId, lt => lt.LogTypeId,
                (logs, lt) => new LogListDTO
                {
                    Id = logs.Id,
                    LogTitle = logs.LogTitle,
                    LogDescription = logs.LogDescription,
                    LogType = lt.LogType,
                    CreatedDate = logs.CreatedDate,
                    UpdatedDate = logs.UpdatedDate,
                    UserName = logs.UserName
                })
                .OrderByDescending(a => a.Id)
                .ToListAsync().ConfigureAwait(false);
            if (logsList != null)
            {
                var logs = new ResultLogsDTO
                {
                    TotalLogsCount = logsList.Count,
                    LogsData = logsList.Skip((input.PageNumber - 1) * input.PageSize)
                    .Take(input.PageSize)
                };

                return logs;
            }
            return null;

        }

        public async Task<IEnumerable<LogTypeDTO>> GetLogType()
        {
            var result = _context.LogTypes.Select(a => a);
            return _mapper.Map<IEnumerable<LogTypeDTO>>(result);
        }

        public async Task<LogsContextDTO> UpdateLog(LogsContextDTO inputLog)
        {
            var logEntity = _mapper.Map<LogsModel>(inputLog);
            if (_context.Logs.Any(a => a.Id == inputLog.Id))
            {
                var existLog = await _context.Logs.FirstOrDefaultAsync(a => a.Id == logEntity.Id).ConfigureAwait(false);
                logEntity.UpdatedDate = DateTime.Now.Date;
                logEntity.CreatedDate = existLog.CreatedDate;
                _context.Entry(existLog).CurrentValues.SetValues(logEntity);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return _mapper.Map<LogsContextDTO>(logEntity);
            }
            return null;
        }
    }
}
