using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using apiCineClub.Models;
namespace apiCineClub.Clases
{
    public class clsOpeGenero
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();
        public tblGenero tblGen
        {
            get; set;
        }  
        public List<tblGenero> listarGenero()
        {
            return oCCE.tblGeneroes   //Tabla modelo entity
                .OrderBy(x => x.Nombre)
                .ToList();
        }
    }
}