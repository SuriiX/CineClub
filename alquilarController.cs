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
                resultado = opeAlqui.listarDetAlquiler();
            }
            else
            {
                resultado = opeAlqui.listarDetAlquilarXCod(id);
            }
            return resultado;
        }

        // POST api/<controller>
        public string Post([FromBody] tblDetAlquiler datArt)
        {
            clsOpeAlquilar opeDetAlqui = new clsOpeAlquilar();

            opeDetAlqui.tblDetAlqui = datArt;
            return opeDetAlqui.Agregar();



        }


        // PUT api/<controller>/5
        public string Put([FromBody] tblDetAlquiler datArt)
        {
            clsOpeAlquilar opeDetAlqui = new clsOpeAlquilar();
            opeDetAlqui.tblDetAlqui = datArt;
            return opeDetAlqui.Modificar();
        }

        // DELETE api/<controller>/5
        public string Delete(int id)
        {
            clsOpeAlquilar opeDetAlqui = new clsOpeAlquilar();
            return opeDetAlqui.Eliminar(id); // Retorna un mensaje del método Eliminar
        }
    }
}