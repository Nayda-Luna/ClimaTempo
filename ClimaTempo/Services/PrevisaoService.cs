using ClimaTempo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClimaTempo.Services
{
    public class PrevisaoService
    {
        private HttpClient client;
        private Previsao previsaoProximosDias;
        private Previsao previsao;
        private JsonSerializerOptions options;
        Uri uri = new Uri("https://brasilapi.com.br/api/cptec/v1/clima/previsao/244");
        public PrevisaoService()
        {
            client = new HttpClient();
            options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<Previsao> GetPrevisaoById(int cityCode)
        {
           // cityCode = 244;
            //Uri completeUri = new Uri($"{uri}/{cityCode}");
            Uri requestUri = new Uri($"{uri}/{Id}");
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    previsao = JsonSerializer.Deserialize<Previsao>(content, options);
                }
                else {
                    previsao = new Previsao
                    {
                        Estado = "SP",
                        clima = new List<Clima>
                        {
                            new Clima {Max=32, Min=28 }
                        }
                    };
                 }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return previsao;
        }

 

        public async Task<Previsao> GetPrevisaoById(int cityCode, int days)
        {
            cityCode = 244;
            days = 3;
            Uri requestUri = new Uri($"{uri}/{cityCode}/{days}");
            try
            {
                HttpResponseMessage response = await client.GetAsync(requestUri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    previsaoProximosDias = JsonSerializer.Deserialize<Previsao>(content, options);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return previsaoProximosDias;
        }
    }
}




