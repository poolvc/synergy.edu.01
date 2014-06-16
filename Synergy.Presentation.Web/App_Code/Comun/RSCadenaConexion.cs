using System;
using System.Text;
using System.Configuration;
using System.Reflection;
using Synergy.Infraestructure.CrossCutting;

namespace Synergy.Presentation.Web
{
    public enum TipoEncriptado
    {
        No = 0,
        Si = 1
    }
    /// <summary>
    /// Clase encargada de obtener la cadena de conexion del Reproting Service
    /// </summary>
    public class RSCadenaConexion
    {

        public virtual string URLServer { get; set; }
        public virtual string Path { get; set; }
        public virtual string UserId { get; set; }
        public virtual string Password { get; set; }
        public virtual int Timeout { get; set; }

        public RSCadenaConexion()
        {
            LeerParametro(ObtenerCadena());
        }

        static public int CommandTimeout
        {
            get { return Convert.ToInt32(AppSettings.CommandTimeout); }
        }


        protected string ObtenerCadena()
        {
            return AppSettings.RSCadenaConexion;     
        }

        protected void LeerParametro(string pstrCadena)
        {
            string[] strParametros = ObtenerCadena().Split(';');
            Type ty = this.GetType();
            PropertyInfo[] pinfs = ty.GetProperties();
            foreach (string strl in strParametros)
            {
                string[] strPs = strl.Split('=');
                foreach (PropertyInfo pinf in pinfs)
                {
                    if (strPs[0].Trim().ToUpper() == pinf.Name.ToUpper())
                    {
                        PropertyInfo pinfSet = ty.GetProperty(pinf.Name);
                        string strTipoDato = pinfSet.PropertyType.FullName;
                        bool isNull = (strPs[1] == null || Convert.IsDBNull(strPs[1]));
                                        
                        object retval = null;
                        switch (strTipoDato)
                        {
                            case "System.String":
                                retval = isNull ? string.Empty : strPs[1].ToString().Trim();
                                break;
                            case "System.Int32":
                                retval = isNull ? 0 : Convert.ToInt32(strPs[1]);
                                break;
                            case "System.DateTime":
                                retval = isNull ? DateTime.MinValue : Convert.ToDateTime(strPs[1]);
                                break;
                            case "System.Boolean":
                                retval = isNull ? false : Convert.ToBoolean(strPs[1]);
                                break;
                            case "System.Double":
                                retval = isNull ? 0 : Convert.ToDouble(strPs[1]);
                                break;
                            case "System.Decimal":
                                retval = isNull ? 0M : Convert.ToDecimal(strPs[1]);
                                break;
                            case "System.Char":
                                retval = isNull ? Char.MinValue : Convert.ToChar(strPs[1]);
                                break;
                        }

                        pinfSet.SetValue(this, retval, null);
                    }
                }
            }
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
                string strEncriptado = AppSettings.RSCadenaConexion;
                string strKey = AppSettings.CadenaClave;
                string strVector = AppSettings.CadenaVector;

                strCadena = Desencriptar(strEncriptado, strVector , strKey);
            }
            else
            {
                strCadena = AppSettings.RSCadenaConexion;
            }

            return strCadena;
        }

    }

}
