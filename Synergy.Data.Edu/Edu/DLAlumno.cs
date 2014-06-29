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
    public class DLAlumno 
    {
        #region  Members

        /// <summary>
        /// Método obtiene el listado de alumnos
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        public DataSet Listar(BEAlumnos pbe)
        {
            DataSet ds = new DataSet();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("[uSP_Syn_ED_Alumno_Fotocheck_Listar]");

            sqlDB.AddInParameter(dbCmd, "@as_PeriodoAcademico", DbType.String, pbe.PeriodoAcademico);
            sqlDB.AddInParameter(dbCmd, "@as_Foto", DbType.String, pbe.Foto);
            sqlDB.AddInParameter(dbCmd, "@as_Exportado", DbType.String, pbe.Exportado);
            sqlDB.AddInParameter(dbCmd, "@as_Grado", DbType.String, pbe.Grado);
            sqlDB.AddInParameter(dbCmd, "@as_Seccion", DbType.String, pbe.Seccion);
            sqlDB.AddInParameter(dbCmd, "@as_Familia", DbType.String, pbe.Familia);
            sqlDB.AddInParameter(dbCmd, "@as_Alumno", DbType.String, pbe.Alumno);
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


        public BEAlumnos Insetar(string strCodigoAlumno,string pstrPeriodoAcademico, string pstrUsuarioCreacion)
        {
            BEAlumnos be = new BEAlumnos();

            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_Syn_ED_Alumno_Fotocheck_Procesar");

            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;

                sqlDB.AddInParameter(dbCmd, "@as_Alumnocodigo", DbType.String, strCodigoAlumno);
                sqlDB.AddInParameter(dbCmd, "@as_PeriodoAcademico", DbType.String, pstrPeriodoAcademico);
                sqlDB.AddInParameter(dbCmd, "@as_UsuarioCreacion", DbType.String, pstrUsuarioCreacion);
                using (IDataReader reader = sqlDB.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {

                        be.Alumno = DBValue.Get<String>(reader, "AlumnoCodigo");
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

        public BEAlumnos InsetarMasivo(BEAlumnos pbe)
        {
            BEAlumnos be = new BEAlumnos();

            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_Syn_ED_Alumno_Fotocheck_ProcesarMasivo");

            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;

                sqlDB.AddInParameter(dbCmd, "@as_PeriodoAcademico", DbType.String, pbe.PeriodoAcademico);
                sqlDB.AddInParameter(dbCmd, "@as_Foto", DbType.String, pbe.Foto);
                sqlDB.AddInParameter(dbCmd, "@as_Exportado", DbType.String, pbe.Exportado);
                sqlDB.AddInParameter(dbCmd, "@as_Grado", DbType.String, pbe.Grado);
                sqlDB.AddInParameter(dbCmd, "@as_Seccion", DbType.String, pbe.Seccion);
                sqlDB.AddInParameter(dbCmd, "@as_Familia", DbType.String, pbe.Familia);
                sqlDB.AddInParameter(dbCmd, "@as_AlumnoCodigo", DbType.String, pbe.Alumno);
                sqlDB.AddInParameter(dbCmd, "@as_UsuarioCreacion", DbType.String, pbe.UsuarioCreacion);

                using (IDataReader reader = sqlDB.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {

                        be.Alumno = DBValue.Get<String>(reader, "AlumnoCodigo");
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
