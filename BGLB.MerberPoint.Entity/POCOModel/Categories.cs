namespace BGLB.MerberPoint.Entity.POCOModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Categories
    {
        [Key]
        [StringLength(20)]
        public string C_Category { get; set; }

        [StringLength(20)]
        public string C_Illustration { get; set; }

        public bool? C_IsShow { get; set; }

        public virtual CategoryItems CategoryItems { get; set; }
    }
}
