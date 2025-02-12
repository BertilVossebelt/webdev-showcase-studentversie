using Microsoft.AspNetCore.Mvc;
using ShowcaseAPI.Models;
using System.Net;
using System.Net.Mail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowcaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        // POST api/Mail  [Bind("FirstName, LastName, Email, Phone")]
        [HttpPost]
        public ActionResult Post([Bind("FirstName, LastName, Email, Phone")] Contactform form)
        {
            Console.WriteLine("Received contact request");
            var body = $"Naam: {form.FirstName} {form.LastName}\nEmail: {form.Email}\nTelefoonnummer: {form.Phone}\n";
            
            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("4bfb1c47bc9419", "c059a42e87c42a"),
                EnableSsl = true
            };
            client.Send(form.Email, "myemail@example.com", "Contactverzoek", body);

            return Ok();
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("ping");
        }
    }
}