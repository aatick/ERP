namespace Common.Data.CommonDataModel
{
    // using UcasPortfolioCodeFirstMigration.Db;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UcasSoftware_Projects")]
    public partial class UcasSoftware_Projects
    {
       

        [Key]
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string AccName { get; set; }
        public string ProjectShortName { get; set; }
        public string Style_Css { get; set; }
        public bool? IsActive { get; set; }
        public string ProjectHomePage { get; set; }

       
    }
}
