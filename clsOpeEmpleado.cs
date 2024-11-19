using apiCineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiCineClub.Clases
{
    public class clsOpeEmpleado
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();

        public tblEmpleado tblEmple { get; set; }

        // Método para listar todas las películas
        public IQueryable listarEmpleado()
        {
            try
            {
                var empleados = from te in oCCE.Set<tblEmpleado>()
                                join tG in oCCE.Set<tblGenero>()
                                on te.Id_Genero equals tG.Codigo
                                join tTd in oCCE.Set<tblTipoDoc>()
                                on te.Id_TipoDoc equals tTd.Codigo
                                orderby te.Codigo
                                select new
                                {
                                    Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                    Codigo = te.Codigo,
                                    Nombre = te.Nombre,
                                    Genero = tG.Nombre,
                                    TipoDoc = tTd.Nombre
                                };

                return empleados; // Retorna el IQueryable directamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las peliculas", ex);
            }
        }
    }
}