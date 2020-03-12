namespace BGLB.MerberPoint.Entity.POCOModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Shops
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shops()
        {
            ConsumeOrders = new HashSet<ConsumeOrders>();
            ExchangGifts = new HashSet<ExchangGifts>();
            ExchangLogs = new HashSet<ExchangLogs>();
            MemCards = new HashSet<MemCards>();
            TransferLogs = new HashSet<TransferLogs>();
            Users = new HashSet<Users>();
        }

        [Key]
        public int S_ID { get; set; }

        [StringLength(20)]
        public string S_Name { get; set; }

        public int? S_Category { get; set; }

        [StringLength(20)]
        public string S_ContactName { get; set; }

        [StringLength(20)]
        public string S_ContactTel { get; set; }

        [StringLength(50)]
        public string S_Address { get; set; }

        [StringLength(100)]
        public string S_Remark { get; set; }

        public bool? S_IsHasSetAdmin { get; set; }

        public DateTime? S_CreateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConsumeOrders> ConsumeOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExchangGifts> ExchangGifts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExchangLogs> ExchangLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemCards> MemCards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransferLogs> TransferLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
