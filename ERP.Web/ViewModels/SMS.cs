namespace ERP.Web.ViewModels
{
    public class SMS
    {
        public int InvestorId { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
        public string MessageId { get; set; }
        public int SMSCount { get; set; }
        public int ErrorCode { get; set; }
    }
}