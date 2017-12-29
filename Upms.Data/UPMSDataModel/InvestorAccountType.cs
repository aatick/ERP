namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvestorAccountType")]
    public partial class InvestorAccountType
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public InvestorAccountType()
        //{
        //    InvestorAccounts = new HashSet<InvestorAccount>();
        //    InvestorSubAccountTypes = new HashSet<InvestorSubAccountType>();
        //}

        public int Id { get; set; }

        [StringLength(10)]
        public string AccountTypeShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string AccountTypeName { get; set; }

        

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorAccount> InvestorAccounts { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<InvestorSubAccountType> InvestorSubAccountTypes { get; set; }
    }
}
