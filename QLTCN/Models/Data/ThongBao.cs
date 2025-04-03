using System.ComponentModel.DataAnnotations;

namespace QLTCCN.Models.Data
{
    public class ThongBao
    {
        [Key]
        public int MaThongBao { get; set; }

        public string MaNguoiDung { get; set; }

        [Required]
        [StringLength(500)]
        public string NoiDung { get; set; }

        public DateTime Ngay { get; set; } = DateTime.Now;

        public bool DaDoc { get; set; } = false;

        [StringLength(20)]
        public string Loai { get; set; } // "Nhắc nhở", "Cảnh báo", "Cập nhật"

        public int? MaLienKet { get; set; } // Liên kết đến bảng khác

        // Navigation property
        public NguoiDung NguoiDung { get; set; }
    }
}
