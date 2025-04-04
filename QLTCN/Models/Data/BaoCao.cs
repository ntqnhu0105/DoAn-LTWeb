using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTCCN.Models.Data
{
    public class BaoCao
    {
        [Key]
        public int MaBaoCao { get; set; }

        public string MaNguoiDung { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Tháng phải nằm trong khoảng từ 1 đến 12.")]
        public int Thang { get; set; }

        [Required]
        public int Nam { get; set; }

        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(15,2)")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = true)]
        [DefaultValue(0)]
        public decimal TongThuNhap { get; set; }

        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(15,2)")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = true)]
        [DefaultValue(0)]
        public decimal TongChiTieu { get; set; }

        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(15,2)")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = true)]
        [DefaultValue(0)]
        public decimal SoTienTietKiem { get; set; }

        [StringLength(200)]
        public string? GhiChu { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime NgayTao { get; set; }

        // Navigation property
        public virtual ApplicationUser NguoiDung { get; set; }
    }
}
