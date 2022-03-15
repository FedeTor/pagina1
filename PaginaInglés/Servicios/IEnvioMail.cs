using PaginaInglés.Models;
using System.Threading.Tasks;

namespace PaginaInglés.Servicios
{
    public interface IEnvioMail
    {
        Task SendEmailAsync(EnvioMail mailRequest);
    }
}
