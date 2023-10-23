using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebsiteBanGiayDep23.Controllers
{
    public class ChatGPTController : Controller
    {
        private const string OpenAIApiKey = "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";
        private const string OpenAIEndpoint = "https://api.openai.com/v1/chat/completions"; // Điều này có thể thay đổi theo phiên bản API của bạn

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetChatResponse(string input)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OpenAIApiKey);

                    var requestData = new
                    {
                        prompt = input,
                        max_tokens = 50 // Số lượng tokens tối đa trong câu trả lời
                    };

                    var response = await httpClient.PostAsJsonAsync(OpenAIEndpoint, requestData);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsAsync<dynamic>();
                        var chatResponse = result.choices[0].text;
                        return Json(new { success = true, response = chatResponse });
                    }
                    else
                    {
                        return Json(new { success = false, response = "Error: Unable to fetch response from ChatGPT" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, response = $"Error: {ex.Message}" });
            }
        }
    }
}