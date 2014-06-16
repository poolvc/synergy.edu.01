//---------------------------------------------------------------------------------------------------------
// Reg. Creador     : Adexus
// Reg. Ultima Modif: Adexus
// Fecha Modif      : 03/03/2008
// Descripción      :
// Realiza una Conexion a una BD Oracle.
// Observacion      :
// Usa la libreria de MS. 
//---------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Synergy.Data.Edu
{
    public class  ConnectionSql 
    {
        string _strCadenaConexion;

        public ConnectionSql()
        {

        }

        /// <summary>
        /// Verifica si hay Conectividad
        /// </summary>
        /// <returns>Devuelve un DataSet</returns>
        public bool ExisteConectividad(string pstrCadenaConexion, out string pstrMensaje)
        {
            bool blExiste = false;
            pstrMensaje = string.Empty;
            SqlConnection cn = new SqlConnection(pstrCadenaConexion);
            try
            {
                cn.Open();
                blExiste = true;
            }
            catch (Exception ex)
            {
                pstrMensaje = ex.Message;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return blExiste;
        }
    }
}
