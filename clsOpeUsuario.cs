using apiCineClub.Models;
using System;
using System.Linq;

namespace apiCineClub.Clases
{
    public class clsOpeUsuario
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();

        public int Validar(string user, string contra)
        {
            try
            {
                var usuario = oCCE.tblUsuarios.FirstOrDefault(u => u.Clave == user);
                if (usuario != null && usuario.Contrasena == contra)
                {
                    return 0; // Usuario válido
                }
                return 1; // Usuario o contraseña incorrectos
            }
            catch (Exception ex)
            {
                // Error interno del servidor
                return -1;
            }
        }
    }
}
