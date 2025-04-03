using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;

namespace QLTCCN.Models.Data
{
    public class LoaiDauTu
    {
        [Key]
        public int MaLoaiDauTu { get; set; }

        [Required]
        [StringLength(50)]
        public string TenLoai { get; set; } // "Cổ phiếu", "Bất động sản", v.v.

        // Navigation property
        public ICollection<DauTu> DauTus { get; set; }
    }
}
