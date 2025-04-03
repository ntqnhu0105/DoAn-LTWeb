using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTCCN.Models.Data
{
    public class MucTieu
    {
        [Key]
        public int MaMucTieu { get; set; }

        public int MaNguoiDung { get; set; }

        public int MaDanhMuc { get; set; }

        [Required]
        [StringLength(100)]
        public string TenMucTieu { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(15,2)")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = true)]
        public decimal SoTienMucTieu { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(15,2)")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = true)]
        public decimal SoTienHienTai { get; set; } = 0;

        [Required]
        public DateTime HanChot { get; set; }

        [StringLength(20)]
        public string TrangThai { get; set; } // "Đang thực hiện", "Hoàn thành", "Thất bại"

        [StringLength(200)]
        public string GhiChu { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        // Navigation properties
        public NguoiDung NguoiDung { get; set; }
        public DanhMuc DanhMuc { get; set; }
    }
}
