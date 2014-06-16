using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using System.ComponentModel;

namespace Synergy.Data.Edu
{
    class MapData
    {
        public static void MapObjects(object sourceObject, object targetObject)
        {

            object[] value = new object[1];
            object[] param = new object[0];
            foreach (PropertyInfo propertyInfo
                in sourceObject.GetType().GetProperties())
            {
                PropertyInfo targetPropertyInfo = targetObject.GetType().GetProperty(propertyInfo.Name);
                if (targetPropertyInfo.CanWrite)
                {
                    value[0] = propertyInfo.GetValue(sourceObject, BindingFlags.Public, null, null, null);
                    targetPropertyInfo.SetValue(targetObject, value, null);
                }

            }

        }

        public static List<T> ConvertTo<T>(IDataReader dr) where T : new()
        {
            Type businessEntityType = typeof(T);
            List<T> entitys = new List<T>();
            Hashtable hashtable = new Hashtable();
            PropertyInfo[] properties = businessEntityType.GetProperties();
            foreach (PropertyInfo info in properties)
            {
                hashtable[info.Name.ToUpper()] = info;
            }
            while (dr.Read())
            {
                T newObject = new T();
                for (int index = 0; index < dr.FieldCount; index++)
                {
                    PropertyInfo info = (PropertyInfo) hashtable[dr.GetName(index).ToUpper()];
                    if ((info != null) && info.CanWrite)
                    {                        
                        object ob = dr;
                        info.SetValue(newObject, dr.GetValue(index), null);
                    }
                   
                }
                setProperty(newObject, "Guid", Guid.NewGuid().ToString());

                entitys.Add(newObject);
            }
            dr.Close();
            return entitys;
        }


        private static object getProperty(object containingObject, string propertyName)
        {
            return containingObject.GetType().InvokeMember(propertyName, BindingFlags.GetProperty, null, containingObject, null);
        }

        private static void setProperty(object containingObject, string propertyName, object newValue)
        {
            containingObject.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, containingObject, new object[] { newValue });
        }

        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue(obj, value, null);
                    }
                    catch
                    {
                        // You can log something here
                        throw;
                    }
                }
            }

            return obj;
        }

        
        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }

        /// <summary>
        /// Convert DataTable to IList&lt;T&gt;. Some column names should match property names- or you'll have a list of empty T entities.
        /// </summary>
        /// <example><code>
        /// IList&lt;Person&gt; people = new List&lt;Person&gt;
        ///    {
        ///        new Person { Id= 1, DoB = DateTime.Now, Name = "Bob", Sex = Person.Sexes.Male }
        ///    };
        /// DataTable dt = DataUtil.ConvertToDataTable(people);
        /// IList&lt;Person&gt; people2 = DataUtil.ConvertToList&lt;Person&gt;(dt); //round trip
        /// //Note that people2 is a list of cloned Person objects
        /// </code></example>
        public static IList<T> ConvertToList<T>(DataTable dt) where T : class, new()
        {
            if (dt == null || dt.Rows.Count == 0) return null;
            IList<T> list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T obj = ConvertDataRowToEntity<T>(row);
                list.Add(obj);
            }
            return list;
        }

        /// <summary>
        /// Convert an IList&lt;T&gt; into a DataTable
        /// </summary>
        /// <example><code>
        /// IList&lt;Person&gt; people = new List&lt;Person&gt;
        ///    {
        ///        new Person { Id= 1, DoB = DateTime.Now, Name = "Bob", Sex = Person.Sexes.Male }
        ///    };
        /// DataTable dt = DataUtil.ConvertToDataTable(people);
        /// </code></example>
        public static DataTable ConvertToDataTable<T>(IList<T> list) where T : class
        {
            DataTable table = CreateDataTable<T>();
            Type objType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(objType);
            //Debug.WriteLine("foreach (" + objType.Name + " item in list) {");
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor property in properties)
                {
                    if (!CanUseType(property.PropertyType)) continue; //shallow only
                    //Debug.WriteLine("row[\"" + property.Name + "\"] = item." + property.Name + "; //.HasValue ? (object)item." + property.Name + ": DBNull.Value;");
                    row[property.Name] = property.GetValue(item) ?? DBNull.Value; //can't use null
                }
                table.Rows.Add(row);
            }
            return table;
        }


        private static bool CanUseType(Type propertyType)
        {
            //only strings and value types
            if (propertyType.IsArray) return false;
            if (!propertyType.IsValueType && propertyType != typeof(string)) return false;
            return true;
        }

        /// <summary>
        /// Convert an IList&lt;T&gt; into a DataTable schema
        /// </summary>
        public static DataTable CreateDataTable<T>() where T : class
        {
            Type objType = typeof(T);
            DataTable table = new DataTable(objType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(objType);
            foreach (PropertyDescriptor property in properties)
            {
                Type propertyType = property.PropertyType;
                if (!CanUseType(propertyType)) continue; //shallow only

                //nullables must use underlying types
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    propertyType = Nullable.GetUnderlyingType(propertyType);
                //enums also need special treatment
                if (propertyType.IsEnum)
                    propertyType = Enum.GetUnderlyingType(propertyType); //probably Int32
                //if you have nested application classes, they just get added. Check if this is valid?
                table.Columns.Add(property.Name, propertyType);
            }
            return table;
        }

        /// <summary>
        /// Convert a single DataRow into an object of type T.
        /// </summary>
        public static T ConvertDataRowToEntity<T>(DataRow row) where T : class, new()
        {
            Type objType = typeof(T);
            T obj = Activator.CreateInstance<T>(); //hence the new() contsraint
            //Debug.WriteLine(objType.Name + " = new " + objType.Name + "();");
            foreach (DataColumn column in row.Table.Columns)
            {
                //may error if no match
                PropertyInfo property =
                    objType.GetProperty(column.ColumnName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property == null || !property.CanWrite)
                {
                    //Debug.WriteLine("//Property " + column.ColumnName + " not in object");
                    continue; //or throw
                }
                object value = row[column.ColumnName];
                if (value == DBNull.Value) value = null;
                property.SetValue(obj, value, null);
            }
            return obj;
        }
    }
}
