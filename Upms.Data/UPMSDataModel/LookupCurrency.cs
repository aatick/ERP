using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSDataModel
{
    [Table("LookupCurrency")]
    public class LookupCurrency
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyShortName { get; set; }
    }
}
