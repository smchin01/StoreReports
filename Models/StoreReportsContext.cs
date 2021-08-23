using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StoreReports.Models
{
    public partial class StoreReportsContext : DbContext
    {
        public StoreReportsContext()
        {
        }

        public StoreReportsContext(DbContextOptions<StoreReportsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<PaidOut> PaidOuts { get; set; }
        public virtual DbSet<ReportDatum> ReportData { get; set; }
        public virtual DbSet<TotalSale> TotalSales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-QTQ7UB4;Database=StoreReports;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bill>(entity =>
            {
                //entity.HasKey(e => e.Date)
                //  .HasName("PK__bills__D9DE21FCD0FD1247");

                entity.HasKey(e => new { e.Bill1, e.Date })
                  .HasName("PK__bills__D9DE21FCD0FD1247");

                entity.ToTable("bills");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("amount");

                entity.Property(e => e.Bill1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("bill");

                //entity.HasOne(d => d.DateNavigation)
                //    .WithMany(p => p.Bill)
                //    .OnDelete(DeleteBehavior.ClientSetNull);


            });

            modelBuilder.Entity<PaidOut>(entity =>
            {
                entity.HasKey(e => e.Date)
                    .HasName("PK__paid_out__D9DE21FC952FAD32");

                entity.ToTable("paid_outs");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("amount");

                entity.Property(e => e.Reason)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("reason");

                entity.HasOne(d => d.DateNavigation)
                    .WithOne(p => p.PaidOut)
                    .HasForeignKey<PaidOut>(d => d.Date)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__paid_outs__date__3A81B327");
            });

            modelBuilder.Entity<ReportDatum>(entity =>
            {
                entity.HasKey(e => e.Date)
                    .HasName("PK__report_d__D9DE21FC7BD11BCA");

                entity.ToTable("report_data");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Cash)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("cash");

                entity.Property(e => e.Checks)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("checks");

                entity.Property(e => e.CreditCard)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("credit_card");

                entity.Property(e => e.Gas)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("gas");

                entity.Property(e => e.InstantLotto)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("instant_lotto");

                entity.Property(e => e.NonTax)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("non_tax");

                entity.Property(e => e.OnlineLotto)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("online_lotto");

                entity.Property(e => e.PaidOuts)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("paid_outs");

                entity.Property(e => e.PaidOutsLotto)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("paid_outs_lotto");

                entity.Property(e => e.StateTax)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("state_tax");

                entity.Property(e => e.Taxable)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("taxable");
            });

            modelBuilder.Entity<TotalSale>(entity =>
            {
                entity.HasKey(e => e.Date)
                    .HasName("PK__total_sa__D9DE21FCC84AA340");

                entity.ToTable("total_sales");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("total_amount");

                entity.HasOne(d => d.DateNavigation)
                    .WithOne(p => p.TotalSale)
                    .HasForeignKey<TotalSale>(d => d.Date)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__total_sale__date__37A5467C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
