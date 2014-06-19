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
    public class DLPeriodoAcademico : DLTransaction, IDLTransaccion<BEPeriodoAcademico>
    {
        #region  Members


        

        /// <summary>
        /// Método obtiene obtiene la Lista de Periodos Academicos
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        public List<BEPeriodoAcademico> Listar(BEPeriodoAcademico pbe)
        {
            List<BEPeriodoAcademico> lst = new List<BEPeriodoAcademico>();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSp_Syn_ED_PeriodoAcademico_Listar");
            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;
                using (IDataReader reader = sqlDB.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        BEPeriodoAcademico be = new BEPeriodoAcademico
                        {
                            PeriodoAcademico = DBValue.Get<String>(reader, "PeriodoAcademico"),
                            DescripcionLocal = DBValue.Get<String>(reader, "DescripcionLocal"),
                        };
                        lst.Add(be);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbCmd.Dispose();
            }
            return lst;
        }


        /// <summary>
        /// Método obtiene Datos Periodos Academicos
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        /// 
        public BEPeriodoAcademico Obtener(BEPeriodoAcademico pbe)
        {
            BEPeriodoAcademico be = new BEPeriodoAcademico();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("Usp_GALEXITO_GEN_Periodo_Obtener");
            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;
                //sqlDB.AddInParameter(dbCmd, "@as_Periodo", DbType.String, pbe.Periodo);

                using (IDataReader reader = sqlDB.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        be.PeriodoAcademico = DBValue.Get<String>(reader, "Periodo");
                        be.DescripcionLocal = DBValue.Get<String>(reader, "DescripcionLocal");
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            finally
            {                
                dbCmd.Dispose();
            }
            return be;
        }
        


        /// <summary>
        /// Método Inserta un Periodos Academicos
        /// </summary>
        /// <returns>Devuelve una entidad</returns>
        public BEPeriodoAcademico Insertar(BEPeriodoAcademico pbe)
        {
            BEPeriodoAcademico be = new BEPeriodoAcademico();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("Usp_GALEXITO_GEN_Periodo_Insertar");
            
            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;
                dbCmd.Transaction = Transaction.InnerDbTransaction; //Para administrar la transaccion
                dbCmd.Connection = Transaction.Connection;  //Para administrar la conecion de la transaccion

                //sqlDB.AddInParameter(dbCmd, "@as_Periodo", DbType.String, pbe.Periodo);
                //sqlDB.AddInParameter(dbCmd, "@ai_IdPeriodoAnual", DbType.Int32, pbe.IdPeriodoAnual);
                //sqlDB.AddInParameter(dbCmd, "@as_Vigente", DbType.String, pbe.Vigente);
                //sqlDB.AddInParameter(dbCmd, "@as_Estado", DbType.String, pbe.Estado);
                //sqlDB.AddInParameter(dbCmd, "@as_UsuarioModificacion", DbType.String, pbe.UsuarioModificacion);
                //sqlDB.AddInParameter(dbCmd, "@as_UsuarioCreacion", DbType.String, pbe.UsuarioCreacion);

                using (IDataReader reader = sqlDB.ExecuteReader(dbCmd, dbCmd.Transaction))
                {
                    while (reader.Read())
                    {
                        be.PeriodoAcademico = DBValue.Get<String>(reader, "PeriodoAcademico");
                        be.FechaCreacion = DBValue.Get<DateTime>(reader, "FechaCreacion");
                        //Error
                        be.Error = DBValue.Get<Int32>(reader, "CodigoError");
                        be.Mensaje = DBValue.Get<String>(reader, "MensajeError");
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            finally
            {
                dbCmd.Dispose();
            }
            return be;
        }


        /// <summary>
        /// Método Actualiza un Periodos Academicos
        /// </summary>
        /// <returns>Devuelve una entidad</returns>
        public BEPeriodoAcademico Actualizar(BEPeriodoAcademico pbe)
        {
            BEPeriodoAcademico be = new BEPeriodoAcademico();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("Usp_GALEXITO_GEN_Periodo_Actualizar");

            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;
                dbCmd.Transaction = Transaction.InnerDbTransaction; //Para administrar la transaccion
                dbCmd.Connection = Transaction.Connection;  //Para administrar la conecion de la transaccion
                //sqlDB.AddInParameter(dbCmd, "@ai_IdPeriodoAnual", DbType.Int32, pbe.IdPeriodoAnual);
                //sqlDB.AddInParameter(dbCmd, "@as_Vigente", DbType.String, pbe.Vigente);
                //sqlDB.AddInParameter(dbCmd, "@as_Estado", DbType.String, pbe.Estado);
                //sqlDB.AddInParameter(dbCmd, "@as_UsuarioModificacion", DbType.String, pbe.UsuarioModificacion);
                //sqlDB.AddInParameter(dbCmd, "@adt_FechaModificacion", DbType.DateTime, pbe.FechaModificacion);

                using (IDataReader reader = sqlDB.ExecuteReader(dbCmd, dbCmd.Transaction))
                {
                    while (reader.Read())
                    {
                        be.PeriodoAcademico = DBValue.Get<String>(reader, "PeriodoAcademico");
                        be.FechaModificacion = DBValue.Get<DateTime>(reader, "FechaModificacion");
                        //Error
                        be.Error = DBValue.Get<Int32>(reader, "CodigoError");
                        be.Mensaje = DBValue.Get<String>(reader, "MensajeError");
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            finally
            {
                dbCmd.Dispose();
            }
            return be;
        }


        /// <summary>
        /// Método Actualiza el Estado Periodos Academicos
        /// </summary>
        /// <returns>Devuelve una entidad</returns>
        public BEPeriodoAcademico Eliminar(BEPeriodoAcademico pbe)
        {
            BEPeriodoAcademico be = new BEPeriodoAcademico();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("Usp_GALEXITO_GEN_Periodo_Eliminar");

            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;
                //dbCmd.Transaction = Transaction.InnerDbTransaction; //Para administrar la transaccion
                //dbCmd.Connection = Transaction.Connection;  //Para administrar la conecion de la transaccion
                //sqlDB.AddInParameter(dbCmd, "@ai_IdPeriodo", DbType.Int32, pbe.IdPeriodo);
                sqlDB.AddInParameter(dbCmd, "@as_Estado", DbType.String, pbe.Estado);
                sqlDB.AddInParameter(dbCmd, "@as_UsuarioModificacion", DbType.String, pbe.UsuarioModificacion);
                sqlDB.AddInParameter(dbCmd, "@adt_FechaModificacion", DbType.DateTime, pbe.FechaModificacion);

                using (IDataReader reader = sqlDB.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        be.PeriodoAcademico = DBValue.Get<String>(reader, "PeriodoAcademico");
                        be.FechaModificacion = DBValue.Get<DateTime>(reader, "FechaModificacion");
                        //Error
                        be.Error = DBValue.Get<Int32>(reader, "CodigoError");
                        be.Mensaje = DBValue.Get<String>(reader, "MensajeError");
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
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
