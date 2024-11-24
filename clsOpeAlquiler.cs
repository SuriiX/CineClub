using apiCineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiCineClub.Clases
{
    public class clsOpeAlquiler
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();

        public tblAlquiler tblAlquil { get; set; }

        // Método para listar todas las películas
        public IQueryable listarAlquilereses()
        {
            try
            {
                var Alquileres_dos = from tA in oCCE.Set<tblAlquiler>()
                                     join tDA in oCCE.Set<tblDetAlquiler>()
                                     on tA.Codigo equals tDA.Id_Alquiler
                                     orderby tA.Codigo
                                     select new
                                     {
                                         Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                         Codigo = tDA.Codigo,
                                     };
                                     
                return Alquileres_dos; // Retorna el IQueryable directamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los alquileres", ex);
            }
        }

        // Método para listar una película por su código
        public IQueryable listarAlquilerXCod(int id)
        {
            try
            {
                var Alquileres_dos = from tA in oCCE.Set<tblAlquiler>()
                                     join tDA in oCCE.Set<tblDetAlquiler>()
                                     on tA.Codigo equals tDA.Id_Alquiler
                                     orderby tA.Codigo
                                     select new
                                     {
                                         Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                         Codigo = tDA.Codigo,
                                     };

                return Alquileres_dos; // Retorna el IQueryable directamente
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

                return $"Error, Hubo un fallo al grabar en el registro con Id: {tblAlquil.Codigo} ";
            }

            tblAlquil.Codigo = idmax;
            try
            {
                oCCE.Alquilereses.Add(tblAlquil);
                oCCE.SaveChanges();
                return $"Registro grabado con éxito con Id: {tblAlquil.Codigo} ";

            }
            catch
            {
                return $"Error, hubo fallo al grabar el registro con Id: {tblAlquil.Codigo} ";

            }


        }
        public string Modificar()
        {
            try
            {
                tblDetAlquiler tbAlquil = oCCE.tblAlquilereses.FirstOrDefault(s => s.Codigo == tblAlquil.Codigo);

                // Si no se encuentra el registro, devolver un mensaje de error
                if (tbAlquil == null)
                {
                    return $"No se encontró la película con ID: {tblAlquil.Codigo}";
                }

                // Actualizar los campos con los nuevos valores
                tbAlquil.Codigo = tblAlquil.Codigo;


                // Guardar los cambios en la base de datos
                oCCE.SaveChanges();

                return $"Se actualizó el registro de el Alquiler con ID: {tbAlquil.Codigo}";

            }
            catch (Exception ex)
            {
                // Retornar el mensaje de error con más detalles sobre el fallo
                return $"Error, hubo un fallo al actualizar el registro de la película con ID: {tblAlquil.Codigo}. Detalles: {ex.Message}";
            }
        }
    }
}

