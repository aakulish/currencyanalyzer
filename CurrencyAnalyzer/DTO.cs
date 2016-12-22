using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyAnalyzer
{
    class ExchangeRateDTO
    {
        [JsonProperty("RUB")]
        public double Rub { get; set; }
    }
    
    class ResponseDTO
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("rates")]
        public ExchangeRateDTO Rate { get; set; }
    }
}
