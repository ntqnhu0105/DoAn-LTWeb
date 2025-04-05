namespace QLTCCN.Models.ViewModels
{
    public class BaoCaoViewModel
    {
        public decimal TongThuNhap { get; set; }
        public decimal TongChiTieu { get; set; }
        public decimal SoTienTietKiem { get; set; }
        public decimal NoPhaiTra { get; set; }
        public decimal LoiNhuanDauTu { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public List<TienDoMucTieuViewModel> TienDoMucTieu { get; set; }
        public string ThuChiData { get; set; }
        public string DanhMucLabels { get; set; }
        public string DanhMucData { get; set; }
        public string DanhMucColors { get; set; }
    }

    public class TienDoMucTieuViewModel
    {
        public string TenMucTieu { get; set; }
        public decimal TienDo { get; set; }
    }
}