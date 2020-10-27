using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChangeLogMVC.Models
{
    public class LogListModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LogTitle { get; set; }
        public string LogType { get; set; }
        public string LogDescription { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime UpdatedDate { get; set; }
    }

    public class ViewModel
    {
        public Log log { get; set; }
        public LogTypes logType { get; set; }
    }
}