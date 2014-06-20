using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Synergy.Data.Edu;
using Synergy.Domain.Edu.Entities;

namespace Synergy.Application.Edu
{
    public class BLAlumnoSel //: IDisposable
    {
        DLAlumnoSel _dl = new DLAlumnoSel();

        /// <summary>
        /// Método obtiene la lista
        /// </summary>
        /// <returns>Devuelve una Lista</returns>
        public List<BEAlumnoSel> ListarPorCampo(BEAlumnoSel pbe)
        {
            return _dl.ListarPorCampo(pbe);
        }

    }
}
