using System.ComponentModel.DataAnnotations;

namespace ERP.Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }

        [Display(Name="Name")]
        public string PersonName { get; set; }  
      
         [Display(Name="User Name")]
        public string UserName { get; set; }
        public long UserLoginId { get; set; }
        public string PersonType { get; set; }
        public long? PersonId { get; set; }

         [Display(Name="Current Password")]
        public string CurrentPassword { get; set; }

         [Display(Name = "New Password")]
        public string NewPassword { get; set; }

          [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
          public int UserId { get; set; }
    }
}