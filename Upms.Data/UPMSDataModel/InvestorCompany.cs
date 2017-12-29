namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorCompany")]
    public partial class InvestorCompany
    {
       // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int InvestorId { get; set; }

        [StringLength(200)]
        public string ContactPerson { get; set; }

        [StringLength(20)]
        public string RegNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RegisteredDate { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photograph { get; set; }

        [Column(TypeName = "image")]
        public byte[] Signature { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }


        public int? BrokerBranchId { get; set; }
        public int? InvestorBranchId { get; set; } 

        //public virtual InvestorDetail InvestorDetail { get; set; }
    }
}
