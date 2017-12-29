using System;

namespace ERP.Web.ViewModels
{
    public class CompanyDepositoryViewModel
    {
        public int Id { get; set; }
        public string CompanyDepositoryShortName { get; set; }
        public string DepositoryCompanyName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreatedUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}