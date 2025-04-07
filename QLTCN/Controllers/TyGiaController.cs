using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLTCCN.Services;
using System.Threading.Tasks;

namespace QLTCCN.Controllers
{
    [Authorize]
    public class TyGiaController : Controller
    {
        private readonly IExchangeRateService _exchangeRateService;

        public TyGiaController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        // GET: TyGia
        public async Task<IActionResult> Index(string baseCurrency = "USD")
        {
            try
            {
                var rates = await _exchangeRateService.GetExchangeRatesAsync(baseCurrency);
                ViewBag.BaseCurrency = baseCurrency;
                ViewBag.Rates = rates;
                ViewBag.Currencies = rates.Keys.OrderBy(k => k).ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        // GET: TyGia/Convert
        public IActionResult Convert()
        {
            ViewBag.Currencies = new List<string> { "USD", "VND", "EUR", "JPY", "GBP", "AUD", "CAD", "CNY" }; // Danh sách tiền tệ phổ biến
            return View();
        }

        // POST: TyGia/Convert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Convert(string fromCurrency, string toCurrency, decimal amount)
        {
            ViewBag.Currencies = new List<string> { "USD", "VND", "EUR", "JPY", "GBP", "AUD", "CAD", "CNY" };

            if (string.IsNullOrEmpty(fromCurrency) || string.IsNullOrEmpty(toCurrency) || amount <= 0)
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin và số tiền hợp lệ.";
                return View();
            }

            try
            {
                var convertedAmount = await _exchangeRateService.ConvertCurrencyAsync(fromCurrency, toCurrency, amount);
                ViewBag.Result = $"{amount:N2} {fromCurrency} = {convertedAmount:N2} {toCurrency}";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }
    }
}