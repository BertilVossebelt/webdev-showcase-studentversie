using Microsoft.AspNetCore.Mvc;
using Showcase_Contactpagina.Models;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Showcase_Contactpagina.Controllers
{
    public class ContactController : Controller
    {
        private readonly HttpClient _httpClient;

        public ContactController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7070");
        }

        // GET: ContactController
        public ActionResult Index()
        {
            return View();
        }

        // POST: ContactController
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Contactform form)
        {
            const string gRecaptchaSecret = "6LcYW98qAAAAAOTbh396YvCAP_wPW5Gq0Yex3bpf";
            
            var values = new Dictionary<string, string>
            {
                { "secret", gRecaptchaSecret },
                { "response", form.gRecaptchaResponse }
            };
            
            var urlEncodedContent = new FormUrlEncodedContent(values);
            var response = await _httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", urlEncodedContent);
            Console.WriteLine($"reCAPTCHA response: {form.gRecaptchaResponse}");

            var jsonResponse = await response.Content.ReadAsStringAsync();
            bool success = (bool)JObject.Parse(jsonResponse)["success"];
            Console.WriteLine(JObject.Parse(jsonResponse));
            if (!success) {return BadRequest(new { message = "reCAPTCHA verification failed." });}
            
            
            if (!ModelState.IsValid)
            const string namePattern = "^[a-zA-Z ]*$"; // Only allow letters and spaces.
            Regex nameRegex = new(namePattern);
            
            // Check name fields with regex, e-mail, phone and length are validated by the model.
            if (!nameRegex.IsMatch(form.FirstName) || !nameRegex.IsMatch(form.LastName) || !ModelState.IsValid)
            {
                ViewBag.Message = "De ingevulde naam voldoet niet aan de gestelde voorwaarden";
                return BadRequest();
            }
            
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var json = JsonConvert.SerializeObject(form, settings);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            response = await _httpClient.PostAsync("/api/Mail", stringContent);

            return !response.IsSuccessStatusCode ? StatusCode(StatusCodes.Status500InternalServerError) : Ok();
        }
    }
}