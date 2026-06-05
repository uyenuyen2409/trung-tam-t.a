using Microsoft.EntityFrameworkCore;
using QuanLyTrungTam.Data;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Cấu hình Entity Framework Core với SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=QuanLyTrungTam.db"));

// Cấu hình CORS cho phép Frontend gọi API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Đăng ký Dependency Injection cho 3-Tier (DAL)
builder.Services.AddScoped<QuanLyTrungTam.DAL.Interfaces.ITaiKhoanDAL, QuanLyTrungTam.DAL.Implementations.TaiKhoanDAL>();
builder.Services.AddScoped<QuanLyTrungTam.DAL.Interfaces.IHocVienDAL, QuanLyTrungTam.DAL.Implementations.HocVienDAL>();
builder.Services.AddScoped<QuanLyTrungTam.DAL.Interfaces.IKhoaHocDAL, QuanLyTrungTam.DAL.Implementations.KhoaHocDAL>();
builder.Services.AddScoped<QuanLyTrungTam.DAL.Interfaces.ILopHocDAL, QuanLyTrungTam.DAL.Implementations.LopHocDAL>();
builder.Services.AddScoped<QuanLyTrungTam.DAL.Interfaces.IGiangVienDAL, QuanLyTrungTam.DAL.Implementations.GiangVienDAL>();
builder.Services.AddScoped<QuanLyTrungTam.DAL.Interfaces.IPhanCongGiangVienDAL, QuanLyTrungTam.DAL.Implementations.PhanCongGiangVienDAL>();
builder.Services.AddScoped<QuanLyTrungTam.DAL.Interfaces.IDangKyHocDAL, QuanLyTrungTam.DAL.Implementations.DangKyHocDAL>();
builder.Services.AddScoped<QuanLyTrungTam.DAL.Interfaces.IThanhToanDAL, QuanLyTrungTam.DAL.Implementations.ThanhToanDAL>();
builder.Services.AddScoped<QuanLyTrungTam.DAL.Interfaces.IThongKeDAL, QuanLyTrungTam.DAL.Implementations.ThongKeDAL>();

var app = builder.Build();

// Tự động tạo database và seed dữ liệu
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");

// Phục vụ file tĩnh từ thư mục wwwroot (HTML, CSS, JS)
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();

// ===== GIẢ LẬP CỬA SỔ DESKTOP =====
// Khi backend khởi động xong, tự động mở trình duyệt ở chế độ --app
// (ẩn thanh địa chỉ, giống hệt app desktop)
var url = "http://localhost:5000";
app.Urls.Add(url);

_ = Task.Run(async () =>
{
    await Task.Delay(1500); // Đợi server khởi động

    try
    {
        // Thử mở bằng Edge (--app mode)
        var edgePaths = new[]
        {
            @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe",
            @"C:\Program Files\Microsoft\Edge\Application\msedge.exe"
        };

        string? edgePath = edgePaths.FirstOrDefault(File.Exists);

        if (edgePath != null)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = edgePath,
                Arguments = $"--app={url}/login.html --window-size=1280,800",
                UseShellExecute = false
            });
        }
        else
        {
            // Fallback: mở bằng Chrome
            Process.Start(new ProcessStartInfo
            {
                FileName = "chrome",
                Arguments = $"--app={url}/login.html --window-size=1280,800",
                UseShellExecute = true
            });
        }
    }
    catch
    {
        // Nếu không tìm được trình duyệt, mở bằng trình duyệt mặc định
        Process.Start(new ProcessStartInfo
        {
            FileName = $"{url}/login.html",
            UseShellExecute = true
        });
    }
});

app.Run();
