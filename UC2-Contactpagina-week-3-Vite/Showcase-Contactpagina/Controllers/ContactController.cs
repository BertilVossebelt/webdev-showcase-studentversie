using Microsoft.AspNetCore.Mvc;
using Showcase_Contactpagina.Models;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
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
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            // Send the form data to the API.
            var response = await _httpClient.PostAsync("/api/Mail", content);

            return !response.IsSuccessStatusCode ? StatusCode(StatusCodes.Status500InternalServerError) : Ok();
        }
    }
}