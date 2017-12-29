using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSDataModel
{
    [Table("TradeUploadFileInformation")]
    public partial class TradeUploadFileInformation
    {
        public int Id { get; set; }
        public int AspNetSecurityModuleId { get; set; }
        [StringLength(150)]
        public string FileName { get; set; }
        public string FileDescription { get; set; }
        [StringLength(1)]
        public string Delimeter { get; set; }
        [StringLength(50)]
        public string Prefix { get; set; }
        [StringLength(5)]
        public string Extension { get; set; }
        [StringLength(100)]
        public string ContainingText { get; set; }
        public int SerialNo { get; set; }
        public int? DependentOnFileId { get; set; }
        [StringLength(100)]
        public string MappingTableName { get; set; }
        public string CDBLFileName { get; set; }
        public int CDBLMaxColumnLength { get; set; }
        public bool HasProcess { get; set; }
        public string UploadProcedureName { get; set; }
        public string ProcessProcedureName { get; set; }
        public string GetProcedureName { get; set; }
        public string RateSetupTableName { get; set; }
        public bool IsShareIn { get; set; }
        public string FileType { get; set; }
        public bool IsActive { get; set; }
    }
}
