using System;
using System.Text;
using System.Configuration;
using Synergy.Domain.Edu.Entities;
using Synergy.Infraestructure.CrossCutting;

namespace Synergy.Data.Edu
{
    public enum TipoEncriptado
    {
        No = 0,
        Si = 1
    }
    /// <summary>
    /// Clase encargada de obtener la cadena de conexion
    /// </summary>
    class  CadenaConexion
    {

        static public int CommandTimeout
        {
            get { return Convert.ToInt32(AppSettings.CommandTimeout); }
        }


        public static string ObtenerCadena()
        {
            return ConfigurationManager.ConnectionStrings["SourceConnectionString"].ConnectionString;     
        }


        public static string ObtenerCadena_SpringRatios()
        {
            return ConfigurationManager.ConnectionStrings["BDCadenaConexion_SpringRatios"].ConnectionString;
        }

        public string Encriptar(string cadena, string vector, string llave)
        {
            EncriptadorDLL.Encriptador objEncriptacion = new EncriptadorDLL.Encriptador();
            return objEncriptacion.Encripta(cadena, vector, llave);
        }

        private static string Desencriptar(string cadenaEncriptada, string vector, string llave)
        {
            EncriptadorDLL.Encriptador objEncriptacion = new EncriptadorDLL.Encriptador();
            return objEncriptacion.Desencripta(cadenaEncriptada, vector, llave);
        }

        public static string Obtener()
        {
            string strCadena = string.Empty;
            
            if (AppSettings.EncriptarCadena.ToUpper() == TipoEncriptado.Si.ToString().ToUpper())
            {
                string strEncriptado =AppSettings.CadenaConexion;
                string strKey = AppSettings.CadenaClave;
                string strVector = AppSettings.CadenaVector;

                strCadena = Desencriptar(strEncriptado, strVector , strKey);
            }
            else
            {
                strCadena = AppSettings.CadenaConexion;
            }

            return strCadena;
        }

    }

}
