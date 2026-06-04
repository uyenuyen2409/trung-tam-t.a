using Microsoft.EntityFrameworkCore;
using QuanLyTrungTam.Data;
using QuanLyTrungTam.DAL.Interfaces;
using QuanLyTrungTam.Models;

namespace QuanLyTrungTam.DAL.Implementations
{
    public class TaiKhoanDAL : ITaiKhoanDAL
    {
        private readonly AppDbContext _context;
        public TaiKhoanDAL(AppDbContext context) { _context = context; }

        public async Task<TaiKhoan?> LoginAsync(string tenDangNhap, string matKhauHash)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                return await _context.TaiKhoans.FirstOrDefaultAsync(t => t.TenDangNhap == tenDangNhap && t.MatKhau == matKhauHash);
            }
            catch (Exception ex) { throw new Exception("Lỗi truy vấn DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }
    }

    public class HocVienDAL : IHocVienDAL
    {
        private readonly AppDbContext _context;
        public HocVienDAL(AppDbContext context) { _context = context; }

        public async Task<List<HocVien>> GetAllAsync()
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                return await _context.HocViens.ToListAsync();
            }
            catch (Exception ex) { throw new Exception("Lỗi truy vấn DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<HocVien?> GetByIdAsync(string id)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                return await _context.HocViens.FindAsync(id);
            }
            catch (Exception ex) { throw new Exception("Lỗi truy vấn DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<List<HocVien>> SearchAsync(string keyword)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                keyword = keyword.ToLower();
                return await _context.HocViens.Where(h => h.MaHocVien.ToLower().Contains(keyword) || h.HoTen.ToLower().Contains(keyword) || h.SoDienThoai.Contains(keyword)).ToListAsync();
            }
            catch (Exception ex) { throw new Exception("Lỗi truy vấn DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> AddAsync(HocVien hocVien)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                if (await _context.HocViens.AnyAsync(h => h.MaHocVien == hocVien.MaHocVien))
                    throw new Exception("Mã học viên đã tồn tại trong hệ thống!");
                _context.HocViens.Add(hocVien);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> UpdateAsync(HocVien hocVien)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var exist = await _context.HocViens.FindAsync(hocVien.MaHocVien);
                if (exist == null) throw new Exception("Không tìm thấy học viên!");
                exist.HoTen = hocVien.HoTen;
                exist.NgaySinh = hocVien.NgaySinh;
                exist.GioiTinh = hocVien.GioiTinh;
                exist.SoDienThoai = hocVien.SoDienThoai;
                exist.Email = hocVien.Email;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var exist = await _context.HocViens.FindAsync(id);
                if (exist == null) throw new Exception("Không tìm thấy học viên!");
                _context.HocViens.Remove(exist);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }
    }

    public class KhoaHocDAL : IKhoaHocDAL
    {
        private readonly AppDbContext _context;
        public KhoaHocDAL(AppDbContext context) { _context = context; }

        public async Task<List<KhoaHoc>> GetAllAsync()
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                return await _context.KhoaHocs.ToListAsync();
            }
            catch (Exception ex) { throw new Exception("Lỗi truy vấn DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<KhoaHoc?> GetByIdAsync(string id)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                return await _context.KhoaHocs.FindAsync(id);
            }
            catch (Exception ex) { throw new Exception("Lỗi truy vấn DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<List<KhoaHoc>> SearchAsync(string keyword)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                keyword = keyword.ToLower();
                return await _context.KhoaHocs.Where(k => k.MaKhoaHoc.ToLower().Contains(keyword) || k.TenKhoaHoc.ToLower().Contains(keyword)).ToListAsync();
            }
            catch (Exception ex) { throw new Exception("Lỗi truy vấn DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> AddAsync(KhoaHoc khoaHoc)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                if (await _context.KhoaHocs.AnyAsync(k => k.MaKhoaHoc == khoaHoc.MaKhoaHoc))
                    throw new Exception("Mã khóa học đã tồn tại!");
                _context.KhoaHocs.Add(khoaHoc);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> UpdateAsync(KhoaHoc khoaHoc)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var exist = await _context.KhoaHocs.FindAsync(khoaHoc.MaKhoaHoc);
                if (exist == null) throw new Exception("Không tìm thấy khóa học!");
                exist.TenKhoaHoc = khoaHoc.TenKhoaHoc;
                exist.HocPhi = khoaHoc.HocPhi;
                exist.SoBuoiHoc = khoaHoc.SoBuoiHoc;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var exist = await _context.KhoaHocs.FindAsync(id);
                if (exist == null) throw new Exception("Không tìm thấy khóa học!");
                _context.KhoaHocs.Remove(exist);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }
    }

    public class LopHocDAL : ILopHocDAL
    {
        private readonly AppDbContext _context;
        public LopHocDAL(AppDbContext context) { _context = context; }

        public async Task<List<dynamic>> GetAllWithKhoaHocAsync()
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var list = await _context.LopHocs.Include(l => l.KhoaHoc)
                    .Select(l => new {
                        l.MaLopHoc, l.TenLopHoc, l.PhongHoc, l.MaKhoaHoc,
                        TenKhoaHoc = l.KhoaHoc != null ? l.KhoaHoc.TenKhoaHoc : ""
                    }).ToListAsync();
                return list.Cast<dynamic>().ToList();
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<List<dynamic>> SearchAsync(string keyword)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                keyword = keyword.ToLower();
                var list = await _context.LopHocs.Include(l => l.KhoaHoc)
                    .Where(l => l.MaLopHoc.ToLower().Contains(keyword) || l.TenLopHoc.ToLower().Contains(keyword))
                    .Select(l => new {
                        l.MaLopHoc, l.TenLopHoc, l.PhongHoc, l.MaKhoaHoc,
                        TenKhoaHoc = l.KhoaHoc != null ? l.KhoaHoc.TenKhoaHoc : ""
                    }).ToListAsync();
                return list.Cast<dynamic>().ToList();
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> AddAsync(LopHoc lopHoc)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                if (await _context.LopHocs.AnyAsync(l => l.MaLopHoc == lopHoc.MaLopHoc))
                    throw new Exception("Mã lớp học đã tồn tại!");
                _context.LopHocs.Add(lopHoc);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> UpdateAsync(LopHoc lopHoc)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var exist = await _context.LopHocs.FindAsync(lopHoc.MaLopHoc);
                if (exist == null) throw new Exception("Không tìm thấy lớp học!");
                exist.TenLopHoc = lopHoc.TenLopHoc;
                exist.PhongHoc = lopHoc.PhongHoc;
                exist.MaKhoaHoc = lopHoc.MaKhoaHoc;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var exist = await _context.LopHocs.FindAsync(id);
                if (exist == null) throw new Exception("Không tìm thấy lớp học!");
                _context.LopHocs.Remove(exist);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }
    }

    public class GiangVienDAL : IGiangVienDAL
    {
        private readonly AppDbContext _context;
        public GiangVienDAL(AppDbContext context) { _context = context; }

        public async Task<List<GiangVien>> GetAllAsync()
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                return await _context.GiangViens.ToListAsync();
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<List<GiangVien>> SearchAsync(string keyword)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                keyword = keyword.ToLower();
                return await _context.GiangViens.Where(g => g.MaGiangVien.ToLower().Contains(keyword) || g.HoTen.ToLower().Contains(keyword)).ToListAsync();
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> AddAsync(GiangVien giangVien)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                if (await _context.GiangViens.AnyAsync(g => g.MaGiangVien == giangVien.MaGiangVien))
                    throw new Exception("Mã giảng viên đã tồn tại!");
                _context.GiangViens.Add(giangVien);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> UpdateAsync(GiangVien giangVien)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var exist = await _context.GiangViens.FindAsync(giangVien.MaGiangVien);
                if (exist == null) throw new Exception("Không tìm thấy giảng viên!");
                exist.HoTen = giangVien.HoTen;
                exist.TrinhDo = giangVien.TrinhDo;
                exist.SoDienThoai = giangVien.SoDienThoai;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var exist = await _context.GiangViens.FindAsync(id);
                if (exist == null) throw new Exception("Không tìm thấy giảng viên!");
                _context.GiangViens.Remove(exist);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }
    }

    public class DangKyHocDAL : IDangKyHocDAL
    {
        private readonly AppDbContext _context;
        public DangKyHocDAL(AppDbContext context) { _context = context; }

        public async Task<List<dynamic>> GetAllDetailsAsync()
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var list = await _context.DangKyHocs.Include(d => d.HocVien).Include(d => d.KhoaHoc)
                    .Select(d => new {
                        d.MaDangKy, d.NgayDangKy, d.MaHocVien,
                        TenHocVien = d.HocVien != null ? d.HocVien.HoTen : "",
                        d.MaKhoaHoc,
                        TenKhoaHoc = d.KhoaHoc != null ? d.KhoaHoc.TenKhoaHoc : "",
                        HocPhi = d.KhoaHoc != null ? d.KhoaHoc.HocPhi : 0
                    }).ToListAsync();
                return list.Cast<dynamic>().ToList();
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> AddAsync(DangKyHoc dangKyHoc)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                if (await _context.DangKyHocs.AnyAsync(d => d.MaDangKy == dangKyHoc.MaDangKy))
                    throw new Exception("Mã đăng ký đã tồn tại!");
                _context.DangKyHocs.Add(dangKyHoc);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var exist = await _context.DangKyHocs.FindAsync(id);
                if (exist == null) throw new Exception("Không tìm thấy đăng ký!");
                _context.DangKyHocs.Remove(exist);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }
    }

    public class PhanCongGiangVienDAL : IPhanCongGiangVienDAL
    {
        private readonly AppDbContext _context;
        public PhanCongGiangVienDAL(AppDbContext context) { _context = context; }

        public async Task<List<dynamic>> GetAllDetailsAsync()
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var list = await _context.PhanCongGiangViens.Include(p => p.GiangVien).Include(p => p.LopHoc)
                    .Select(p => new {
                        p.MaPhanCong, p.VaiTro, p.MaGiangVien,
                        TenGiangVien = p.GiangVien != null ? p.GiangVien.HoTen : "",
                        p.MaLopHoc,
                        TenLopHoc = p.LopHoc != null ? p.LopHoc.TenLopHoc : ""
                    }).ToListAsync();
                return list.Cast<dynamic>().ToList();
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> AddAsync(PhanCongGiangVien phanCong)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                if (await _context.PhanCongGiangViens.AnyAsync(p => p.MaPhanCong == phanCong.MaPhanCong))
                    throw new Exception("Mã phân công đã tồn tại!");
                _context.PhanCongGiangViens.Add(phanCong);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var exist = await _context.PhanCongGiangViens.FindAsync(id);
                if (exist == null) throw new Exception("Không tìm thấy!");
                _context.PhanCongGiangViens.Remove(exist);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }
    }

    public class ThanhToanDAL : IThanhToanDAL
    {
        private readonly AppDbContext _context;
        public ThanhToanDAL(AppDbContext context) { _context = context; }

        public async Task<List<dynamic>> GetAllDetailsAsync()
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var list = await _context.ThanhToans
                    .Include(t => t.DangKyHoc).ThenInclude(d => d!.HocVien)
                    .Include(t => t.DangKyHoc).ThenInclude(d => d!.KhoaHoc)
                    .Select(t => new {
                        t.MaHoaDon, t.SoTienThu, t.NgayThanhToan, t.MaDangKy,
                        TenHocVien = t.DangKyHoc != null && t.DangKyHoc.HocVien != null ? t.DangKyHoc.HocVien.HoTen : "",
                        TenKhoaHoc = t.DangKyHoc != null && t.DangKyHoc.KhoaHoc != null ? t.DangKyHoc.KhoaHoc.TenKhoaHoc : ""
                    }).ToListAsync();
                return list.Cast<dynamic>().ToList();
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> AddAsync(ThanhToan thanhToan)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                if (await _context.ThanhToans.AnyAsync(t => t.MaHoaDon == thanhToan.MaHoaDon))
                    throw new Exception("Mã hóa đơn đã tồn tại!");
                _context.ThanhToans.Add(thanhToan);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                var exist = await _context.ThanhToans.FindAsync(id);
                if (exist == null) throw new Exception("Không tìm thấy!");
                _context.ThanhToans.Remove(exist);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { throw new Exception("Lỗi DB: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }
    }

    public class ThongKeDAL : IThongKeDAL
    {
        private readonly AppDbContext _context;
        public ThongKeDAL(AppDbContext context) { _context = context; }

        public async Task<dynamic> GetThongKeTongHopAsync()
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
                
                var tongHocVien = await _context.HocViens.CountAsync();
                var tongKhoaHoc = await _context.KhoaHocs.CountAsync();
                var tongLopHoc = await _context.LopHocs.CountAsync();
                var tongGiangVien = await _context.GiangViens.CountAsync();
                var tongDangKy = await _context.DangKyHocs.CountAsync();
                var hocVienDangHoc = await _context.DangKyHocs.Select(d => d.MaHocVien).Distinct().CountAsync();
                var tongDoanhThu = await _context.ThanhToans.SumAsync(t => t.SoTienThu);

                return new { tongHocVien, tongKhoaHoc, tongLopHoc, tongGiangVien, tongDangKy, hocVienDangHoc, tongDoanhThu };
            }
            catch (Exception ex) { throw new Exception("Lỗi thống kê: " + ex.Message); }
            finally { await conn.CloseAsync(); }
        }
    }
}
