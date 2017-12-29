namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmailSMSInvestorAccess")]
    public partial class EmailSMSInvestorAccess
    {
        public int Id { get; set; }

        public int InvestorId { get; set; }

        public int EmailSMSTypeId { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatedUserId { get; set; }

        public bool IsActive { get; set; }

        public int? BrokerBranchId { get; set; }

        public int? InvestorBranchId { get; set; }
    }
}
