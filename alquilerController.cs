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
                resultado = opeAlquil.listarAlquiler();
            }
            else
            {
                resultado = opeAlquil.listarAlquilerXCod(id);
            }
            return resultado;
        }

        // POST api/<controller>
        public void Post([FromBody] tblAlquiler datArt)
        {




        }

        // PUT api/<controller>/5
        public void Put(tblAlquiler datArt)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}