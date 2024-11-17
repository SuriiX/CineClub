using apiCineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiCineClub.Clases
{
    public class clsOpePelicula
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();

            public tblPelicula tblPeli { get; set; }

        // Método para listar todas las películas
        public IQueryable listarPelicula()
        {
            try
            {
                var peliculas = from tP in oCCE.Set<tblPelicula>()
                                join tTP in oCCE.Set<tblProductora>() on tP.Id_Productora equals tTP.Id_Productora
                                join tTps in oCCE.Set<tblPai>() on tP.Id_Pais equals tTps.Id_Pais
                                join tD in oCCE.Set<tblDirector>() on tP.Id_Director equals tD.Id_Director
                                join tE in oCCE.Set<tblEmpleado>() on tP.Id_Empleado equals tE.Id_Empleado
                                orderby tP.Id_Pelicula
                                select new
                                {
                                    Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                    Codigo = tP.Id_Pelicula,
                                    Nombre = tP.Nombre,
                                    FechaEstreno = tP.Fecha_Estreno,
                                    idProductora = tTP.Id_Productora,
                                    Productora = tTP.Nombre,
                                    idPais = tTps.Id_Pais,
                                    Nacionalidad = tTps.Nombre,
                                    idDirector = tD.Id_Director,
                                    Director = tD.Nombre,
                                    Empleado = tE.Nombre,
                                    idPDirector = tD.Id_Pais,
                                    idGDirector = tD.Id_Genero,
                                    idEDirector = tD.Id_Empleado,
                                    idTDDirector = tD.Id_TipoDoc
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
                               join tTP in oCCE.Set<tblProductora>() on tP.Id_Productora equals tTP.Id_Productora
                               join tTps in oCCE.Set<tblPai>() on tP.Id_Pais equals tTps.Id_Pais
                               join tD in oCCE.Set<tblDirector>() on tP.Id_Director equals tD.Id_Director
                               join tE in oCCE.Set<tblEmpleado>() on tP.Id_Empleado equals tE.Id_Empleado
                               where tP.Id_Pelicula == id
                               orderby tP.Id_Pelicula
                               select new
                               {
                                   Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                   Codigo = tP.Id_Pelicula,
                                   Nombre = tP.Nombre,
                                   FechaEstreno = tP.Fecha_Estreno,
                                   idProductora = tTP.Id_Productora,
                                   Productora = tTP.Nombre,
                                   idPais = tTps.Id_Pais,
                                   Nacionalidad = tTps.Nombre,
                                   idDirector = tD.Id_Director,
                                   Director = tD.Nombre,
                                   Empleado = tE.Nombre,
                                   idPDirector = tD.Id_Pais,
                                   idGDirector = tD.Id_Genero,
                                   idEDirector = tD.Id_Empleado,
                                   idTDDirector = tD.Id_TipoDoc
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
    
