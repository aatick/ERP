using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ERP.Web.ViewModels
{
    public class DayEndViewModel :BaseModel
    {
        public int? OfficeId { get; set; }
        
        public DateTime? BusinessDate { get; set; }
        public IEnumerable<SelectListItem> officeListItems { get; set; }
    }
}