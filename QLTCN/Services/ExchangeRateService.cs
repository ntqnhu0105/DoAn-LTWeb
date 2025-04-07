using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace QLTCCN.Services
{
    public interface IExchangeRateService
    {
        Task<Dictionary<string, decimal>> GetExchangeRatesAsync(string baseCurrency);
        Task<decimal> ConvertCurrencyAsync(string fromCurrency, string toCurrency, decimal amount);
    }

    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public ExchangeRateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ExchangeRateApi:ApiKey"] ?? throw new ArgumentNullException(nameof(configuration), "API key is missing in configuration.");
            _baseUrl = configuration["ExchangeRateApi:BaseUrl"] ?? throw new ArgumentNullException(nameof(configuration), "Base URL is missing in configuration.");
        }

        public async Task<Dictionary<string, decimal>> GetExchangeRatesAsync(string baseCurrency)
        {
            var url = $"{_baseUrl}{_apiKey}/latest/{baseCurrency}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Không thể lấy tỷ giá ngoại tệ.");
            }

            var content = await response.Content.ReadAsStringAsync();
            using var jsonDoc = JsonDocument.Parse(content);
            var root = jsonDoc.RootElement;

            if (root.GetProperty("result").GetString() != "success")
            {
                throw new Exception("Lỗi khi lấy tỷ giá: " + root.GetProperty("error-type").GetString());
            }

            var rates = new Dictionary<string, decimal>();
            var conversionRates = root.GetProperty("conversion_rates");

            foreach (var rate in conversionRates.EnumerateObject())
            {
                rates[rate.Name] = rate.Value.GetDecimal();
            }

            return rates;
        }

        public async Task<decimal> ConvertCurrencyAsync(string fromCurrency, string toCurrency, decimal amount)
        {
            var rates = await GetExchangeRatesAsync(fromCurrency);
            if (!rates.ContainsKey(toCurrency))
            {
                throw new Exception($"Không hỗ trợ chuyển đổi từ {fromCurrency} sang {toCurrency}.");
            }

            var rate = rates[toCurrency];
            return amount * rate;
        }
    }
}