using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Synergy.Data.Edu;
using Synergy.Domain.Edu.Entities;



namespace Synergy.Application.Edu
{
    public class BLEmpleado
    {
        //Objeto transaccion
        DLTransactionMng _transaccion = null;
        DLEmpleado _dlEmpleado = new DLEmpleado();

        /// <summary>
        /// Método extrae Companias por Usuario
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        public DataSet Listar_Sel(BEEmpleado pbe)
        {
            return _dlEmpleado.Listar_Sel(pbe);
        }

        public DataSet Listar_Repetido(BEEmpleado pbe)
        {
            return _dlEmpleado.Listar_Repetido(pbe);
        }

    }
}
