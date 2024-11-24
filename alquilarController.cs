using System;
using System.Collections.Generic;
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
                resultado = opeAlqui.listarAlquilerXCod(int id);
            }
            return resultado;
        }

        // POST api/<controller>
        public string Post([FromBody] tblDetAlquiler datArt)
        {
            clsOpeAlquilar opeAlqui = new clsOpeAlquilar();

            opeAlqui.tblAlqui = datArt;
            return opeAlqui.Agregar();



        }

        // PUT api/<controller>/5
        public string Put(tblDetAlquiler datArt)
        {
            clsOpeAlquilar opeAlqui = new clsOpeAlquilar();

            opeAlqui.tblAlqui = datArt;
            return opeAlqui.Modificar();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
