namespace BGLB.MerberPoint.Entity.POCOModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CategoryItems
    {
        [Key]
        [StringLength(20)]
        public string C_Category { get; set; }

        public int? CI_ID { get; set; }

        [StringLength(20)]
        public string CI_Name { get; set; }

        public virtual Categories Categories { get; set; }
    }
}
