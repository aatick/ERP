﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ERP.Web.ViewModels
{
    public class AccVoucherEntryViewModel
    {
        public long TrxMasterID { get; set; }
        public int OfficeID { get; set; }

        [Display(Name = "Transaction Date")]
        public DateTime TrxDate { get; set; }
       // public string TrxDtMsg { get; set; }

        [Display(Name = "Voucher No")]
        public long? VoucherNo { get; set; }

        [Display(Name = "Description")]
        public string VoucherDesc { get; set; }

        [Display(Name = "Voucher Type")]
        public string VoucherType { get; set; }

        [Display(Name = "Transactoin Type")]
        public string TransactionType { get; set; }
        public string Reference { get; set; }
        public bool? IsPosted { get; set; }
        public bool? IsActive { get; set; }

        public long TrxDetailsID { get; set; }

        [Display(Name = "Account")]
        public int? AccID { get; set; }

        public string AccFullName { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Debit { get; set; }
        public string Narration { get; set; }
        public string AccMode { get; set; }
        public int? OfficeLevel { get; set; }
        public bool? IsAutoVoucher { get; set; }

        [Display(Name = "Rectify")]
        public bool IsRectify { get; set; }
        public string AutoVoucher { get; set; }
        [Display(Name = "Bank Account")]
        public int? BAccID { get; set; }
        public IEnumerable<SelectListItem> VoucherTypeList { get; set; }
        public IEnumerable<SelectListItem> TransactionTypeList { get; set; }
        public IEnumerable<SelectListItem> VoucherNoList { get; set; }
    }
}