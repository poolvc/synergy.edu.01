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

        public DataSet Listar(BEEmpleado pbe)
        {
            DataSet ds = new DataSet();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("[uSP_Syn_ED_Empleado_Fotocheck_Listar]");

            sqlDB.AddInParameter(dbCmd, "@as_PeriodoAcademico", DbType.String, pbe.PeriodoAcademico);
            sqlDB.AddInParameter(dbCmd, "@as_Foto", DbType.String, pbe.Foto);
            sqlDB.AddInParameter(dbCmd, "@as_Exportado", DbType.String, pbe.Exportado);
            sqlDB.AddInParameter(dbCmd, "@ai_Pagina", DbType.Int32, pbe.Pagina);

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

        public BEEmpleado Insertar(string strCodigo, string pstrPeriodoAcademico, string pstrUsuarioCreacion)
        {
            BEEmpleado be = new BEEmpleado();

            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_Syn_ED_Empleado_Fotocheck_Procesar");

            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;

                sqlDB.AddInParameter(dbCmd, "@as_codigo", DbType.String, strCodigo);
                sqlDB.AddInParameter(dbCmd, "@as_PeriodoAcademico", DbType.String, pstrPeriodoAcademico);
                sqlDB.AddInParameter(dbCmd, "@as_UsuarioCreacion", DbType.String, pstrUsuarioCreacion);
                using (IDataReader reader = sqlDB.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        //Error
                        be.Error = DBValue.Get<Int32>(reader, "CodigoError");
                        be.Mensaje = DBValue.Get<String>(reader, "MensajeError");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCmd.Dispose();
            }

            return be;
        }

        public BEEmpleado InsetarMasivo(BEEmpleado pbe)
        {
            BEEmpleado be = new BEEmpleado();

            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_Syn_ED_Empleado_Fotocheck_ProcesarMasivo");

            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;

                sqlDB.AddInParameter(dbCmd, "@as_PeriodoAcademico", DbType.String, pbe.PeriodoAcademico);
                sqlDB.AddInParameter(dbCmd, "@as_Foto", DbType.String, pbe.Foto);
                sqlDB.AddInParameter(dbCmd, "@as_Exportado", DbType.String, pbe.Exportado);
                sqlDB.AddInParameter(dbCmd, "@as_Codigo", DbType.String, pbe.Codigo);
                sqlDB.AddInParameter(dbCmd, "@as_UsuarioCreacion", DbType.String, pbe.UsuarioCreacion);

                using (IDataReader reader = sqlDB.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {

                         
                        //Error
                        be.Error = DBValue.Get<Int32>(reader, "CodigoError");
                        be.Mensaje = DBValue.Get<String>(reader, "MensajeError");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCmd.Dispose();
            }

            return be;
        }

        #endregion  Members
    }
}
