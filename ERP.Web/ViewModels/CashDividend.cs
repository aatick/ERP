namespace ERP.Web.ViewModels
{
    public class CashDividend
    {
        public int InvestorId { get; set; }
        public int CompanyId { get; set; }
        public string RecordDate { get; set; }
        public decimal Rate { get; set; }
        public decimal NetAmount { get; set; }
    }
}