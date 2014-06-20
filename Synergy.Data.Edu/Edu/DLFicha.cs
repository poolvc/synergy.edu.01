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
    public class DLFicha : DLTransaction, IDLTransaccion<BEFicha>
    {
        #region  Members

        /// <summary>
        /// Método obtiene obtiene los Abributos del Alumno
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        public List<BEFicha> ObtnerAlumnoPorAtributo(BEFicha pbe)
        {
            List<BEFicha> lst = new List<BEFicha>();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_Syn_ED_Alumno_Obtener_X_Atributo");
            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;
                sqlDB.AddInParameter(dbCmd, "@as_PeriodoAcademico", DbType.String, pbe.PeriodoAcademico);
                sqlDB.AddInParameter(dbCmd, "@as_CodigoAlumno", DbType.String, pbe.CodigoAlumno);
                using (IDataReader reader = sqlDB.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        BEFicha be = new BEFicha
                        {
                            Columna = DBValue.Get<string>(reader, "Columna"),
                            Descripcion = DBValue.Get<string>(reader, "Descripcion"),
                            Valor = DBValue.Get<string>(reader, "Valor"),

                            Fila = Convert.ToInt32(DBValue.Get<Int64>(reader, "Fila")),
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
        /// Método obtiene obtiene los Abributos de la Familia
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        public List<BEFicha> ObtnerFamiliaPorAtributo(BEFicha pbe)
        {
            List<BEFicha> lst = new List<BEFicha>();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_Syn_ED_Familia_Obtener_X_Atributo");
            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;
                sqlDB.AddInParameter(dbCmd, "@as_PeriodoAcademico", DbType.String, pbe.PeriodoAcademico);
                sqlDB.AddInParameter(dbCmd, "@as_AlumnoGrupo", DbType.String, pbe.AlumnoGrupo);
                sqlDB.AddInParameter(dbCmd, "@as_Vinculo", DbType.String, pbe.Vinculo);
                using (IDataReader reader = sqlDB.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        BEFicha be = new BEFicha
                        {
                            Columna = DBValue.Get<string>(reader, "Columna"),
                            Descripcion = DBValue.Get<string>(reader, "Descripcion"),
                            Valor = DBValue.Get<string>(reader, "Valor"),

                            Fila = Convert.ToInt32(DBValue.Get<Int64>(reader, "Fila")),
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
        /// Método obtiene obtiene los Abributos del Empleado
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        public List<BEFicha> ObtnerEmpleadoPorAtributo(BEFicha pbe)
        {
            List<BEFicha> lst = new List<BEFicha>();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_Syn_ED_Empleado_Obtener_X_Atributo");
            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;
                sqlDB.AddInParameter(dbCmd, "@as_PeriodoAcademico", DbType.String, pbe.PeriodoAcademico);
                sqlDB.AddInParameter(dbCmd, "@as_CodigoEmpleado", DbType.String, pbe.CodigoEmpleado);
                using (IDataReader reader = sqlDB.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        BEFicha be = new BEFicha
                        {
                            Columna = DBValue.Get<string>(reader, "Columna"),
                            Descripcion = DBValue.Get<string>(reader, "Descripcion"),
                            Valor = DBValue.Get<string>(reader, "Valor"),

                            Fila = Convert.ToInt32(DBValue.Get<Int64>(reader, "Fila")),
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


        #endregion  Members
    }
}
