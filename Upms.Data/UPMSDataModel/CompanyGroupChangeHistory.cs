namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyGroupChangeHistory")]
    public partial class CompanyGroupChangeHistory
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int? PresentGroupId { get; set; }

        public int? PreviousGroupId { get; set; }

        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public int CreatedUserId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        //public virtual CompanyInformation CompanyInformation { get; set; }

        //public virtual MarketGroup MarketGroup { get; set; }

        //public virtual MarketGroup MarketGroup1 { get; set; }
    }
}
