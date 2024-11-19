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
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();
        public IQueryable Get(int id, int comando)
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

        // POST api/<controller>
        public string Post([FromBody] tblPelicula datArt)
        {
            clsOpePelicula opePeli = new clsOpePelicula();

            opePeli.tblPeli = datArt;
            return opePeli.Agregar();



        }

        // PUT api/<controller>/5
        public string Put([FromBody] tblPelicula datArt)
        {
            clsOpePelicula opePeli = new clsOpePelicula();
            opePeli.tblPeli = datArt;
            return opePeli.Modificar();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}