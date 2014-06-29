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
    public class DLFamilia 
    {
        #region  Members

        /// <summary>
        /// Método obtiene el listado de alumnos
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        public DataSet Listar(BEFamilia pbe)
        {
            DataSet ds = new DataSet();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("[uSP_Syn_ED_Familia_Fotocheck_Listar]");

            sqlDB.AddInParameter(dbCmd, "@as_PeriodoAcademico", DbType.String, pbe.PeriodoAcademico);
            sqlDB.AddInParameter(dbCmd, "@as_Foto", DbType.String, pbe.Foto);
            sqlDB.AddInParameter(dbCmd, "@as_Exportado", DbType.String, pbe.Exportado);
            sqlDB.AddInParameter(dbCmd, "@as_Familia", DbType.String, pbe.Familia);
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


        public BEFamilia Insetar(BEFamilia obe)
        {
            BEFamilia be = new BEFamilia();

            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_Syn_ED_Familia_Fotocheck_Procesar");

            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;

                sqlDB.AddInParameter(dbCmd, "@as_codigo", DbType.String, obe.CodigoFamilia);
                sqlDB.AddInParameter(dbCmd, "@as_Vinculo", DbType.String, obe.Vinculo);
                sqlDB.AddInParameter(dbCmd, "@as_DocumentoIdentidad", DbType.String, obe.DocumentoIdentidad);
                sqlDB.AddInParameter(dbCmd, "@as_PeriodoAcademico", DbType.String, obe.PeriodoAcademico);
                sqlDB.AddInParameter(dbCmd, "@as_UsuarioCreacion", DbType.String, obe.UsuarioCreacion);
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

        public BEFamilia InsetarMasivo(BEFamilia pbe)
        {
            BEFamilia be = new BEFamilia();

            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_Syn_ED_Familia_Fotocheck_ProcesarMasivo");

            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;

                sqlDB.AddInParameter(dbCmd, "@as_PeriodoAcademico", DbType.String, pbe.PeriodoAcademico);
                sqlDB.AddInParameter(dbCmd, "@as_Foto", DbType.String, pbe.Foto);
                sqlDB.AddInParameter(dbCmd, "@as_Exportado", DbType.String, pbe.Exportado);
                sqlDB.AddInParameter(dbCmd, "@as_Codigo", DbType.String, pbe.CodigoFamilia);
                sqlDB.AddInParameter(dbCmd, "@as_Vinculo", DbType.String, pbe.Vinculo);
                sqlDB.AddInParameter(dbCmd, "@as_Familia", DbType.String, pbe.Familia);
                sqlDB.AddInParameter(dbCmd, "@as_DocumentoIdentidad", DbType.String, pbe.DocumentoIdentidad);
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
