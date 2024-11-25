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

        public tblDetAlquiler tblAlqui { get; set; }

        // Método para listar todas las películas
        public IQueryable listarAlquileres()
        {
            try
            {
                var Alquileres = from tDA in oCCE.Set<tblDetAlquiler>()

                                 join tPE in oCCE.Set<tblPeliculaEjemplar>()
                                 on tDA.Codigo equals tPE.Id_DetAlquiler
                                 join tA in oCCE.Set<tblAlquiler>()
                                 on tDA.Id_Alquiler equals tA.Codigo
                                 join Pe in oCCE.Set<tblPelicula>()
                                 on tPE.Id_Pelicula equals Pe.Codigo
                                 orderby tDA.Codigo
                                 select new
                                 {
                                     Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                     Codigo = tDA.Codigo,
                                     Cantidad = tDA.Cantidad,
                                     Fecha_Inicio = tDA.Fecha_Inicio,
                                     Fecha_Fin = tDA.Fecha_Fin,
                                     Vlr_Alquiler = tDA.Vlr_Alquiler,
                                     Id_Alquiler = tDA.Id_Alquiler,
                                     Id_Ejemplar = tPE.Codigo,
                                     Nombre = Pe.Nombre,

                                 };

                return Alquileres; // Retorna el IQueryable directamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los Alquileres", ex);
            }
        }

        // Método para listar una película por su código
        public IQueryable listarAlquilerXCod(int id)
        {
            try
            {

                var Alquileres = from tDA in oCCE.Set<tblDetAlquiler>()
                                 join tPE in oCCE.Set<tblPeliculaEjemplar>()
                                 on tDA.Codigo equals tPE.Id_DetAlquiler
                                 join tA in oCCE.Set<tblAlquiler>()
                                 on tDA.Id_Alquiler equals tA.Codigo
                                 join Pe in oCCE.Set<tblPelicula>()
                                 on tPE.Id_Pelicula equals Pe.Codigo
                                 orderby tDA.Codigo
                                 select new
                                 {
                                     Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                     Codigo = tDA.Codigo,
                                     Cantidad = tDA.Cantidad,
                                     Fecha_Inicio = tDA.Fecha_Inicio,
                                     Fecha_Fin = tDA.Fecha_Fin,
                                     Vlr_Alquiler = tDA.Vlr_Alquiler,
                                     Id_Alquiler = tDA.Id_Alquiler,
                                     Nombre = Pe.Nombre,
                                     Id_Ejemplar = tPE.Codigo,
                                 };

                return Alquileres; // Retorna el IQueryable directamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el alquiler", ex);
            }
        }

        public string Agregar()
        {

            var idmax = 0;
            try
            {
                idmax = oCCE.tblPeliculas.DefaultIfEmpty().Max(r => r == null ? 1 : r.Codigo + 1);
            }
            catch
            {

                return $"Error, Hubo un fallo al grabar en el registro: {tblAlqui.Nombre}, con Id: {tblAlqui.Codigo} ";
            }

            tblAlqui.Codigo = idmax;
            try
            {
                oCCE.tblAlquilers.Add(tblAlqui);
                oCCE.SaveChanges();
                return $"Registro grabado con éxito: {tblAlqui.Nombre} , con Id: {tblAlqui.Codigo} ";

            }
            catch
            {
                return $"Error, hubo fallo al grabar el registro: {tblAlqui.Nombre} , con Id: {tblAlqui.Codigo} ";

            }


        }
        public string Modificar()
        {
            try
            {
                tblDetAlquiler tbAlqui = oCCE.tblAlquilers.FirstOrDefault(s => s.Codigo == tblAlqui.Codigo);

                // Si no se encuentra el registro, devolver un mensaje de error
                if (tbAlqui == null)
                {
                    return $"No se encontró la película con ID: {tblAlqui.Codigo}";
                }

                // Actualizar los campos con los nuevos valores
                tbAlqui.Codigo = tblAlqui.Codigo;
                tbAlqui.Cantidad = tblAlqui.Cantidad;
                tbAlqui.Fecha_Inicio = tblAlqui.Fecha_Inicio;
                tbAlqui.Fecha_Fin = tblAlqui.Fecha_Fin;
                tbAlqui.Vlr_Alquiler = tblAlqui.Vlr_Alquiler;
                tbAlqui.Id_Alquiler = tblAlqui.Id_Alquiler;


                // Guardar los cambios en la base de datos
                oCCE.SaveChanges();

                return $"Se actualizó el registro de el Alquiler con ID: {tbAlqui.Codigo}";

            }
            catch (Exception ex)
            {
                // Retornar el mensaje de error con más detalles sobre el fallo
                return $"Error, hubo un fallo al actualizar el registro de la película con ID: {tblAlqui.Codigo}. Detalles: {ex.Message}";
            }
        }
    }
}