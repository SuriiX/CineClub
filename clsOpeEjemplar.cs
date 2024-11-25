using apiCineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiCineClub.Clases
{
    public class clsOpeEjemplar
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();

        public tblPeliculaEjemplar tblEjemp { get; set; }

        // Método para listar todas las películas
        public IQueryable listarEjemplar()
        {
            try
            {
                var peliculas = from tP in oCCE.Set<tblPelicula>()
                                join tPE in oCCE.Set<tblPeliculaEjemplar>()
                                on tP.Codigo equals tPE.Id_Pelicula
                                orderby tPE.Codigo
                                select new
                                {
                                    Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                    Codigo = tPE.Codigo,
                                    Nombre = tP.Nombre

                                };

                return peliculas; // Retorna el IQueryable directamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las peliculas", ex);
            }
        }

        // Método para listar una película por su código
        public IQueryable listarPeliculaXCod(int id)
        {
            try
            {
                var pelicula = from tP in oCCE.Set<tblPelicula>()
                               join tTP in oCCE.Set<tblProductora>()
                               on tP.Id_Productora equals tTP.Codigo
                               join tTps in oCCE.Set<tblPai>()
                               on tP.Id_Pais equals tTps.Codigo
                               join tD in oCCE.Set<tblDirector>()
                               on tP.Id_Director equals tD.Codigo
                               join tE in oCCE.Set<tblEmpleado>()
                               on tP.Id_Empleado equals tE.Codigo

                               where tP.Codigo == id

                               orderby tP.Codigo
                               select new
                               {
                                   Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                   Codigo = tP.Codigo,
                                   Nombre = tP.Nombre,
                                   FechaEstreno = tP.Fecha_Estreno,
                                   Id_Productora = tTP.Codigo,
                                   Productora = tTP.Nombre,
                                   Id_Nacionalidad = tTps.Codigo,
                                   Nacionalidad = tTps.Nombre,
                                   Id_Director = tD.Codigo,
                                   Director = tD.Nombre,
                                   Id_Empleado = tE.Codigo,
                                   Empleado = tE.Nombre,
                               };

                return pelicula; // Retorna el IQueryable directamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la pelicula", ex);
            }
        }
    }
}
