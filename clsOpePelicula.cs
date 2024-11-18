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
                                join tTP in oCCE.Set<tblProductora>()
                                on tP.Id_Productora equals tTP.Id_Productora
                                join tTps in oCCE.Set<tblPai>()
                                on tP.Id_Pais equals tTps.Id_Pais
                                join tD in oCCE.Set<tblDirector>()
                                on tP.Id_Director equals tD.Id_Director
                                join tE in oCCE.Set<tblEmpleado>()
                                on tP.Id_Empleado equals tE.Id_Empleado
                                orderby tP.Id_Pelicula
                                select new
                                {
                                    Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                    Codigo = tP.Id_Pelicula,
                                    Nombre = tP.Nombre,
                                    FechaEstreno = tP.Fecha_Estreno,
                                    Productora = tTP.Nombre,
                                    Nacionalidad = tTps.Nombre,
                                    Director = tD.Nombre,
                                    Empleado = tE.Nombre,
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
                               on tP.Id_Productora equals tTP.Id_Productora
                               join tTps in oCCE.Set<tblPai>()
                               on tP.Id_Pais equals tTps.Id_Pais
                               join tD in oCCE.Set<tblDirector>()
                               on tP.Id_Director equals tD.Id_Director
                               join tE in oCCE.Set<tblEmpleado>()
                               on tP.Id_Empleado equals tE.Id_Empleado

                               where tP.Id_Pelicula == id

                               orderby tP.Id_Pelicula
                               select new
                               {
                                   Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                   Codigo = tP.Id_Pelicula,
                                   Nombre = tP.Nombre,
                                   FechaEstreno = tP.Fecha_Estreno,
                                   Productora = tTP.Nombre,
                                   Nacionalidad = tTps.Nombre,
                                   Director = tD.Nombre,
                                   Empleado = tE.Nombre,
                               };

                return pelicula; // Retorna el IQueryable directamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la pelicula", ex);
            }
        }

        public string Agregar()
        {

            var idmax = 0;
            try
            {
                idmax = oCCE.tblPeliculas.DefaultIfEmpty().Max(r => r == null ? 1 : r.Id_Pelicula + 1);
            }
            catch
            {

                return $"Error, Hubo un fallo al grabar en el registro: {tblPeli.Nombre}, con Id: {tblPeli.Id_Pelicula} ";
            }

            tblPeli.Id_Pelicula = idmax;
            try
            {
                oCCE.tblPeliculas.Add(tblPeli);
                oCCE.SaveChanges();
                return $"Registro grabado con éxito: {tblPeli.Nombre} , con Id: {tblPeli.Id_Pelicula} ";

            }
            catch
            {
                return $"Error, hubo fallo al grabar el registro: {tblPeli.Nombre} , con Id: {tblPeli.Id_Pelicula} ";

            }


        }
        public string Modificar()
        {
            try
            {
                var tbPeli = oCCE.tblPeliculas.FirstOrDefault(s => s.Id_Pelicula == tblPeli.Id_Pelicula);

                // Si no se encuentra el registro, devolver un mensaje de error
                if (tbPeli == null)
                {
                    return $"No se encontró la película con ID: {tblPeli.Id_Pelicula}";
                }

                // Actualizar los campos con los nuevos valores
                tbPeli.Nombre = tblPeli.Nombre;
                tbPeli.Fecha_Estreno = tblPeli.Fecha_Estreno;
                tbPeli.Id_Productora = tblPeli.Id_Productora;
                tbPeli.Id_Pais = tblPeli.Id_Pais;
                tbPeli.Id_Director = tblPeli.Id_Director;
                tbPeli.Id_Empleado = tblPeli.Id_Empleado;

                // Guardar los cambios en la base de datos
                oCCE.SaveChanges();

                return $"Se actualizó el registro de la película con ID: {tbPeli.Id_Pelicula}";

            }
            catch (Exception ex)
            {
                // Retornar el mensaje de error con más detalles sobre el fallo
                return $"Error, hubo un fallo al actualizar el registro de la película con ID: {tblPeli.Id_Pelicula}. Detalles: {ex.Message}";
            }
        }
    }
}
    
