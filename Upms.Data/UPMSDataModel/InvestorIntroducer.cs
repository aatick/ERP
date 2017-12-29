namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorIntroducer")]
    public partial class InvestorIntroducer
    {
        public int Id { get; set; }

        public int InvestorId { get; set; }

        public int? IntroducerInvestorId { get; set; }

        public int? EmployeeId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }


        public int? BrokerBranchId { get; set; }
        public int? InvestorBranchId { get; set; } 

        //public virtual BrokerEmployee BrokerEmployee { get; set; }

        //public virtual InvestorDetail InvestorDetail { get; set; }

        //public virtual InvestorDetail InvestorDetail1 { get; set; }
    }
}
