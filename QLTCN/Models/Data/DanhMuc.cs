using System.ComponentModel.DataAnnotations;

namespace QLTCCN.Models.Data
{
    public class DanhMuc
    {
        [Key]
        public int MaDanhMuc { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDanhMuc { get; set; }

        [StringLength(10)]
        public string Loai { get; set; } // "Thu" hoặc "Chi"

        // Navigation properties
        public ICollection<GiaoDich> GiaoDichs { get; set; }
        public ICollection<MucTieu> MucTieus { get; set; }
    }
}
