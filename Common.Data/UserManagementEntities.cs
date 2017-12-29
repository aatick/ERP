using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data
{
    public class UserManagementEntities : IdentityDbContext<ApplicationUser>
    {
        public UserManagementEntities()
            : base("ERPDbContext", false)
        {

        }
    }
}

