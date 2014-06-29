using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Configuration;
using Synergy.Domain.Edu.Entities;
using Synergy.Data.Edu;

namespace Synergy.Data.Edu
{
    public class DLGrado 
    {
        #region  Members


        

        /// <summary>
        /// Método obtiene los grados academicos
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        public DataSet Listar()
        {
            DataSet ds = new DataSet();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("[uSp_Syn_ED_Grado_Listar]");
            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;
                ds = sqlDB.ExecuteDataSet(dbCmd);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbCmd.Dispose();
            }
            return ds;
        }


          

        #endregion  Members
    }
}
