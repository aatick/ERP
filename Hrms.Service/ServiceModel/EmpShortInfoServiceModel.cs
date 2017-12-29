using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Service.ServiceModel
{
 public   class EmpShortInfoServiceModel
    {
        /*
       emp_id emp_name    emp_office_code branch_id   branch_name branch_office_code	
branch_short_name dept_id dept_office_id dept_name   dept_short_name desg_id desg_name	
desg_short_name desg_office_code    job_office_code job_name    job_reportord

    */
          public int  emp_id { get; set; }
        public string emp_office_code { get; set; }
        public string emp_name { get; set; }
         public int? branch_id { get; set; }
        public string branch_name { get; set; }
        public string branch_office_code { get; set; }
        public string branch_short_name { get; set; }
        public int? dept_id { get; set; }
        public int? dept_office_id { get; set; }
        public string dept_name { get; set; }
        public string dept_short_name { get; set; }
        public int? desg_id { get; set; }
        public string desg_name { get; set; }
        public string desg_short_name { get; set; }
        public string desg_office_code { get; set; }
        public string job_office_code { get; set; }
        public string job_name { get; set; }
        public decimal? job_reportord { get; set; }
        public decimal? emp_status_id { get; set; }


    }
}
