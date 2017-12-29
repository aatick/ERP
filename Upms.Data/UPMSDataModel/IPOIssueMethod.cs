using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSDataModel
{
    [Table("IPOIssueMethod")]
    public class IPOIssueMethod
    {
        public int Id { get; set; }
        public string IssueMethodName { get; set; }
    }
}
