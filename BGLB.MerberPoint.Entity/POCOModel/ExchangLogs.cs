namespace BGLB.MerberPoint.Entity.POCOModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExchangLogs
    {
        [Key]
        public int EL_ID { get; set; }

        public int? S_ID { get; set; }

        public int? U_ID { get; set; }

        public int? MC_ID { get; set; }

        [StringLength(50)]
        public string MC_CardID { get; set; }

        [StringLength(20)]
        public string MC_Name { get; set; }

        public int? EG_ID { get; set; }

        [StringLength(50)]
        public string EG_GiftCode { get; set; }

        [StringLength(50)]
        public string EG_GiftName { get; set; }

        public int? L_Number { get; set; }

        public int? EL_Point { get; set; }

        public DateTime? EL_CreateTime { get; set; }

        public virtual Shops Shops { get; set; }

        public virtual Users Users { get; set; }
    }
}
