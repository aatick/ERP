namespace Upms.Data.UPMSDataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileTradeStatusDSE")]
    public partial class FileTradeStatusDSE
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string Action { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [StringLength(12)]
        public string ISIN { get; set; }

        [StringLength(20)]
        public string AssetClass { get; set; }

        [StringLength(15)]
        public string OrderID { get; set; }

        [StringLength(15)]
        public string RefOrderID { get; set; }

        [StringLength(1)]
        public string Side { get; set; }

        [StringLength(16)]
        public string BOID { get; set; }

        [StringLength(50)]
        public string SecurityCode { get; set; }

        [StringLength(50)]
        public string Board { get; set; }

        [StringLength(10)]
        public string Date { get; set; }

        [StringLength(10)]
        public string Time { get; set; }

        [StringLength(20)]
        public string Quantity { get; set; }

        [StringLength(25)]
        public string Price { get; set; }

        [StringLength(25)]
        public string Value { get; set; }

        [StringLength(50)]
        public string ExecID { get; set; }

        [StringLength(15)]
        public string Session { get; set; }

        [StringLength(5)]
        public string FillType { get; set; }

        [StringLength(1)]
        public string Category { get; set; }

        [StringLength(1)]
        public string CompulsorySpot { get; set; }

        [StringLength(20)]
        public string ClientCode { get; set; }

        [StringLength(20)]
        public string TraderDealerID { get; set; }

        [StringLength(20)]
        public string OwnerDealerID { get; set; }

        [StringLength(20)]
        public string TradeReportType { get; set; }
    }
}
