using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BerberKuaforRandevu.Controllers
{
    public class YapayZekaController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile fotograf)
        {
            if (fotograf == null || fotograf.Length == 0)
            {
                return View();
            }

            var base64Image = ConvertToBase64(fotograf);
            var apiResponse = await PostImageAsync(base64Image);

            // API yanıtını işleyin
            ViewBag.ApiResponse = apiResponse;

            Console.WriteLine(apiResponse);

            return View();
        }


        private async Task<string> PostImageAsync(string base64Image)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");

            var requestContent = new
            {
                messages = new[]
                {
                new
                {
                role = "user",
                content = new object[]
                {
                    new { type = "text", text = "Kendini profesyonel bir kuaför olarak düşün ve fotoğraftaki kişiye yeni bir saç modeli öner. Kısa bir cevap ver (4 cümleden kısa). Cevabın en azından 1 saç modeli ismi içersin. Kelime tekrarlarından kaçın." },
                    new { type = "image_url", image_url = new { url = $"data:image/jpeg;base64,{base64Image}" } }
                }
                }
                },
                model = "llama-3.2-11b-vision-preview",
                temperature = 1,
                max_tokens = 128,
                top_p = 1,
                stream = false,
                stop = null as string
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.groq.com/openai/v1/chat/completions", jsonContent);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync(); 
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent); 
            return apiResponse!.Choices.FirstOrDefault()!.Message.Content;
        }

        private string ConvertToBase64(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }

    }

    public class ApiResponse
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public long Created { get; set; }
        public string Model { get; set; }
        public List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        public int Index { get; set; }
        public Message Message { get; set; }
    }

    public class Message
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }

}
