using Changelog.Abstraction.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Changelog.Abstraction.Abstractions
{
    public interface ILogsService
    {
        Task<ResultLogsDTO> GetLogs(LogsListContextDTO input);
        Task<LogsContextDTO> GetLog(int logId);
        Task<LogsContextDTO> AddLog(LogsContextDTO inputLog);
        Task<LogsContextDTO> UpdateLog(LogsContextDTO inputLog);
        Task<bool> DeleteLog(int logId);

        Task<IEnumerable<LogTypeDTO>> GetLogType();
    }
}
