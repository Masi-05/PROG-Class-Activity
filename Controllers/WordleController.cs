using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleWordle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordleController : Controller
    {
        private readonly HttpClient _client;

        public WordleController(HttpClient client)
        {
            _client = client;
        }

        // GET: Word
        public async Task<ActionResult> Index()
        {
            string apiUrl = "http://localhost:58764/api/words"; 

            try
            {
                var response = await _client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var words = JsonConvert.DeserializeObject<List<string>>(json); 

                    if (words != null && words.Count > 0)
                    {
                        Random random = new Random();
                        var randomWord = words[random.Next(words.Count)];

                        
                        ViewData["RandomWord"] = randomWord;
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "No words found from API.";
                    }
                }
                else
                {
                    ViewData["ErrorMessage"] = "Failed to retrieve words from API.";
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Error: {ex.Message}";
            }

            return View();
        }
    }
}
