namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyListedToDepositoryCompany")]
    public partial class CompanyListedToDepositoryCompany
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int CompanyDepositoryId { get; set; }

        [Column(TypeName = "date")]
        public DateTime SpotStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime SpotEndDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime SuspendStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime SuspendEndDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime DematStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime TradingStartDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

        //public virtual CompanyDepository CompanyDepository { get; set; }

        //public virtual CompanyInformation CompanyInformation { get; set; }
    }
}
