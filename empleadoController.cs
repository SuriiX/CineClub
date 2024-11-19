using apiCineClub.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace apiCineClub.Controllers
{
    [EnableCors(origins: "http://localhost:52006", headers: "*", methods: "*")]
    public class empleadoController : ApiController
    {
        // GET api/<controller>
        public IQueryable Get(int id, int comando)
        {
            clsOpeEmpleado opeEmple = new clsOpeEmpleado();
            IQueryable resultado;

            if (comando == 1)
            {
                resultado = opeEmple.listarEmpleado();
            }
            else
            {
                resultado = null;
            }
            return resultado;
        }
        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}