using apiCineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiCineClub.Clases
{
    public class clsOpeProductora
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();

        public tblProductora tblProduc { get; set; }

        // Método para listar todas las películas
        public IQueryable listarProductora()
        {
            try
            {
                var peliculas = from tP in oCCE.Set<tblProductora>()
                                join tE in oCCE.Set<tblEmpleado>()
                                on tP.Id_Empleado equals tE.Codigo
                                orderby tP.Codigo
                                select new
                                {
                                    Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                    Codigo = tP.Codigo,
                                    Nombre = tP.Nombre,
                                    Empleado = tE.Nombre,
                                };

                return peliculas; // Retorna el IQueryable directamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las peliculas", ex);
            }
        }
   
    }
}