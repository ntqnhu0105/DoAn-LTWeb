using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLTCCN.Models.Data
{
    public class LichSuTraNo
    {
        [Key]
        public int MaTraNo { get; set; }

        public int MaNo { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(15,2)")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ", ApplyFormatInEditMode = true)]
        public decimal SoTienTra { get; set; }

        public DateTime NgayTra { get; set; } = DateTime.Now;

        [StringLength(200)]
        public string GhiChu { get; set; }

        // Navigation property
        public NoKhoanVay NoKhoanVay { get; set; }
    }
}
