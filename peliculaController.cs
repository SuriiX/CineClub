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
    public class peliculaController : ApiController
    {
        public IQueryable Get(int id, int comando )
        {
            clsOpePelicula opePeli = new clsOpePelicula();
            IQueryable resultado;

            if (comando == 1)
            {
                resultado = opePeli.listarPelicula(); 
            }
            else
            {
                resultado = opePeli.listarPeliculaXCod(id); 
            }
            return resultado;
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