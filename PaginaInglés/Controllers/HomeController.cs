using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaginaInglés.Models;
using PaginaInglés.Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaInglés.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEnvioMail _enviomail;   

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IEnvioMail enviomail)
        {
            _logger = logger;
            _enviomail = enviomail;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("EnvioMail")]
        public async Task<IActionResult> Send([FromForm] RequestEnvioMail request)
        {
            var verdadero = true;
            try
            {
                var envio = new EnvioMail()
                {
                    //Body = request.message,
                    //Subject = request.subject,
                    //Email = request.email,
                    //Body = request.message + " " + request.phone + request.name,
                    From = request.email,
                    Body = $"Nombre: {request.name}, Email: {request.email}, Teléfono: {request.phone}, Mensaje: {request.message}",
                    Subject = request.subject,                    
                    ToEmail = "fedeetorancio@gmail.com"         
                };
                await _enviomail.SendEmailAsync(envio);
                TempData["ok"] = verdadero;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                verdadero = false;
                TempData["ok"] = verdadero;
                throw ex;
            }
        }
    }
}
