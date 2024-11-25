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
        public IQueryable listarAlquiler()
        {
            try
            {
                var Alquileres_dos = from tA in oCCE.Set<tblAlquiler>()
                                     join tDA in oCCE.Set<tblDetAlquiler>()
                                     on tA.Codigo equals tDA.Id_Alquiler
                                     join tC in oCCE.Set<tblCliente>()
                                     on tA.Id_Cliente equals tC.Codigo
                                     orderby tA.Codigo
                                     select new
                                     {
                                         Editar = $"<a class='btn btn-info btn-sm' href='#'><i class='fas fa-pencil-alt'></i> Editar</a>",
                                         Codigo = tDA.Codigo,
                                         Nombre = tC.Nombre,
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
        }    }
}