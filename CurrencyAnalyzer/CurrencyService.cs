using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyAnalyzer
{
    class CurrencyService
    {
        public async Task<ExchangeRate> GetExchangeRateForDate(string currency, DateTime date)
        {
            HttpClient client = new HttpClient();
            string url = string.Format("http://api.fixer.io/{0}?symbols={1},RUB&base={1}", date.ToString("yyyy-MM-dd"), currency);
           
            string result = await client.GetStringAsync(url);

            var response = JsonConvert.DeserializeObject<ResponseDTO>(result);

            // Response came for a different date
            if (DateTime.ParseExact(response.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture) != date)
                return null;
            return new ExchangeRate { Date = date, Rate = response.Rate.Rub };
        }

        public async Task<List<ExchangeRate>> GetLastNRates(string currency, int n)
        {
            DateTime date = DateTime.Now.Date;
            List<ExchangeRate> result = new List<ExchangeRate>();
            int cnt = 0;
            while (cnt < n)
            {
                var rate = await GetExchangeRateForDate(currency, date);
                // rate will be null if the response comes for a different date
                if (rate != null)
                {
                    result.Add(rate);                    
                    cnt++;
                }
                date = date.AddDays(-1);
            }
            return result;
        }
    }
}
