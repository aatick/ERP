using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonDataModel
{
    [Table("ReportInformation")]
    public class ReportInformation
    {
        public int Id { get; set; }
        public string ReportName { get; set; }
        public int AspNetSecurityModuleId { get; set; }
        public int SerialNo { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ProjectShortName { get; set; }
    }
}
