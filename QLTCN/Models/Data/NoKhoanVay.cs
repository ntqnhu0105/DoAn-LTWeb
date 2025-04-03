using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTCCN.Models.Data
{
    public class NoKhoanVay
    {
        [Key]
        public int MaNo { get; set; }

        public string MaNguoiDung { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(15,2)")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = true)]
        public decimal SoTien { get; set; }

        [Range(0, 999.99)]
        public decimal LaiSuat { get; set; }

        [Range(1, int.MaxValue)]
        public int KyHan { get; set; }

        [Required]
        public DateTime NgayBatDau { get; set; }

        public DateTime? NgayKetThuc { get; set; }

        [StringLength(20)]
        public string TrangThai { get; set; } // "Hoạt động", "Đã thanh toán", "Quá hạn"

        [StringLength(200)]
        public string GhiChu { get; set; }

        public DateTime? NgayTraTiepTheo { get; set; }

        // Navigation properties
        public NguoiDung NguoiDung { get; set; }
        public ICollection<LichSuTraNo> LichSuTraNos { get; set; }
    }
}
