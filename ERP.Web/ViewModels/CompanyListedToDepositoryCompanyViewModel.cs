using System;

namespace ERP.Web.ViewModels
{
    public class CompanyListedToDepositoryCompanyViewModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int CompanyDepositoryId { get; set; }
        public string SpotStartDate { get; set; }
        public string SpotEndDate { get; set; }
        public string SuspendStartDate { get; set; }
        public string SuspendEndDate { get; set; }
        public string DematStartDate { get; set; }
        public string TradingStartDate { get; set; }
        public string CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreatedUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }

        public string  CompanyName { get; set; }
        public string CompanyDepositoryName { get; set; }
    }
}