namespace BGLB.MerberPoint.Entity.POCOModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MerberPointModel : DbContext
    {
        public MerberPointModel()
            : base("name=MerberPointModel")
        {
        }

        public virtual DbSet<CardLevels> CardLevels { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CategoryItems> CategoryItems { get; set; }
        public virtual DbSet<ConsumeOrders> ConsumeOrders { get; set; }
        public virtual DbSet<ExchangGifts> ExchangGifts { get; set; }
        public virtual DbSet<ExchangLogs> ExchangLogs { get; set; }
        public virtual DbSet<MemCards> MemCards { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }
        public virtual DbSet<TransferLogs> TransferLogs { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>()
                .Property(e => e.C_Category)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .Property(e => e.C_Illustration)
                .IsUnicode(false);

            modelBuilder.Entity<Categories>()
                .HasOptional(e => e.CategoryItems)
                .WithRequired(e => e.Categories);

            modelBuilder.Entity<CategoryItems>()
                .Property(e => e.C_Category)
                .IsUnicode(false);

            modelBuilder.Entity<CategoryItems>()
                .Property(e => e.CI_Name)
                .IsUnicode(false);

            modelBuilder.Entity<ConsumeOrders>()
                .Property(e => e.CO_TotalMoney)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ConsumeOrders>()
                .Property(e => e.CO_DiscountMoney)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ConsumeOrders>()
                .Property(e => e.CO_Recash)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ConsumeOrders>()
                .Property(e => e.CO_Remark)
                .IsUnicode(false);

            modelBuilder.Entity<ExchangGifts>()
                .Property(e => e.EG_GiftCode)
                .IsUnicode(false);

            modelBuilder.Entity<ExchangGifts>()
                .Property(e => e.EG_GiftName)
                .IsUnicode(false);

            modelBuilder.Entity<ExchangGifts>()
                .Property(e => e.EG_Photo)
                .IsUnicode(false);

            modelBuilder.Entity<ExchangGifts>()
                .Property(e => e.EG_Remark)
                .IsUnicode(false);

            modelBuilder.Entity<MemCards>()
                .Property(e => e.MC_Photo)
                .IsUnicode(false);

            modelBuilder.Entity<Shops>()
                .Property(e => e.S_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Shops>()
                .Property(e => e.S_ContactName)
                .IsUnicode(false);

            modelBuilder.Entity<Shops>()
                .Property(e => e.S_ContactTel)
                .IsUnicode(false);

            modelBuilder.Entity<Shops>()
                .Property(e => e.S_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Shops>()
                .Property(e => e.S_Remark)
                .IsUnicode(false);

            modelBuilder.Entity<TransferLogs>()
                .Property(e => e.TL_TransferMoney)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TransferLogs>()
                .Property(e => e.TL_Remark)
                .IsUnicode(false);
        }
    }
}
