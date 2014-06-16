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
    public class DLEmpleado : DLTransaction, IDLTransaccion<BEEmpleado>
    {
        #region  Members

        /// <summary>
        /// Método obtiene los Origenes de Datos Activos
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        public DataSet Listar_Sel(BEEmpleado pbe)
        {
            DataSet ds = new DataSet();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_GALEXITO_GEN_Empleado_Listar_Sel");
            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;
                sqlDB.AddInParameter(dbCmd, "@ai_IdCompania", DbType.Int32, pbe.IdCompania);
                sqlDB.AddInParameter(dbCmd, "@as_CentroCosto", DbType.String, pbe.CentroCosto);
                sqlDB.AddInParameter(dbCmd, "@ai_IdEmpleado", DbType.Int32, pbe.IdEmpleado);
                sqlDB.AddInParameter(dbCmd, "@ai_IdProyecto", DbType.Int32, pbe.IdProyecto);
                sqlDB.AddInParameter(dbCmd, "@ai_IdDepartamentoTrabajo", DbType.Int32, pbe.IdDepartamentoTrabajo);
                sqlDB.AddInParameter(dbCmd, "@ai_IdSucursal", DbType.Int32, pbe.IdSucursal);
                sqlDB.AddInParameter(dbCmd, "@ai_IdGradoSalario", DbType.Int32, pbe.IdGradoSalario);
                sqlDB.AddInParameter(dbCmd, "@ai_IdPuesto", DbType.Int32, pbe.IdPuesto);
                sqlDB.AddInParameter(dbCmd, "@ai_IdCargo", DbType.Int32, pbe.IdTipoPuesto);
                sqlDB.AddInParameter(dbCmd, "@ai_IdAreaFuncional", DbType.Int32, pbe.IdAreaFuncional);
                sqlDB.AddInParameter(dbCmd, "@as_EmpleadoNombre", DbType.String, pbe.EmpleadoNombre);
                sqlDB.AddInParameter(dbCmd, "@as_SituacionTrabajo", DbType.String, pbe.SituacionTrabajo);
                sqlDB.AddInParameter(dbCmd, "@as_ClaseTrabajador", DbType.String, pbe.ClaseTrabajador);
                sqlDB.AddInParameter(dbCmd, "@as_Estado", DbType.String, pbe.Estado);
                sqlDB.AddInParameter(dbCmd, "@as_Tipo", DbType.String, pbe.Tipo);
                sqlDB.AddInParameter(dbCmd, "@ai_Pagina", DbType.Int32, pbe.Pagina);
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

        public DataSet Listar_Repetido(BEEmpleado pbe)
        {
            DataSet ds = new DataSet();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_GALEXITO_GEN_Empleado_Listar_Repetido");
            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;
                sqlDB.AddInParameter(dbCmd, "@ai_Pagina", DbType.Int32, pbe.Pagina);
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
