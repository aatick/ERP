namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorType")]
    public partial class InvestorType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InvestorType()
        {
            InvestorAccounts = new HashSet<InvestorAccount>();
        }

        public int Id { get; set; }

        [StringLength(10)]
        public string InvestorTypeShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string InvestorTypeName { get; set; }

        public int InvestorTypeOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvestorAccount> InvestorAccounts { get; set; }
    }
}
