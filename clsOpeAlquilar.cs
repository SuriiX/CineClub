using apiCineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiCineClub.Clases
{
    public class clsOpeAlquilar
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();

        public tblDetAlquiler tblDetAlqui { get; set; }

        // Método para listar todas las películas
        public IQueryable listarDetAlquiler()
        {
            try
            {
                var alquilar = from tDe in oCCE.Set<tblDetAlquiler>()
                                join tE in oCCE.Set<tblPeliculaEjemplar>()
                                on tDe.Id_PeliculaEjem equals tE.Codigo
                                join tA in oCCE.Set<tblAlquiler>()
                                on tDe.Id_Alquiler equals tA.Codigo
                                join tP in oCCE.Set<tblPelicula >()
                                on tE.Id_Pelicula equals tP.Codigo
                                join tC in oCCE.Set<tblCliente>()
                                on tA.Id_Cliente equals tC.Codigo

                               orderby tDe.Codigo
                                select new
                                {
                                    Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                    Codigo = tDe.Codigo,
                                    Cantidad = tDe.Cantidad,
                                    FechaInicio = tDe.Fecha_Inicio,
                                    FechaFinal = tDe.Fecha_Fin,
                                    ValorAlquiler = tDe.Vlr_Alquiler,
                                    PeliculaEjemplar = tP.Nombre,
                                    Cliente =tC.Nombre
                                };

                return alquilar; // Retorna el IQueryable directamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las peliculas", ex);
            }
        }

        // Método para listar una película por su código
        public IQueryable listarDetAlquilarXCod(int id)
        {
            try
            {
                var alquilar = from tDe in oCCE.Set<tblDetAlquiler>()
                               join tE in oCCE.Set<tblPeliculaEjemplar>()
                               on tDe.Id_PeliculaEjem equals tE.Codigo
                               join tA in oCCE.Set<tblAlquiler>()
                               on tDe.Id_Alquiler equals tA.Codigo
                               join tP in oCCE.Set<tblPelicula>()
                               on tE.Id_Pelicula equals tP.Codigo
                               where tDe.Codigo==id
                               orderby tDe.Codigo
                               select new
                               {
                                   Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                   Codigo = tDe.Codigo,
                                   Cantidad = tDe.Cantidad,
                                   FechaInicio = tDe.Fecha_Inicio,
                                   FechaFinal = tDe.Fecha_Fin,
                                   ValorAlquiler = tDe.Vlr_Alquiler,
                                   Id_Ejemplar = tE.Codigo,
                                   Id_Alquiler = tA.Codigo,
                               };

                return alquilar; // Retorna el IQueryable directamente
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
                var idmax = oCCE.tblDetAlquilers.DefaultIfEmpty().Max(r => r == null ? 1 : r.Codigo + 1);
                tblDetAlqui.Codigo = idmax;

                // Insertar el registro
                oCCE.tblDetAlquilers.Add(tblDetAlqui);
                oCCE.SaveChanges();

                return $"Registro grabado con éxito: con ID: {tblDetAlqui.Codigo}";
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
                var tbDetA = oCCE.tblDetAlquilers.FirstOrDefault(s => s.Codigo == tblDetAlqui.Codigo);

                // Si no se encuentra el registro, devolver un mensaje de error
                if (tbDetA == null)
                {
                    return $"No se encontró la película con ID: {tblDetAlqui.Codigo}";
                }

                // Actualizar los campos con los nuevos valores
                tbDetA.Codigo = tblDetAlqui.Codigo;
                tbDetA.Cantidad = tblDetAlqui.Cantidad;
                tbDetA.Fecha_Inicio = tblDetAlqui.Fecha_Inicio;
                tbDetA.Fecha_Fin = tblDetAlqui.Fecha_Fin;
                tbDetA.Vlr_Alquiler = tblDetAlqui.Vlr_Alquiler ;
                tbDetA.Id_PeliculaEjem = tblDetAlqui.Id_PeliculaEjem;
                tbDetA.Id_Alquiler = tblDetAlqui.Id_Alquiler;

                // Guardar los cambios en la base de datos
                oCCE.SaveChanges();

                return $"Se actualizó el registro de la película con ID: {tbDetA.Codigo}";

            }
            catch (Exception ex)
            {
                // Retornar el mensaje de error con más detalles sobre el fallo
                return $"Error, hubo un fallo al actualizar el registro de la película con ID: {tblDetAlqui.Codigo}. Detalles: {ex.Message}";
            }
        }
        public string Eliminar(int id)
        {
            try
            {
                // Buscar el registro en la base de datos por el código
                var detAlquiler = oCCE.tblDetAlquilers.FirstOrDefault(p => p.Codigo == id);
                if (detAlquiler == null)
                {
                    return $"Error: No se encontró el registro con Código {id}.";
                }

                // Eliminar el registro
                oCCE.tblDetAlquilers.Remove(detAlquiler);
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
