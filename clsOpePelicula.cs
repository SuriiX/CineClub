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
                                on tP.Id_Productora equals tTP.Codigo
                                join tTps in oCCE.Set<tblPai>()
                                on tP.Id_Pais equals tTps.Codigo
                                join tD in oCCE.Set<tblDirector>()
                                on tP.Id_Director equals tD.Codigo
                                join tE in oCCE.Set<tblEmpleado>()
                                on tP.Id_Empleado equals tE.Codigo
                                orderby tP.Codigo
                                select new
                                {
                                    Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                    Codigo = tP.Codigo,
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

        public string Agregar()
        {
            try
            {
                // Obtener el próximo ID (si es necesario)
                var idmax = oCCE.tblPeliculas.DefaultIfEmpty().Max(r => r == null ? 1 : r.Codigo + 1);
                tblPeli.Codigo = idmax;

                // Insertar el registro
                oCCE.tblPeliculas.Add(tblPeli);
                oCCE.SaveChanges();

                return $"Registro grabado con éxito: {tblPeli.Nombre}, con ID: {tblPeli.Codigo}";
            }
            catch (Exception ex)
            {
                return $"Error al grabar el registro: {ex.Message}";
            }
        }
        public string Modificar()
        {
            try
            {
                var tbPeli = oCCE.tblPeliculas.FirstOrDefault(s => s.Codigo == tblPeli.Codigo);

                // Si no se encuentra el registro, devolver un mensaje de error
                if (tbPeli == null)
                {
                    return $"No se encontró la película con ID: {tblPeli.Codigo}";
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

                return $"Se actualizó el registro de la película con ID: {tbPeli.Codigo}";

            }
            catch (Exception ex)
            {
                // Retornar el mensaje de error con más detalles sobre el fallo
                return $"Error, hubo un fallo al actualizar el registro de la película con ID: {tblPeli.Codigo}. Detalles: {ex.Message}";
            }
        }
        public string Eliminar(int id)
        {
            try
            {
                // Buscar el registro en la base de datos por el código
                var pelicula = oCCE.tblPeliculas.FirstOrDefault(p => p.Codigo == id);
                if (pelicula == null)
                {
                    return $"Error: No se encontró el registro con Código {id}.";
                }

                // Eliminar el registro
                oCCE.tblPeliculas.Remove(pelicula);
                oCCE.SaveChanges();
                return $"Registro con Código {id} eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al intentar eliminar el registro con Código {id}: {ex.Message}";
            }
        }
    }
}
    
