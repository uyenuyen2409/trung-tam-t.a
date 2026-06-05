using Microsoft.EntityFrameworkCore;
using QuanLyTrungTam.Models;

namespace QuanLyTrungTam.Data
{
    /// <summary>
    /// Database Context-Quản lý kết nối và ánh xạ các bảng SQLite
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<HocVien> HocViens { get; set; }
        public DbSet<KhoaHoc> KhoaHocs { get; set; }
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<GiangVien> GiangViens { get; set; }
        public DbSet<PhanCongGiangVien> PhanCongGiangViens { get; set; }
        public DbSet<DangKyHoc> DangKyHocs { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed tài khoản mặc định (Đã băm SHA256)
            modelBuilder.Entity<TaiKhoan>().HasData(
                new TaiKhoan { TenDangNhap = "admin", MatKhau = QuanLyTrungTam.Helpers.SecurityHelper.HashPassword("admin123") }
            );

            // Seed dữ liệu mẫu - Khóa học
            modelBuilder.Entity<KhoaHoc>().HasData(
                new KhoaHoc { MaKhoaHoc = "KH001", TenKhoaHoc = "Tiếng Anh Giao Tiếp", HocPhi = 5000000, SoBuoiHoc = 30 },
                new KhoaHoc { MaKhoaHoc = "KH002", TenKhoaHoc = "IELTS Cơ Bản", HocPhi = 8000000, SoBuoiHoc = 45 },
                new KhoaHoc { MaKhoaHoc = "KH003", TenKhoaHoc = "TOEIC 600+", HocPhi = 6000000, SoBuoiHoc = 36 }
            );

            // Seed dữ liệu mẫu - Giảng viên
            modelBuilder.Entity<GiangVien>().HasData(
                new GiangVien { MaGiangVien = "GV001", HoTen = "Nguyễn Văn An", TrinhDo = "Thạc sĩ Ngôn ngữ Anh", SoDienThoai = "0901234567" },
                new GiangVien { MaGiangVien = "GV002", HoTen = "Trần Thị Bích", TrinhDo = "IELTS 8.5", SoDienThoai = "0912345678" }
            );

            // Seed dữ liệu mẫu - Học viên
            modelBuilder.Entity<HocVien>().HasData(
                new HocVien { MaHocVien = "HV001", HoTen = "Lê Hoàng Nam", NgaySinh = new DateTime(2003, 5, 15), GioiTinh = "Nam", SoDienThoai = "0987654321", Email = "nam.le@email.com" },
                new HocVien { MaHocVien = "HV002", HoTen = "Phạm Thị Hoa", NgaySinh = new DateTime(2002, 8, 20), GioiTinh = "Nữ", SoDienThoai = "0976543210", Email = "hoa.pham@email.com" }
            );

            // Seed dữ liệu mẫu - Lớp học
            modelBuilder.Entity<LopHoc>().HasData(
                new LopHoc { MaLopHoc = "LH001", TenLopHoc = "GiaoTiep_01", PhongHoc = "Phòng A1", MaKhoaHoc = "KH001" },
                new LopHoc { MaLopHoc = "LH002", TenLopHoc = "IELTS_01", PhongHoc = "Phòng B2", MaKhoaHoc = "KH002" }
            );

            // Cấu hình mối quan hệ
            modelBuilder.Entity<LopHoc>()
                .HasOne(l => l.KhoaHoc)
                .WithMany(k => k.LopHocs)
                .HasForeignKey(l => l.MaKhoaHoc);

            modelBuilder.Entity<DangKyHoc>()
                .HasOne(d => d.HocVien)
                .WithMany(h => h.DangKyHocs)
                .HasForeignKey(d => d.MaHocVien);

            modelBuilder.Entity<DangKyHoc>()
                .HasOne(d => d.KhoaHoc)
                .WithMany(k => k.DangKyHocs)
                .HasForeignKey(d => d.MaKhoaHoc);

            modelBuilder.Entity<PhanCongGiangVien>()
                .HasOne(p => p.GiangVien)
                .WithMany(g => g.PhanCongGiangViens)
                .HasForeignKey(p => p.MaGiangVien);

            modelBuilder.Entity<PhanCongGiangVien>()
                .HasOne(p => p.LopHoc)
                .WithMany(l => l.PhanCongGiangViens)
                .HasForeignKey(p => p.MaLopHoc);

            modelBuilder.Entity<ThanhToan>()
                .HasOne(t => t.DangKyHoc)
                .WithMany(d => d.ThanhToans)
                .HasForeignKey(t => t.MaDangKy);
        }
    }
}
