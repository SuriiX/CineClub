using apiCineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiCineClub.Clases
{
    public class clsOpeEstado
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();
        public tblEstado tblEst
        {
            get; set;
        }
        public List<tblEstado> listarEstado()
        {
            return oCCE.tblEstadoes  //Tabla modelo entity
                .OrderBy(x => x.Nombre)
                .ToList();
        }
    }
}