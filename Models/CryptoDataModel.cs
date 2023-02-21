using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AlphaVantage.Net.Crypto;
using Newtonsoft.Json.Linq;
using LiveCharts;
using LiveCharts.Wpf;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CryptoO.Models
{
    public class CryptoDataModel
    {
        public async Task<List<CryptoDataPoint>> GetCryptoData(string symbol, string currency, int days)
        {
            List<CryptoDataPoint> dataPoints = new List<CryptoDataPoint>();

            using (var client = new HttpClient())
            {
                string url = $"https://api.coingecko.com/api/v3/coins/{symbol}/market_chart?vs_currency={currency}&days={days}&interval=daily";
                var response = await client.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();
                JObject jsonData = JObject.Parse(responseContent);

                JArray pricesArray = jsonData["prices"].Value<JArray>();

                foreach (JArray data in pricesArray)
                {
                    double unixTimeStamp = data[0].Value<double>();
                    double price = data[1].Value<double>();

                    DateTime timeStamp = DateTimeOffset.FromUnixTimeMilliseconds((long)unixTimeStamp).LocalDateTime;
                    CryptoDataPoint dataPoint = new CryptoDataPoint(timeStamp, price);
                    dataPoints.Add(dataPoint);
                }
            }

            return dataPoints;
        }
    }

}