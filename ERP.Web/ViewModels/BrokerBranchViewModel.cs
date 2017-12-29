using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ERP.Web.ViewModels
{
    public class BrokerBranchViewModel
    {
        public int Id { get; set; }
        public long RowSL { get; set; }
        public int BrokerId { get; set; }
        public string  BrokerName { get; set; }
        public int? ThanaId { get; set; }
        public string  ThanaName { get; set; }
        public bool IsOwner { get; set; }
        public string BrokerBranchName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string ContactName { get; set; }
        public string ContactMobile { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFax { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreatedUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }

        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }

        public IEnumerable<SelectListItem> BrokeList { get; set; }
    }
}