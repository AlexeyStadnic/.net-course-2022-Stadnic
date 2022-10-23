using Models;
using Newtonsoft.Json;

namespace Services
{
    public class CurrencyService
    {        
        public async Task<CurrencyResponce> CurrencyConversion(Currency from, Currency to, int amount)
        {
            CurrencyResponce currencyResponce;
            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await 
                    client.GetAsync($"https://www.amdoren.com/api/currency.php?api_key=nQpJHTHkKqHLkiqRmrq7nEXTRGmgtf" +
                    $"&from={from.Name}&to={to.Name}&amount={amount}");
                responseMessage.EnsureSuccessStatusCode();
                string message = await responseMessage.Content.ReadAsStringAsync();
                currencyResponce = JsonConvert.DeserializeObject<CurrencyResponce>(message);
            }
            return currencyResponce;
        }
    }
}
