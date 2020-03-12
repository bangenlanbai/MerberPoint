namespace BGLB.MerberPoint.Entity.POCOModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            ConsumeOrders = new HashSet<ConsumeOrders>();
            ExchangLogs = new HashSet<ExchangLogs>();
            TransferLogs = new HashSet<TransferLogs>();
        }

        [Key]
        public int U_ID { get; set; }

        public int? S_ID { get; set; }

        [StringLength(20)]
        public string U_LoginName { get; set; }

        [StringLength(50)]
        public string U_Password { get; set; }

        [StringLength(20)]
        public string U_RealName { get; set; }

        [StringLength(10)]
        public string U_Sex { get; set; }

        [StringLength(20)]
        public string U_Telephone { get; set; }

        public int? U_Role { get; set; }

        public bool? U_CanDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConsumeOrders> ConsumeOrders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExchangLogs> ExchangLogs { get; set; }

        public virtual Shops Shops { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransferLogs> TransferLogs { get; set; }
    }
}
