using QuanLyTrungTam.Models;

namespace QuanLyTrungTam.DAL.Interfaces
{
    public interface ITaiKhoanDAL
    {
        Task<TaiKhoan?> LoginAsync(string tenDangNhap, string matKhauHash);
    }

    public interface IHocVienDAL
    {
        Task<List<HocVien>> GetAllAsync();
        Task<HocVien?> GetByIdAsync(string id);
        Task<List<HocVien>> SearchAsync(string keyword);
        Task<bool> AddAsync(HocVien hocVien);
        Task<bool> UpdateAsync(HocVien hocVien);
        Task<bool> DeleteAsync(string id);
    }

    public interface IKhoaHocDAL
    {
        Task<List<KhoaHoc>> GetAllAsync();
        Task<KhoaHoc?> GetByIdAsync(string id);
        Task<List<KhoaHoc>> SearchAsync(string keyword);
        Task<bool> AddAsync(KhoaHoc khoaHoc);
        Task<bool> UpdateAsync(KhoaHoc khoaHoc);
        Task<bool> DeleteAsync(string id);
    }

    public interface ILopHocDAL
    {
        Task<List<dynamic>> GetAllWithKhoaHocAsync();
        Task<List<dynamic>> SearchAsync(string keyword);
        Task<bool> AddAsync(LopHoc lopHoc);
        Task<bool> UpdateAsync(LopHoc lopHoc);
        Task<bool> DeleteAsync(string id);
    }

    public interface IGiangVienDAL
    {
        Task<List<GiangVien>> GetAllAsync();
        Task<List<GiangVien>> SearchAsync(string keyword);
        Task<bool> AddAsync(GiangVien giangVien);
        Task<bool> UpdateAsync(GiangVien giangVien);
        Task<bool> DeleteAsync(string id);
    }

    public interface IDangKyHocDAL
    {
        Task<List<dynamic>> GetAllDetailsAsync();
        Task<bool> AddAsync(DangKyHoc dangKyHoc);
        Task<bool> DeleteAsync(string id);
    }

    public interface IPhanCongGiangVienDAL
    {
        Task<List<dynamic>> GetAllDetailsAsync();
        Task<bool> AddAsync(PhanCongGiangVien phanCong);
        Task<bool> DeleteAsync(string id);
    }

    public interface IThanhToanDAL
    {
        Task<List<dynamic>> GetAllDetailsAsync();
        Task<bool> AddAsync(ThanhToan thanhToan);
        Task<bool> DeleteAsync(string id);
    }

    public interface IThongKeDAL
    {
        Task<dynamic> GetThongKeTongHopAsync();
    }
}
