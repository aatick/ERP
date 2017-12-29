namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MarketInformation")]
    public partial class MarketInformation
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public MarketInformation()
        //{
        //    BrokerTraders = new HashSet<BrokerTrader>();
        //    CompanySharePriceHistories = new HashSet<CompanySharePriceHistory>();
        //}

        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string MarketShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string ExchangeName { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? MarketCode { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

      

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<BrokerTrader> BrokerTraders { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CompanySharePriceHistory> CompanySharePriceHistories { get; set; }
    }
}
