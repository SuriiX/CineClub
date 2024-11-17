using apiCineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiCineClub.Clases
{
    public class clsOpeRol
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();
        public tblRol tblRo
        {
            get; set;
        }
        public List<tblRol> listarRol()
        {
            return oCCE.tblRols  //Tabla modelo entity
                .OrderBy(x => x.Nombre)
                .ToList();
        }
    }
}