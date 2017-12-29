using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Service.ServiceModel
{
   public class RResult
    {
        public int result { get; set; }
        public int isSuccess { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public DataTable dataList { get; set; }
    }
}
