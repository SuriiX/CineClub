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
    public class alquilerController : ApiController
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();
        public IQueryable Get(int id, int comando)
        {
            clsOpeAlquiler opeAlquil = new clsOpeAlquiler();
            IQueryable resultado;

            if (comando == 1)
            {
                resultado = opeAlquil.listarAlquilereses();
            }
            else
            {
                resultado = opeAlquil.listarAlquilerXCod(int id);
            }
            return resultado;
        }

        // POST api/<controller>
        public string Post([FromBody] tblAlquiler datArt)
        {
            clsOpeAlquiler opeAlquil = new clsOpeAlquiler();

            opeAlquil.tblAlquil = datArt;
            return opeAlquil.Agregar();



        }

        // PUT api/<controller>/5
        public string Put(tblAlquiler datArt)
        {
            clsOpeAlquiler opeAlquil = new clsOpeAlquiler();

            opeAlquil.tblAlquil = datArt;
            return opeAlquil.Modificar();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
