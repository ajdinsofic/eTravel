using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using eTravelAgencija.Model.model;
using eTravelAgencija.Model.ResponseObject;
using Microsoft.Extensions.Options;

namespace eTravelAgencija.Services.Services
{
    public class PayPalService
    {
        private readonly PayPalSettings _settings;
        private readonly HttpClient _httpClient;

        public PayPalService(
            IOptions<PayPalSettings> settings,
            IHttpClientFactory httpClientFactory)
        {
            _settings = settings.Value;
            _httpClient = httpClientFactory.CreateClient();
        }

        // =====================================================
        // 🔑 PAYPAL BASE URL (Sandbox / Live)
        // =====================================================
        private string BaseUrl =>
            _settings.Mode.Equals("Live", StringComparison.OrdinalIgnoreCase)
                ? "https://api-m.paypal.com"
                : "https://api-m.sandbox.paypal.com";

        // =====================================================
        // 🔐 GET ACCESS TOKEN
        // =====================================================
        private async Task<string> GetAccessToken()
        {
            var auth = Convert.ToBase64String(
                Encoding.ASCII.GetBytes($"{_settings.ClientId}:{_settings.Secret}")
            );

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{BaseUrl}/v1/oauth2/token"
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Basic", auth);

            request.Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("PayPal auth failed");
            }

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);

            return doc.RootElement
                .GetProperty("access_token")
                .GetString()!;
        }

        // =====================================================
        // 🧾 CREATE PAYPAL ORDER
        // =====================================================
        public async Task<PayPalOrderResponse> CreateOrder(
            decimal amount,
            string currency = "USD")
        {
            var token = await GetAccessToken();

            var body = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                    new
                    {
                        amount = new
                        {
                            currency_code = currency,
                            value = amount.ToString("0.00", CultureInfo.InvariantCulture)
                        }
                    }
                },
                application_context = new
                {
                    brand_name = "eTravel",
                    landing_page = "LOGIN",
                    user_action = "PAY_NOW",
                    return_url = "https://etravel.com/paypal-success",
                    cancel_url = "https://etravel.com/paypal-cancel"
                }
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{BaseUrl}/v2/checkout/orders"
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            request.Content = new StringContent(
                JsonSerializer.Serialize(body),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"CreateOrder failed: {json}");
            }

            return JsonSerializer.Deserialize<PayPalOrderResponse>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            )!;
        }

        // =====================================================
        // 💰 CAPTURE PAYPAL ORDER
        // =====================================================
        public async Task<PayPalCaptureResponse> CaptureOrder(string orderId)
        {
            var token = await GetAccessToken();

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{BaseUrl}/v2/checkout/orders/{orderId}/capture"
            );

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            request.Content = new StringContent("{}", Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"CaptureOrder failed: {json}");
            }

            return JsonSerializer.Deserialize<PayPalCaptureResponse>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            )!;
        }
    }
}
