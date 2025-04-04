using Microsoft.AspNetCore.Identity;

namespace QLTCCN.Models.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public ICollection<TaiKhoan> TaiKhoans { get; set; }
        public ICollection<GiaoDich> GiaoDichs { get; set; }
        public ICollection<MucTieu> MucTieus { get; set; }
        public ICollection<NoKhoanVay> NoKhoanVays { get; set; }
        public ICollection<ThongBao> ThongBaos { get; set; }
        public ICollection<DauTu> DauTus { get; set; }
    }
}
