using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Synergy.Data.Edu
{
    class DBValue
    {
        /// <summary>
        /// Obtiene el valor del DataReader y lo castea segun el tipo "T" especificado. Si se trata de un valor DBNull, devuelve un valor por defecto del tipo "T" especificado.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Get<T>(IDataReader reader, string fieldName)
        {
            if (!Convert.IsDBNull(reader[fieldName]))
                return (T)reader[fieldName];

            object retval=null;
            switch (typeof(T).FullName)
            {
                case "System.String":
                    retval=string.Empty;
                    break;
                case "System.DateTime":
                    retval = DateTime.MinValue;
                    break;                
                case "System.Boolean":
                    retval = false;
                    break;
                case "System.Decimal":
                    retval = 0M;
                    break;
                case "System.Char":
                    retval = char.MinValue;
                    break;
                default:
                    retval = 0;
                    break;
            }
            return (T)retval;
        }

        /// <summary>
        /// Obtiene el valor del DataReader y lo castea segun el tipo "T" especificado. Si se trata de un valor DBNull, devuelve el valor especificado por defecto.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Get<T>(IDataReader reader, string fieldName, T defaultValue)
        {
            if (!Convert.IsDBNull(reader[fieldName]))
                return (T)reader[fieldName];
            return defaultValue;
        }
        /// <summary>
        /// Obtiene el valor no nulo del execute scalar y lo castea segun el tipo "T" especificado. Si se trata de un valor DBNull, devuelve un valor por defecto del tipo "T" especificado.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static T GetScalar<T>(object scalar)
        {
            bool isNull = (scalar == null || Convert.IsDBNull(scalar));

            object retval = null;
            switch (typeof(T).FullName)
            {
                case "System.String":                    
                    retval = isNull?string.Empty: scalar.ToString().Trim();
                    break;
                case "System.Int32":
                    retval = isNull ? 0 : Convert.ToInt32(scalar);
                    break;
                case "System.DateTime":
                    retval = isNull ? DateTime.MinValue : Convert.ToDateTime(scalar);
                    break;
                case "System.Boolean":
                    retval = isNull ? false : Convert.ToBoolean(scalar);
                    break;
                case "System.Double":
                    retval = isNull ? 0 : Convert.ToDouble(scalar);
                    break;
                case "System.Decimal":
                    retval = isNull ? 0M: Convert.ToDecimal(scalar);
                    break;
                case "System.Char":
                    retval = isNull ? Char.MinValue : Convert.ToChar(scalar);
                    break;
            }
            return (T)retval;
        }
        /// <summary>
        /// Obtiene el valor no nulo del execute scalar y lo castea segun el tipo "T" especificado. Si se trata de un valor DBNull, devuelve el valor especificado por defecto.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static T GetScalar<T>(object scalar, T defaultValue)
        {
            if (scalar != null && !Convert.IsDBNull(scalar))
                return (T)scalar;
            return defaultValue;
        }


        /// <summary>
        /// Obtiene el valor del DataReader y lo castea segun el tipo "T" especificado. Si se trata de un valor DBNull, devuelve un valor por defecto del tipo "T" especificado.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Get<T>(DataRow datarow, string fieldName)
        {
            if (!Convert.IsDBNull(datarow[fieldName]))
                return (T)datarow[fieldName];

            object retval = null;
            switch (typeof(T).FullName)
            {
                case "System.String":
                    retval = string.Empty;
                    break;
                case "System.DateTime":
                    retval = DateTime.MinValue;
                    break;
                case "System.Boolean":
                    retval = false;
                    break;
                case "System.Decimal":
                    retval = 0M;
                    break;
                case "System.Char":
                    retval = char.MinValue;
                    break;
                default:
                    retval = 0;
                    break;
            }
            return (T)retval;
        }
    }
}
