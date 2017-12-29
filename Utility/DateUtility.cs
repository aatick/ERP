using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public  class DateUtility
    {
       public static string ExtractDate(string myDate)
       {
           string result = string.Empty;
           if (myDate != null && myDate != "")
           {
               try
               {
                   result = Convert.ToDateTime(myDate).ToString("dd-mm-yy");  //DateTime.Parse(myDate, new System.Globalization.CultureInfo("en-CA", true), System.Globalization.DateTimeStyles.AdjustToUniversal).ToString("dd-mm-yy");
                   return Convert.ToDateTime(result).Date.ToString();
               }
               catch (Exception)
               {
                   throw new ApplicationException("DateTime conversion error");
               }
           }
           return result = "";
       }
    }
}
