namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BrokerbranchDPBranchMapping")]
    public partial class BrokerbranchDPBranchMapping
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int DPBranchId { get; set; }

        public int BrokerBranchId { get; set; }
    }
}
