namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FundTransferMaster")]
    public partial class FundTransferMaster
    {
        [Key]
        public int TransferNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TransferDate { get; set; }

        public int? GroupId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }
    }
}
