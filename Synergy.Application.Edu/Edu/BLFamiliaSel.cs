using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Synergy.Data.Edu;
using Synergy.Domain.Edu.Entities;

namespace Synergy.Application.Edu
{
    public class BLFamiliaSel //: IDisposable
    {
        DLFamiliaSel _dl = new DLFamiliaSel();

        /// <summary>
        /// Método obtiene la lista
        /// </summary>
        /// <returns>Devuelve una Lista</returns>
        public List<BEFamiliaSel> ListarPorCampo(BEFamiliaSel pbe)
        {
            return _dl.ListarPorCampo(pbe);
        }

    }
}
