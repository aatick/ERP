namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompanyDepository")]
    public partial class CompanyDepository
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public CompanyDepository()
        //{
        //    CompanyInformations = new HashSet<CompanyInformation>();
        //    CompanyListedToDepositoryCompanies = new HashSet<CompanyListedToDepositoryCompany>();
        //}

        public int Id { get; set; }

        [StringLength(20)]
        public string CompanyDepositoryShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string DepositoryCompanyName { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(200)]
        public string WebAddress { get; set; }

        [StringLength(100)]
        public string ContactPerson { get; set; }

        [StringLength(100)]
        public string ContactPhone { get; set; }

        [StringLength(100)]
        public string ContactEmail { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }

      

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CompanyInformation> CompanyInformations { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CompanyListedToDepositoryCompany> CompanyListedToDepositoryCompanies { get; set; }
    }
}
