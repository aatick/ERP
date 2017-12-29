using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upms.Data.UPMSDataModel
{
    [Table("IPOCurrencyMapping")]
    public class IPOCurrencyMapping
    {
        public int Id { get; set; }
        public int IPODeclarationId { get; set; }
        public int CurrencyId { get; set; }
        public decimal CurrencyConversionRate { get; set; }
        public decimal LotAmount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreatedUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}
