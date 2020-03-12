namespace BGLB.MerberPoint.Entity.POCOModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CardLevels
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CardLevels()
        {
            MemCards = new HashSet<MemCards>();
        }

        [Key]
        public int CL_ID { get; set; }

        [StringLength(20)]
        public string CL_LevelName { get; set; }

        [StringLength(50)]
        public string CL_NeedPoint { get; set; }

        public double? CL_Point { get; set; }

        public double? CL_Percent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemCards> MemCards { get; set; }
    }
}
