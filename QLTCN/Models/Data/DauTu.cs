using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTCCN.Models.Data
{
    public class DauTu
    {
        [Key]
        public int MaDauTu { get; set; }

        public string MaNguoiDung { get; set; }

        public int MaLoaiDauTu { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(15,2)")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = true)]
        public decimal GiaTri { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(15,2)")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = true)]
        public decimal GiaTriHienTai { get; set; }

        public DateTime Ngay { get; set; } = DateTime.Now;

        public DateTime? NgayKetThuc { get; set; }

        [StringLength(20)]
        public string TrangThai { get; set; } // "Hoạt động", "Đã bán", "Đang chờ"

        [StringLength(200)]
        public string GhiChu { get; set; }

        // Navigation properties
        public NguoiDung NguoiDung { get; set; }
        public LoaiDauTu LoaiDauTu { get; set; }
    }
}
