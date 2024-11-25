using apiCineClub.Clases;
using apiCineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
namespace apiCineClub.Controllers
{
    [EnableCors(origins: "http://localhost:52006", headers: "*", methods: "*")]
    public class UsuarioController : ApiController
    {
        [HttpPost]
        public int Login([FromBody] LoginRequest datos)
        {
            if (datos == null || string.IsNullOrWhiteSpace(datos.Clave) || string.IsNullOrWhiteSpace(datos.Contrasena))
            {
                Console.WriteLine("Error: Datos de entrada incompletos.");
                return 1; // Usuario o contraseña incorrectos
            }

            try
            {
                clsOpeUsuario operativa = new clsOpeUsuario();
                int resultado = operativa.Validar(datos.Clave, datos.Contrasena);

                if (resultado == 1)
                {
                    Console.WriteLine("Error: Usuario o contraseña incorrectos.");
                }
                else if (resultado == -1)
                {
                    Console.WriteLine("Error: Error interno al procesar la solicitud.");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado en el controlador: {ex.Message}");
                return -1; // Error interno del servidor
            }
        }

    }

    public class LoginRequest
    {
        public string Clave { get; set; }
        public string Contrasena { get; set; }
    }
}
