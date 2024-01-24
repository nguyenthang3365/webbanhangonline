using System.Data.Entity;
namespace project03.Models.entiy
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Qlhanghoa")
        {
        }

       
        public virtual DbSet<LoaiHang> LoaiHangs { get; set; }
      
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }

        public virtual DbSet<GIOHANG> GIOHANGS { get; set; }

        public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }

        public virtual DbSet<LICHSUMUA> LICHSUMUAs { get; set; }

        public virtual DbSet<BINHLUAN> BINHLUANs { get; set; }
        public virtual DbSet<SLIDES> SLIDEs { get; set; }
        public virtual DbSet<TRUYCAP> TRUYCAPs { get; set; }

        public virtual DbSet<LIENHE> LIENHEs { get; set; }

        public virtual DbSet<BINHLUAN_TIN> BINHLUAN_TINs { get; set; }

        public virtual DbSet<TINTUC> TINTUCs { get; set; }
        public virtual DbSet<KICHTHUOC> KICHTHUOCs { get; set; }
        /* protected override void OnModelCreating(DbModelBuilder modelBuilder)
         {
             modelBuilder.Entity<HangHoa>()
                 .Property(e => e.Gia)
                 .HasPrecision(18, 0);

             modelBuilder.Entity<LoaiHang>()
                 .HasMany(e => e.HangHoas)
                 .WithRequired(e => e.LoaiHang)
                 .WillCascadeOnDelete(false);
         }*/
    }
}
