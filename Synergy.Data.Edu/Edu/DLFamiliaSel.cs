﻿using System;
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
    public class DLFamiliaSel : DLTransaction, IDLTransaccion<BEFamiliaSel>
    {
        #region  Members

        /// <summary>
        /// Método obtiene obtiene la Lista de Familias por Campo
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        public List<BEFamiliaSel> ListarPorCampo(BEFamiliaSel pbe)
        {
            List<BEFamiliaSel> lst = new List<BEFamiliaSel>();
            SqlDatabase sqlDB = new SqlDatabase(CadenaConexion.Obtener());
            DbCommand dbCmd = sqlDB.GetStoredProcCommand("uSP_Syn_ED_Familia_Listar_X_Campo");
            try
            {
                dbCmd.CommandTimeout = CadenaConexion.CommandTimeout;
                sqlDB.AddInParameter(dbCmd, "@as_Columna", DbType.String, pbe.Columna);
                sqlDB.AddInParameter(dbCmd, "@as_Valor", DbType.String, pbe.Valor);
                sqlDB.AddInParameter(dbCmd, "@ai_Pagina", DbType.Int32, pbe.Pagina);
                using (IDataReader reader = sqlDB.ExecuteReader(dbCmd))
                {
                    while (reader.Read())
                    {
                        BEFamiliaSel be = new BEFamiliaSel
                        {
                            AlumnoGrupo = DBValue.Get<string>(reader, "AlumnoGrupo"),
                            ApellidoPaterno = DBValue.Get<string>(reader, "ApellidoPaterno"),
                            ApellidoMaterno = DBValue.Get<string>(reader, "ApellidoMaterno"),
                            Estado = DBValue.Get<String>(reader, "Estado"),

                            FilasXPagina = DBValue.Get<int>(reader, "FilasXPagina"),
                            TotalFilas = DBValue.Get<int>(reader, "TotalFilas"),
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
