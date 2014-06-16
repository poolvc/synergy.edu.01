using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Synergy.Data.Edu;
using Synergy.Domain.Edu.Entities;

namespace Synergy.Application.Edu
{
    public class BLPeriodoAcademico : IDisposable
    {
        DLPeriodoAcademico _dl = new DLPeriodoAcademico();

        /// <summary>
        /// Método obtiene la lista
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        public List<BEPeriodoAcademico> Listar(BEPeriodoAcademico pbe)
        {
            return _dl.Listar(pbe);
        }

        /// <summary>
        /// Método que obtiene un objeto
        /// </summary>
        /// <returns>Devuelve un Objeto</returns>
        public BEPeriodoAcademico Obtener(BEPeriodoAcademico pbe)
        {
            return _dl.Obtener(pbe);
        }

        /// <summary>
        /// Método que inserta un objeto
        /// </summary>
        /// <returns>Devuelve un Objeto</returns>
        public BEPeriodoAcademico Insertar(BEPeriodoAcademico pbe)
        {
            return _dl.Insertar(pbe);
        }

        /// <summary>
        /// Método que actualiza un objeto
        /// </summary>
        /// <returns>Devuelve un Objeto</returns>
        public BEPeriodoAcademico Actualizar(BEPeriodoAcademico pbe)
        {
            return _dl.Actualizar(pbe);
        }

        /// <summary>
        /// Método que elimina logicamente un objeto
        /// </summary>
        /// <returns>Devuelve un Objeto</returns>
        public BEPeriodoAcademico Eliminar(BEPeriodoAcademico pbe)
        {
            return _dl.Eliminar(pbe);
        }


        public void Dispose()
        {
            Dispose();
        }
    }
}
