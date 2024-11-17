using apiCineClub.Clases;
using apiCineClub.Models;
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
    public class tipoTelController : ApiController
    {
        public List<tblTipoTelefono> Get()
        {
            clsOpeTipoTel oTipoTel = new clsOpeTipoTel();
            return oTipoTel.listarTipoTel();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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