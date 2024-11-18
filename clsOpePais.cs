using apiCineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiCineClub.Clases
{
    public class clsOpePais
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();
        public tblPai tblPais
        {
            get; set;
        }
        public List<tblPai> listarPais()
        {
            return oCCE.tblPais   //Tabla modelo entity
                .OrderBy(x => x.Codigo)
                .ToList();
        }
    }
}