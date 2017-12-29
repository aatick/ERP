namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyEpsNavChangeHistory")]
    public partial class CompanyEpsNavChangeHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int CompanyId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DeclarationDate { get; set; }

        public decimal? EPS { get; set; }

        public decimal? NAV { get; set; }

        public decimal AuthorizeCapital { get; set; }

        public decimal PaidUpCapital { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        public virtual CompanyInformation CompanyInformation { get; set; }
    }
}
