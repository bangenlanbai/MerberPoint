namespace BGLB.MerberPoint.Entity.POCOModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TransferLogs
    {
        [Key]
        public int TL_ID { get; set; }

        public int? S_ID { get; set; }

        public int? U_ID { get; set; }

        public int? TL_FromMC_ID { get; set; }

        [StringLength(50)]
        public string TL_FromMC_CardID { get; set; }

        public int? TL_ToMC_ID { get; set; }

        [StringLength(50)]
        public string TL_ToMC_CardID { get; set; }

        [Column(TypeName = "money")]
        public decimal? TL_TransferMoney { get; set; }

        [StringLength(200)]
        public string TL_Remark { get; set; }

        public DateTime? TL_CreateTime { get; set; }

        public virtual Shops Shops { get; set; }

        public virtual Users Users { get; set; }
    }
}
