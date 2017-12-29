namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmailSMSDPAccess")]
    public partial class EmailSMSDPAccess
    {
        public int Id { get; set; }

        public int DepositoryParticipantId { get; set; }

        public int EmailSMSTypeId { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatedUserId { get; set; }

        public bool IsActive { get; set; }

        public int? BrokerBranchId { get; set; }
    }
}
