using apiCineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiCineClub.Clases
{
    public class clsOpeTipoDoc
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();
        public tblTipoDoc tblTDoc
        {
            get; set;
        }
        public List<tblTipoDoc> listarTipoDoc()
        {
            return oCCE.tblTipoDocs   //Tabla modelo entity
                .OrderBy(x => x.Nombre)
                .ToList();
        }
    }
}