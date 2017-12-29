using System;

namespace ERP.Web.ViewModels
{
    public class LookupRelationViewModel
    {
        public int Id { get; set; }
        public string Relation { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }
        public bool IsActive { get; set; }
    }
}