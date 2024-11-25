using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using apiCineClub.Models;
using apiCineClub.Clases;

namespace apiCineClub.Controllers
{
    [EnableCors(origins: "http://localhost:52006", headers: "*", methods: "*")]
    public class alquilarController : ApiController
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();

        // GET api/alquilar
        public IQueryable Get(int id, int comando)
        {
            clsOpeAlquilar opeAlqui = new clsOpeAlquilar();
            IQueryable resultado;

            if (comando == 1)
            {
                resultado = opeAlqui.listarAlquileres();
            }
            else
            {
                // Asegúrate de pasar el parámetro correctamente
                resultado = opeAlqui.listarAlquilerXCod(id);
            }

            return resultado;
        }

        // POST api/alquilar
        public string Post([FromBody] tblDetAlquiler datArt)
        {
            clsOpeAlquilar opeAlqui = new clsOpeAlquilar();
            opeAlqui.tblAlqui = datArt;

            // Asegúrate de que el método Agregar esté correctamente implementado
            return opeAlqui.Agregar();
        }

        // PUT api/alquilar/5
        public string Put([FromBody] tblDetAlquiler datArt)
        {
            clsOpeAlquilar opeAlqui = new clsOpeAlquilar();
            opeAlqui.tblAlqui = datArt;

            // Asegúrate de que el método Modificar esté correctamente implementado
            return opeAlqui.Modificar();
        }

        // DELETE api/alquilar/5
        public void Delete(int id)
        {
            // Implementar lógica de eliminación si es necesario
        }
    }
}
