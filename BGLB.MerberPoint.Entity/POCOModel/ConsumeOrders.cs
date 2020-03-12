namespace BGLB.MerberPoint.Entity.POCOModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ConsumeOrders
    {
        [Key]
        public int CO_ID { get; set; }

        public int? S_ID { get; set; }

        public int? U_ID { get; set; }

        [StringLength(20)]
        public string CO_OrderCode { get; set; }

        public byte? CO_OrderType { get; set; }

        public int? MC_ID { get; set; }

        [StringLength(50)]
        public string MC_CardID { get; set; }

        public int? EG_ID { get; set; }

        [Column(TypeName = "money")]
        public decimal? CO_TotalMoney { get; set; }

        [Column(TypeName = "money")]
        public decimal? CO_DiscountMoney { get; set; }

        public int? CO_GavePoint { get; set; }

        [Column(TypeName = "money")]
        public decimal? CO_Recash { get; set; }

        [StringLength(255)]
        public string CO_Remark { get; set; }

        public DateTime? CO_CreateTime { get; set; }

        public virtual Shops Shops { get; set; }

        public virtual Users Users { get; set; }
    }
}
