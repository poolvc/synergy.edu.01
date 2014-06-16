using System;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for Constantes
/// </summary>
/// 
namespace Synergy.Infraestructure.CrossCutting
{
    public static class AppSettings
    {
        public static string CadenaConexion = ConfigurationManager.AppSettings["BDCadenaConexion"] as string;

        public static string CadenaClave = ConfigurationManager.AppSettings["CadenaClave"] as string;
        public static string CadenaVector = ConfigurationManager.AppSettings["CadenaVector"] as string;

        public static string FormatoFecha = ConfigurationManager.AppSettings["FormatoFecha"] as string;
        public static string FormatoFechaHora = ConfigurationManager.AppSettings["FormatoFechaHora"] as string;
        public static string FormatoFechaJQuery = ConfigurationManager.AppSettings["FormatoFechaJQuery"] as string;
        public static string CommandTimeout = ConfigurationManager.AppSettings["CommandTimeout"] as string;

        public static string EncriptarCadena = ConfigurationManager.AppSettings["EncriptarCadena"] as string;
        public static string AplicacionCodigo = ConfigurationManager.AppSettings["AplicacionCodigo"] as string;
        public static string LenguajeDesarrollo = ConfigurationManager.AppSettings["LenguajeDesarrollo"] as string;

        public static string RSCadenaConexion = ConfigurationManager.AppSettings["RSCadenaConexion"] as string;
        public static string ISCadenaConexion = ConfigurationManager.AppSettings["ISCadenaConexion"] as string;
        public static string AplicacionKeyDesarrollo = ConfigurationManager.AppSettings["AplicacionKeyDesarrollo"] as string;

    }
}
