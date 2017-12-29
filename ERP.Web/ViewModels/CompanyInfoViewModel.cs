using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ERP.Web.ViewModels
{
    public class CompanyInfoViewModel
    {
        public int Id { get; set; }

        public int? CountryId { get; set; }

        public int? SectorId { get; set; }

        public string  SectorName { get; set; }
        public int? MarketTypeId { get; set; }
        public string MarketTypeName { get; set; }
        public int? CompanyDepositoryId { get; set; }
        public string CompanyDepositoryName { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string WebAddress { get; set; }

        public string TradeIdDSE { get; set; }

        public string TradeIdCSE { get; set; }

        public int Status { get; set; }

        public decimal? EPS { get; set; }

        public decimal? NAV { get; set; }
        public string ISINNo { get; set; }

        public decimal? AuthorizeCapital { get; set; }

        public decimal? PaidUpCapital { get; set; }

        public bool IsForeign { get; set; }

        public bool IsMarginLoan { get; set; }

        public decimal? MarketPrice { get; set; }

        public DateTime? MarketPriceDate { get; set; }

        public decimal? FaceValue { get; set; }

        public int? MarketLot { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        public string  CompanyDepositoryShortName { get; set; }
        public string DepositoryCompanyName { get; set; }
        public string CompanyShortName { get; set; }
        public string CountryName { get; set; }
        public string InstrumentTypeName { get; set; }
        public int? InstrumentTypeId { get; set; }
        public string IsForeignMsg { get; set; }
        public string IsMarginLoanMsg { get; set; }
        public string MarketPriceDateMsg { get; set; }
        public decimal? Premium { get; set; }
        public string ScripCode { get; set; }
        public string StatusMsg { get; set; }
        public long RowSl { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> MarketSectorList { get; set; }
        public IEnumerable<SelectListItem> MarketInstrumentTypeList { get; set; }
        public IEnumerable<SelectListItem> MarketGroupList { get; set; }
        public IEnumerable<SelectListItem> CompanyDepositoryList { get; set; }
    }
}