using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ERP.Web.ViewModels
{
    public class BrokerTraderViewModel
    {
        public int Id { get; set; }
        public int BrokerBranchId { get; set; }
        public int MarketId { get; set; }
        public int EmployeeId { get; set; }
        public string TraderCode { get; set; }
        public string PCNo { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }


        public long? RowSl { get; set; }
        public string BrokerBranchName { get; set; }
        public string ExchangeName { get; set; }
        public string EmployeeName { get; set; }
        public string ValidFromMsg { get; set; }
        public string ValidToMsg { get; set; }
        public IEnumerable<SelectListItem> BrokerBranchList { get; set; }
        public IEnumerable<SelectListItem> MarketList { get; set; }
        public IEnumerable<SelectListItem> EmployeeList { get; set; }
    }
}