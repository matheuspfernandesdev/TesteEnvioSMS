using System.IO;
using System.Net;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;

namespace TesteEnvioSMS
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            await EnviarSMS();



            //string pathJson = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, @"Settings.json");

            //string myJsonString = File.ReadAllText(pathJson);
            //Settings settings = JsonSerializer.Deserialize<Settings>(myJsonString);

            //string ceperinha = settings.chave_ofc_comtele_prd;







            //dynamic item = await JsonFileReader.ReadAsync<Item>(pathJson);
            //string teste3 = item.chave_ofc_comtele_prd;





            //dynamic settings = JsonDocument.Parse(teste);

            //string teste2 = settings.chave_ofc_comtele_prd;

            //using (StreamReader r = new StreamReader("file.json"))
            //{
            //    string json = r.ReadToEnd();
            //    List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
            //}

        }


        public static T Read<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(text);
        }

        public static class JsonFileReader
        {
            public static async Task<T> ReadAsync<T>(string filePath)
            {
                using FileStream stream = File.OpenRead(filePath);
                return await JsonSerializer.DeserializeAsync<T>(stream);
            }
        }

        private static async Task EnviarSMS()
        {
            using (var client = new HttpClient())
            {
                object envio = new
                {
                    Sender = "1",
                    Receivers = "5531989683332",
                    Content = "A ceperinha não tá jambrada"
                };

                var jsonContent = JsonSerializer.Serialize(envio);
                var conteudo = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                conteudo.Headers.Add("auth-key", "724b7f2e-6147-4001-ae25-bf2a68001ca5");
                var resposta = await client.PostAsync("https://sms.comtele.com.br/api/v2/send", conteudo);
                string stringResponse = await resposta.Content.ReadAsStringAsync();

                if (!resposta.IsSuccessStatusCode)
                {
                    throw new Exception();
                }
            }



            /////////////////////



            //var companyForCreation = new CompanyForCreationDto
            //{
            //    Name = "Hawk IT Ltd.",
            //    Country = "USA",
            //    Address = "Hawk IT Street 365"
            //};

            //var company = JsonSerializer.Serialize(companyForCreation);

            //var request = new HttpRequestMessage(HttpMethod.Post, "companies");
            ////request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //request.Content = new StringContent(company, Encoding.UTF8);
            //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //var response = await _httpClient.SendAsync(request);
            //response.EnsureSuccessStatusCode();

            //var content = await response.Content.ReadAsStringAsync();
            //var createdCompany = JsonSerializer.Deserialize<CompanyDto>(content, _options);
        }

    }
}
