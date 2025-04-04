using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTCCN.Models.Data
{
    public class TaiKhoan
    {
        [Key]
        public int MaTaiKhoan { get; set; }

        public string MaNguoiDung { get; set; }

        [Required]
        [StringLength(50)]
        public string TenTaiKhoan { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(15,2)")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = true)]
        public decimal SoDu { get; set; } = 0;

        [StringLength(20)]
        public string LoaiTaiKhoan { get; set; } // "Tiền mặt", "Thẻ tín dụng", v.v.

        public DateTime NgayTao { get; set; } = DateTime.Now;

        // Navigation properties
        public ApplicationUser? NguoiDung { get; set; }
        public ICollection<GiaoDich>? GiaoDichs { get; set; }
    }
}
