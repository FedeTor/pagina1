using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace PaginaInglés.Models
{
    public class EnvioMail
    {
        public string Name { get; set; }     
        public string From { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }        
        public string Body { get; set; }
        //public List<IFormFile> Attachments { get; set; } //esto es por si hay archivos para adjuntar
    }

    public class RequestEnvioMail
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
    }
}
