using System;

namespace ERP.Web.ViewModels
{
    public class lookupBankBranchViewModel
    {
        public int Id { get; set; }
        public long RowSl { get; set; }
        public int? ThanaId { get; set; }
        public int? DivitionId { get; set; }
        public int? DistrictId { get; set; }
        public string ThanaName { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string RoutingNo { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreatedUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}