namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorSubAccountType")]
    public partial class InvestorSubAccountType
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public InvestorSubAccountType()
        //{
        //    InvestorAccounts = new HashSet<InvestorAccount>();
        //}

        public int Id { get; set; }

        public int AccountTypeId { get; set; }

        [StringLength(10)]
        public string SubAccountShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string SubAccountName { get; set; }

       

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorAccount> InvestorAccounts { get; set; }

        //public virtual InvestorAccountType InvestorAccountType { get; set; }
    }
}
