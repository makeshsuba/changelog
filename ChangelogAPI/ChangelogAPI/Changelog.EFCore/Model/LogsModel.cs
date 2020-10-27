using System;
using System.Collections.Generic;
using System.Text;

namespace Changelog.EFCore.Model
{
    public class LogsModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LogTitle { get; set; }
        public int LogTypeId { get; set; }
        public string LogDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
