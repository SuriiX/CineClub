using apiCineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiCineClub.Clases
{
    public class clsOpeDirector
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();

        public tblDirector tblDirec { get; set; }

        // Método para listar todas las películas
        public IQueryable listarDirector()
        {
            try
            {
                var peliculas = from tD in oCCE.Set<tblDirector>()
                                join tP in oCCE.Set<tblPai>()
                                on tD.Id_Pais equals tP.Codigo
                                join tE in oCCE.Set<tblEmpleado>()
                                on tD.Id_Empleado equals tE.Codigo
                                join tG in oCCE.Set<tblGenero>()
                                on tD.Id_Genero equals tG.Codigo
                                join tTd in oCCE.Set<tblTipoDoc>()
                                on tD.Id_TipoDoc equals tTd.Codigo
                                orderby tD.Codigo
                                select new
                                {
                                    Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                    Codigo = tD.Codigo,
                                    Nombre = tD.Nombre,
                                    Pais = tP.Nombre,
                                    Empleado = tE.Nombre,
                                    Genero = tG.Nombre,
                                    TipoDoc= tTd.Nombre
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