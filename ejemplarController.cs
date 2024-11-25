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
    public class ejemplarController : ApiController
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();
        public IQueryable Get(int id, int comando)
        {
            clsOpeEjemplar opeEjem = new clsOpeEjemplar();
            IQueryable resultado;

            if (comando == 1)
            {
                resultado = opeEjem.listarEjemplar();
            }
            else
            {
                resultado = opeEjem.listarPeliculaXCod(id);
            }
            return resultado;
        }

        
    }
}