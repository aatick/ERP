namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyInformation")]
    public partial class CompanyInformation
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public CompanyInformation()
        //{
        //    CompanyEpsNavChangeHistories = new HashSet<CompanyEpsNavChangeHistory>();
        //    CompanyGroupChangeHistories = new HashSet<CompanyGroupChangeHistory>();
        //    CompanyListedToDepositoryCompanies = new HashSet<CompanyListedToDepositoryCompany>();
        //    CompanySharePriceHistories = new HashSet<CompanySharePriceHistory>();
        //}

        public int Id { get; set; }

        public int? CountryId { get; set; }

        public int? SectorId { get; set; }

        public int? GroupId { get; set; }

        public int? InstrumentTypeId { get; set; }

        public int? CompanyDepositoryId { get; set; }

        [Required]
        [StringLength(250)]
        public string CompanyName { get; set; }

        [StringLength(20)]
        public string CompanyShortName { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(25)]
        public string WebAddress { get; set; }

        [StringLength(20)]
        public string TradeIdDSE { get; set; }

        [StringLength(20)]
        public string TradeIdCSE { get; set; }

        public int Status { get; set; }

        public decimal EPS { get; set; }

        public decimal NAV { get; set; }

        [StringLength(50)]
        public string ISINNo { get; set; }

        public decimal? Premium { get; set; }

        public decimal? AuthorizeCapital { get; set; }

        public decimal? PaidUpCapital { get; set; }

        public bool IsForeign { get; set; }

        public decimal FaceValue { get; set; }

        public int MarketLot { get; set; }

        public decimal? MarketPrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MarketPriceDate { get; set; }

        [StringLength(10)]
        public string ScripCode { get; set; }

        public bool IsMarginLoan { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

       

        //public virtual CompanyDepository CompanyDepository { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CompanyEpsNavChangeHistory> CompanyEpsNavChangeHistories { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CompanyGroupChangeHistory> CompanyGroupChangeHistories { get; set; }

       // public virtual LookupCountry LookupCountry { get; set; }

        //public virtual MarketGroup MarketGroup { get; set; }

       // public virtual MarketInstrumentType MarketInstrumentType { get; set; }

       // public virtual MarketSector MarketSector { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CompanyListedToDepositoryCompany> CompanyListedToDepositoryCompanies { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CompanySharePriceHistory> CompanySharePriceHistories { get; set; }
    }
}
