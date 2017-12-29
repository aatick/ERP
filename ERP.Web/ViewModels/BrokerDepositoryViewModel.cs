using System;

namespace ERP.Web.ViewModels
{
    public class BrokerDepositoryViewModel
    {
        public int Id { get; set; }
        public string DPCode { get; set; }
        public string BORefNo { get; set; }
        public string DepositoryParticipantName { get; set; }
        public string DPShortName { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public bool PayInOut { get; set; }
        public string ClearingAccDSE { get; set; }
        public string ClearingAccCSE { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreatedUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}