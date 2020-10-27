using System;
using System.Collections.Generic;
using System.Text;

namespace Changelog.Abstraction.DTOs
{

    public class LogListDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LogTitle { get; set; }
        public string LogType { get; set; }
        public string LogDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
    public class ResultLogsDTO
    {
        public IEnumerable<LogListDTO> LogsData { get; set; }
        public int TotalLogsCount { get; set; }
    }
}
