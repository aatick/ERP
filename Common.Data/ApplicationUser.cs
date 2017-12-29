using Common.Data.CommonDataModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            DateCreated = DateTime.Now;
        }

        public string FirstName { get; set; }
        public int UserId { get; set; }
        public string LastName { get; set; }

        public string ProfilePicUrl { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public bool Activated { get; set; }

        public int RoleId { get; set; }

        //public string UserName { get; set; }

        public string PersonType { get; set; }

        public long PersonId { get; set; }

        public bool IsRemoved { get; set; }

        public string DisplayName
        {
            get { return FirstName + " " + LastName; }
        }
        // public virtual Employee Employee { get; set; }
    }
}
