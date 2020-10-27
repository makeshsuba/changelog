using System;
using System.Collections.Generic;
using System.Text;

namespace Changelog.Abstraction.DTOs
{
    public class LogsContextDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LogTitle { get; set; }
        public int LogTypeId { get; set; }
        public string LogDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class LogsListContextDTO
    {
        public string UserName { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }

    public class LogTypeDTO
    {
        public int LogTypeId { get; set; }
        public bool IsVisible { get; set; }
        public string LogType { get; set; }
    }

}
