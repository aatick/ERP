namespace ERP.Web.ViewModels
{
    public class InvestorCharge
    {
        public int InvestorId { get; set; }
        public int ChargeId { get; set; }
        public string InvestorCode { get; set; }
        public string InvestorName { get; set; }
        public string ChargeName { get; set; }
        public decimal ChargeAmount { get; set; }
        public decimal MinimumCharge { get; set; }
        public bool IsPercentage { get; set; }
        public bool IsSlab { get; set; }
        public int TotalRecord { get; set; }
    }
}