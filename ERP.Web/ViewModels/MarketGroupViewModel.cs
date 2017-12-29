using System;

namespace ERP.Web.ViewModels
{
    public class MarketGroupViewModel
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
       public string GroupName { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreatedUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}