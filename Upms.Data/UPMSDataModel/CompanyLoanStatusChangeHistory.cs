namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyLoanStatusChangeHistory")]
    public partial class CompanyLoanStatusChangeHistory
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public bool IsMargin { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public int CreatedUserId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
