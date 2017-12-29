namespace Common.Data.CommonDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserLogin")]
    public partial class UserLogin
    {
        public long UserLoginId { get; set; }

        [StringLength(10)]
        public string PersonType { get; set; }

        public long? PersonId { get; set; }

        public bool IsRemoved { get; set; }

        public long CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public long? UpdateBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
