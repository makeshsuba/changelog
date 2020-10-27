using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Changelog.EFCore.Model
{
    public class LogTypesModel
    {
        [Key]
        public int LogTypeId { get; set; }
        public string LogType { get; set; }
        public bool IsVisible { get; set; }
    }
}
