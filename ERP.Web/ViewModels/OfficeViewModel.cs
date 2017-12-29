using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ERP.Web.ViewModels
{
    public class OfficeViewModel : BaseModel
    {
        public int OfficeId { get; set; }
        public int? CompanyId { get; set; }

        [Display(Name = "Code(কোড)")]
        public string OfficeCode { get; set; }
        [Display(Name = "Name(নাম)")]
        public string OfficeName { get; set; }
        public byte OfficeLevel { get; set; }
        public string FirstLevel { get; set; }
        public string SecondLevel { get; set; }
        public string ThirdLevel { get; set; }
        public string FourthLevel { get; set; }
        [Display(Name = "Operation Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime OperationStartDate { get; set; }
        [Display(Name = "Address(ঠিকানা)")]
        public string OfficeAddress { get; set; }
        [Display(Name = "Post Code(পোষ্ট অফিসের কোড)")]
        public string PostCode { get; set; }
        [Display(Name = "Email(ই-মেইল)")]
        //public Nullable<int> GeoLocationID { get; set; }
        public string Email { get; set; }
       [Display(Name = "Phone(ফোন)")]
        public string Phone { get; set; }
        [Display(Name = "Parent Code(প্রধান কোড)")]
        public string ParentId { get; set; }
        public IEnumerable<SelectListItem> GeoLocationList { get; set; }
        
    }
}