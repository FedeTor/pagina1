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
        private readonly IEnvioMail _enviomail;   // inyeccion de dependecia de la interface IEnvioMail // _enviomail es un variable

        private readonly ILogger<HomeController> _logger;

        //el constructor va de la linea 19-23
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
        public async Task<IActionResult> Send(RequestEnvioMail request) // [FromForm] RequestEnvioMail request lo q trae del formulario
        {
            var verdadero = true;
            try
            {
                var envio = new EnvioMail()
                {
                    //Body = request.message + " " + request.phone + request.name,
                    Name = request.name,
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
                TempData["ok"] = null;
                throw ex;
            }
        }
    }
}
