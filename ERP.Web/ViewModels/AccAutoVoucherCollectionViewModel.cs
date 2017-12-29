using System.Collections.Generic;
using System.Web.Mvc;

namespace ERP.Web.ViewModels
{
    public class AccAutoVoucherCollectionViewModel
    {
        public int? OfficeId { get; set; }
        public string BusinessDate { get; set; }
        public IEnumerable<SelectListItem> officeListItems { get; set; }
    }
}