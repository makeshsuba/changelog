using Changelog.Abstraction.Abstractions;
using Changelog.Abstraction.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Changelog.Business
{
    public class LogsService : ILogsService
    {
        private readonly ILogsStorage _logStorage;

        public LogsService(ILogsStorage logStorage)
        {
            _logStorage = logStorage;
        }
        public Task<LogsContextDTO> AddLog(LogsContextDTO inputLog)
        {
            return _logStorage.AddLog(inputLog);
        }

        public Task<bool> DeleteLog(int logId)
        {
            return _logStorage.DeleteLog(logId);
        }

        public Task<LogsContextDTO> GetLog(int logId)
        {
            return _logStorage.GetLog(logId);
        }

        public Task<ResultLogsDTO> GetLogs(LogsListContextDTO input)
        {
            return _logStorage.GetLogs(input);
        }

        public Task<IEnumerable<LogTypeDTO>> GetLogType()
        {
            return _logStorage.GetLogType();
        }

        public Task<LogsContextDTO> UpdateLog(LogsContextDTO inputLog)
        {
            return _logStorage.UpdateLog(inputLog); 
        }
    }
}
