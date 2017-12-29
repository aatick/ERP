using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Web.ViewModels
{
    public class AccAccountDetailsViewModel
    {
        public long TrxMasterID { get; set; }

        public int OfficeID { get; set; }
        public int OfficeLevel { get; set; }
       
        public System.DateTime TrxDate { get; set; }
        public DateTime LastWorkingDate { get; set; }
        public string TrxDtMsg { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherType { get; set; }
        public string VoucherDesc { get; set; }
        public string Reference { get; set; }
        public Nullable<decimal> TotDebit { get; set; }
        public Nullable<decimal> TotCredit { get; set; }
        public Nullable<bool> IsAutoVoucher { get; set; }
       
       
       
        [Display(Name = "Rectify")]
        public bool? IsRectify { get; set; }
        public bool? IsPosted { get; set; }
        public bool? IsYearlyClosing { get; set; }
        
    }
}