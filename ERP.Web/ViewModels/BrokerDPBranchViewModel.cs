using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ERP.Web.ViewModels
{
    public class BrokerDPBranchViewModel
    {
        public long RowSL { get; set; }
        public int Id { get; set; }
        public int DepositoryParticipantId { get; set; }
        public int? ThanaId { get; set; }
        public string DPBranchName { get; set; }
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
        public string DepositoryParticipantName { get; set; }
        public string ThanaName { get; set; }
        public IEnumerable<SelectListItem> BrokeDepositoryList { get; set; }
    }
}