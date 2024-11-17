using apiCineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiCineClub.Clases
{
    public class clsOpeCiudad
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();
        public tblCiudad tblCiu
        {
            get; set;
        }
        public List<tblCiudad> listarCiudad()
        {
            return oCCE.tblCiudads   //Tabla modelo entity
                .OrderBy(x => x.Nombre)
                .ToList();
        }
    }
}
