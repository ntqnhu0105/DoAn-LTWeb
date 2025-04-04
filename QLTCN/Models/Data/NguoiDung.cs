using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTCCN.Models.Data
{
    public class NguoiDung
    {
        [Key]
        public string MaNguoiDung { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        // Navigation properties
        public ICollection<TaiKhoan> TaiKhoans { get; set; }
        public ICollection<GiaoDich> GiaoDichs { get; set; }
        public ICollection<MucTieu> MucTieus { get; set; }
        public ICollection<NoKhoanVay> NoKhoanVays { get; set; }
        public ICollection<ThongBao> ThongBaos { get; set; }
        public ICollection<DauTu> DauTus { get; set; }
    }
}
