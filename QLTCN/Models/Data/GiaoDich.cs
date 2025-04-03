using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTCCN.Models.Data
{
    public class GiaoDich
    {
        [Key]
        public int MaGiaoDich { get; set; }

        public int MaNguoiDung { get; set; }

        public int MaTaiKhoan { get; set; }

        public int MaDanhMuc { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(15,2)")] // Đảm bảo lưu trữ chính xác 2 chữ số thập phân
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = true)] // Định dạng hiển thị
        public decimal SoTien { get; set; }

        [Required]
        [StringLength(10)]
        public string LoaiGiaoDich { get; set; }

        public DateTime NgayGiaoDich { get; set; } = DateTime.Now;

        [StringLength(200)]
        public string GhiChu { get; set; }

        // Navigation properties
        public NguoiDung NguoiDung { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
        public DanhMuc DanhMuc { get; set; }
    }
}
