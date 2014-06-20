using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Synergy.Data.Edu;
using Synergy.Domain.Edu.Entities;

namespace Synergy.Application.Edu
{
    public class BLFicha //: IDisposable
    {
        DLFicha _dl = new DLFicha();

        /// <summary>
        /// Método obtiene atributos de Alumnos
        /// </summary>
        /// <returns>Devuelve una Lista</returns>
        public List<BEFicha> ObtnerAlumnoPorAtributo(BEFicha pbe)
        {
            return _dl.ObtnerAlumnoPorAtributo(pbe);
        }

        /// <summary>
        /// Método obtiene atributos de Familia
        /// </summary>
        /// <returns>Devuelve una Lista</returns>
        public List<BEFicha> ObtnerFamiliaPorAtributo(BEFicha pbe)
        {
            return _dl.ObtnerFamiliaPorAtributo(pbe);
        }

        /// <summary>
        /// Método obtiene atributos de Empleado
        /// </summary>
        /// <returns>Devuelve una Lista</returns>
        public List<BEFicha> ObtnerEmpleadoPorAtributo(BEFicha pbe)
        {
            return _dl.ObtnerEmpleadoPorAtributo(pbe);
        }

    }
}
