using CarSales.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Client.Services
{
    public class ApiClient
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;

        public ApiClient(string baseUrl = "https://localhost:44381/api/")
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("Base URL не может быть пустым", nameof(baseUrl));

            _baseUrl = baseUrl.TrimEnd('/') + "/";
            _http = new HttpClient();
        }

        /// <summary>
        /// Получить ежемесячный отчёт по продажам.
        /// </summary>
        public async Task<List<MonthlySalesReportDto>> GetMonthlySalesAsync(int year, string model = null)
        {
            var url = BuildUrl("reports/monthly", year, model);

            using var response = await _http.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Ошибка API ({response.StatusCode}): {error}");
            }

            var data = await response.Content.ReadFromJsonAsync<List<MonthlySalesReportDto>>();
            return data ?? new List<MonthlySalesReportDto>();
        }

        /// <summary>
        /// Получить ежемесячный отчёт по продажам в формате Excel.
        /// </summary>
        public async Task<byte[]> GetMonthlySalesExcelAsync(int year, string model = null)
        {
            var url = BuildUrl("reports/monthly/excel", year, model);

            using var response = await _http.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Ошибка API ({response.StatusCode}): {error}");
            }

            return await response.Content.ReadAsByteArrayAsync();
        }

        /// <summary>
        /// Формируем полный URL.
        /// </summary>
        private string BuildUrl(string endpoint, int year, string model)
        {
            var url = $"{_baseUrl}{endpoint}?year={year}";
            if (!string.IsNullOrWhiteSpace(model))
                url += $"&model={Uri.EscapeDataString(model)}";
            return url;
        }
    }
}
        
