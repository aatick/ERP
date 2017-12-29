namespace Hrms.Data.HRMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_Document_Upload
    {
        public long Id { get; set; }

        public int emp_id { get; set; }

        public int document_type_id { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] document_image { get; set; }

        [StringLength(200)]
        public string remarks { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int CreatedUserId { get; set; }

        public int? UpdateUserId { get; set; }

        public bool IsActive { get; set; }
    }
}
