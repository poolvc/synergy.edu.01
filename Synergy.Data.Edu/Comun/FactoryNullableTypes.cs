using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Synergy.Data.Edu
{
    [Serializable]
    #region FactoryNullableTypes
    public sealed class FactoryNullableTypes
    {

        public static System.String getString(IDataReader rdr, int col)
        {
            if (rdr.GetValue(col).Equals(DBNull.Value))
                return null;
            else
                return rdr.GetString(col);
        }

        public static System.String getString(IDataReader rdr, string col)
        {
            if (rdr[col].Equals(DBNull.Value))
                return null;
            else
                return rdr[col].ToString();
        }

        public static Nullable<System.Double> getDouble(IDataReader rdr, int col)
        {
            if (rdr.GetValue(col).Equals(DBNull.Value))
                return null;
            else
                return Convert.ToDouble(rdr[col]);

        }

        public static Nullable<System.Double> getDouble(IDataReader rdr, string col)
        {
            if (rdr[col].Equals(DBNull.Value))
                return null;
            else
                return Convert.ToDouble(rdr[col]);
        }

        public static Nullable<System.Int16> getInt16(IDataReader rdr, int col)
        {
            if (rdr[col].Equals(DBNull.Value))
                return null;
            else
                return Convert.ToInt16(rdr[col]);
        }

        public static Nullable<System.Int16> getInt16(IDataReader rdr, string col)
        {
            if (rdr[col].Equals(DBNull.Value))
                return null;
            else
                return Convert.ToInt16(rdr[col]);
        }

        public static Nullable<System.Int32> getInt32(IDataReader rdr, int col)
        {
            if (rdr.GetValue(col).Equals(DBNull.Value))
                return null;
            else
                return Convert.ToInt32(rdr[col]);
        }

        public static Nullable<System.Int32> getInt32(IDataReader rdr, string col)
        {
            if (rdr[col].Equals(DBNull.Value))
                return null;
            else
                return Convert.ToInt32(rdr[col]);

        }

        public static Nullable<System.Int64> getInt64(IDataReader rdr, int col)
        {
            if (rdr.GetValue(col).Equals(DBNull.Value))
                return null;
            else
                return Convert.ToInt64(rdr[col]);

        }

        public static Nullable<System.Int64> getInt64(IDataReader rdr, string col)
        {
            if (rdr[col].Equals(DBNull.Value))
                return null;
            else
                return Convert.ToInt64(rdr[col]);
        }

        public static Nullable<System.DateTime> getDate(IDataReader rdr, int col)
        {
            if (rdr.GetValue(col).Equals(DBNull.Value))
                return null;
            else
                return Convert.ToDateTime(rdr[col]);
        }

        public static Nullable<System.DateTime> getDate(IDataReader rdr, string col)
        {
            if (rdr[col].Equals(DBNull.Value))
                return null;
            else
                return Convert.ToDateTime(rdr[col]);
        }

        public static Nullable<System.Boolean> getBoolean(IDataReader rdr, int col)
        {
            if (rdr.GetValue(col).Equals(DBNull.Value))
                return null;
            else
                return Convert.ToBoolean(rdr[col]);
        }

        public static Nullable<System.Boolean> getBoolean(IDataReader rdr, string col)
        {
            if (rdr[col].Equals(DBNull.Value))
                return null;
            else
                return Convert.ToBoolean(rdr[col]);
        }
    }
    #endregion FactoryNullableTypes
}
