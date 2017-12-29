using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ERP.Web.ViewModels
{
    public class BrokerInfoViewModel
    {
        public int Id { get; set; }

        public int DepositoryParticipantId { get; set; }
        public string  DepositoryParticipantName { get; set; }
        public int MarketId { get; set; }
        public string MarketName { get; set; }
        public int BrokerTypeId { get; set; }
        public string BrokerTypeName { get; set; }
        public string BrokerShortName { get; set; }
        public string BrokerName { get; set; }
        public string RegistrationNo { get; set; }
        public int TotalTrader { get; set; }
        public bool IsOwner { get; set; }
        public string BrokerCode { get; set; }
        public string IsOwnerMsg { get; set; }
        public decimal FreeLimit { get; set; }
        public decimal AuthCapital { get; set; }
        public decimal PaidUpCapital { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssueDateMsg { get; set; }
        public string DepositMode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }
        public IEnumerable<SelectListItem> BrokerTypeList { get; set; }
        public IEnumerable<SelectListItem> MarketList { get; set; }
        public IEnumerable<SelectListItem> BrokerDepositoryList { get; set; }
    }
}