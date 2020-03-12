namespace BGLB.MerberPoint.Entity.POCOModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExchangGifts
    {
        [Key]
        public int EG_ID { get; set; }

        public int? S_ID { get; set; }

        [StringLength(255)]
        public string EG_GiftCode { get; set; }

        [StringLength(255)]
        public string EG_GiftName { get; set; }

        [StringLength(255)]
        public string EG_Photo { get; set; }

        public int? EG_Point { get; set; }

        public int? EG_Number { get; set; }

        public int? G_ExchangNum { get; set; }

        [StringLength(255)]
        public string EG_Remark { get; set; }

        public virtual Shops Shops { get; set; }
    }
}
