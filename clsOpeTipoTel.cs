using apiCineClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiCineClub.Clases
{
    public class clsOpeTipoTel
    {
        private readonly bdCineClubEntities oCCE = new bdCineClubEntities();
        public tblTipoTelefono tblTipoT
        {
            get; set;
        }
        public List<tblTipoTelefono> listarTipoTel()
        {
            return oCCE.tblTipoTelefonoes   //Tabla modelo entity
                .OrderBy(x => x.Nombre)
                .ToList();
        }
    }
}