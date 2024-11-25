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
                // Verificar si hay más de 4 registros para el mismo ejemplar
                int conteo = oCCE.tblDetAlquilers.Count(a => a.Id_PeliculaEjem == tblDetAlqui.Id_PeliculaEjem);
                if (conteo >= 4)
                {
                    return $"No se puede agregar el registro. Solo hay disponibilidad de 4 ejemplares para esta película.";
                }

                // Obtener el siguiente ID disponible
                var idmax = oCCE.tblDetAlquilers.DefaultIfEmpty().Max(r => r == null ? 1 : r.Codigo + 1);
                tblDetAlqui.Codigo = idmax;

                // Verificar penalización
                var penalizacionInfo = oCCE.tblPenalizacions
                    .Where(p => p.Id_DetAlquiler == tblDetAlqui.Codigo)
                    .Select(p => new { p.Vlr_Penalizacion, p.Motivo })
                    .FirstOrDefault();

                if (penalizacionInfo != null && penalizacionInfo.Vlr_Penalizacion > 0)
                {
                    tblDetAlqui.Vlr_Alquiler += penalizacionInfo.Vlr_Penalizacion;

                    return $"Penalización aplicada de {penalizacionInfo.Vlr_Penalizacion}. " +
                           $"Motivo: {penalizacionInfo.Motivo}. " +
                           $"Total a pagar: {tblDetAlqui.Vlr_Alquiler}";
                }

                // Agregar el registro
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
                // Validar que no existan más de 4 registros para el ejemplar
                int conteo = oCCE.tblDetAlquilers.Count(a => a.Id_PeliculaEjem == tblDetAlqui.Id_PeliculaEjem && a.Codigo != tblDetAlqui.Codigo);
                if (conteo >= 4)
                {
                    return $"No se puede modificar el registro. Solo hay disponibilidad de 4 ejemplares para esta película.";
                }

                var tbDetA = oCCE.tblDetAlquilers.FirstOrDefault(s => s.Codigo == tblDetAlqui.Codigo);

                if (tbDetA == null)
                {
                    return $"No se encontró el alquiler con ID: {tblDetAlqui.Codigo}";
                }

                // Verificar penalización
                var penalizacionInfo = oCCE.tblPenalizacions
                    .Where(p => p.Id_DetAlquiler == tblDetAlqui.Codigo)
                    .Select(p => new { p.Vlr_Penalizacion, p.Motivo })
                    .FirstOrDefault();

                if (penalizacionInfo != null && penalizacionInfo.Vlr_Penalizacion > 0)
                {
                    tblDetAlqui.Vlr_Alquiler += penalizacionInfo.Vlr_Penalizacion;

                    tbDetA.Cantidad = tblDetAlqui.Cantidad;
                    tbDetA.Fecha_Inicio = tblDetAlqui.Fecha_Inicio;
                    tbDetA.Fecha_Fin = tblDetAlqui.Fecha_Fin;
                    tbDetA.Vlr_Alquiler = tblDetAlqui.Vlr_Alquiler;
                    tbDetA.Id_PeliculaEjem = tblDetAlqui.Id_PeliculaEjem;
                    tbDetA.Id_Alquiler = tblDetAlqui.Id_Alquiler;

                    oCCE.SaveChanges();

                    return $"Penalización aplicada de {penalizacionInfo.Vlr_Penalizacion}. " +
                           $"Motivo: {penalizacionInfo.Motivo}. " +
                           $"Total a pagar: {tblDetAlqui.Vlr_Alquiler}";
                }

                tbDetA.Cantidad = tblDetAlqui.Cantidad;
                tbDetA.Fecha_Inicio = tblDetAlqui.Fecha_Inicio;
                tbDetA.Fecha_Fin = tblDetAlqui.Fecha_Fin;
                tbDetA.Vlr_Alquiler = tblDetAlqui.Vlr_Alquiler;
                tbDetA.Id_PeliculaEjem = tblDetAlqui.Id_PeliculaEjem;
                tbDetA.Id_Alquiler = tblDetAlqui.Id_Alquiler;

                oCCE.SaveChanges();

                return $"Se actualizó el registro de alquiler con ID: {tbDetA.Codigo}";
            }
            catch (Exception ex)
            {
                return $"Error al actualizar el registro: {ex.Message}";
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
