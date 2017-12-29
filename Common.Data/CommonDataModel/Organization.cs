namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Organization")]
    public partial class Organization
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string OrganizationName { get; set; }

        [Required]
        [StringLength(20)]
        public string OrganizationShortName { get; set; }

        [Required]
        [StringLength(500)]
        public string OrganizationAddress { get; set; }

        [StringLength(500)]
        public string OrganizationLogoName { get; set; }

        [StringLength(100)]
        public string OrganizationDealerCode { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        [StringLength(10)]
        public string SMSPassword { get; set; }
        [Required]
        [StringLength(20)]
        public string SMSMobileNo { get; set; }
        [Required]
        [StringLength(100)]
        public string SMSUserName { get; set; }
        public string SoftwareLogo { get; set; }
        public bool HasPaddingInInvestorCode { get; set; }
        public int LengthOfInvestorCode { get; set; }
        public bool IsDSEPrimary { get; set; }
        public string BuySaleOrderLogo { get; set; }

    }
}
