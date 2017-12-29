namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BrokerTrader")]
    public partial class BrokerTrader
    {
        public int Id { get; set; }

        public int MarketId { get; set; }

        public int? EmployeeId { get; set; }

        [StringLength(15)]
        public string TraderCode { get; set; }

        [StringLength(15)]
        public string PCNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ValidFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ValidTo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        public int CreatedUserId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        public int? BrokerBranchId { get; set; }
        //public virtual BrokerBranch BrokerBranch { get; set; }

        //public virtual BrokerEmployee BrokerEmployee { get; set; }

        //public virtual MarketInformation MarketInformation { get; set; }
    }
}
