using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_Chikitsa_Utility.UtilityTools
{
    public static class ExtendedUtility
    {

        public static List<T> ConvertDataTable<T>(this DataTable  dt)
        {
            List<T> data = new List<T>();   
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;

        }

        private static T GetItem <T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            try
            {
                foreach (DataColumn column in dr.Table.Columns)
                {
                    foreach (PropertyInfo  pro in temp.GetProperties())
                    {
                        if (pro.Name.ToLower() == column.ColumnName.ToLower())
                        {
                            if (pro.PropertyType.FullName.Contains(dr.Table.Columns[column.ColumnName].DataType.FullName, StringComparison.InvariantCultureIgnoreCase))
                            {
                                pro.SetValue(obj, dr[column.ColumnName] == DBNull.Value ? null : dr[column.ColumnName]);
                            }
                            else if (pro.PropertyType.FullName.Contains("bool", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (Convert.ToString(dr[column.ColumnName]).Equals("1"))
                                    pro.SetValue(obj, true, null);
                                else if (Convert.ToString(dr[column.ColumnName]).Equals("0"))
                                    pro.SetValue(obj, false, null);
                                else
                                    pro.SetValue(obj, null, null);
                            }
                            else
                            {
                                MethodInfo method = typeof(ExtendedUtility).GetMethod("ConvertValue");
                                MethodInfo genricMethod = method.MakeGenericMethod(pro.PropertyType);
                                var value = genricMethod.Invoke(null, new object[] { Convert.ToString(dr[column.ColumnName]) });
                                pro.SetValue(obj, value, null);
                            }

                        }
                        else
                            continue;
                    }
                }

            }
            catch (Exception ex)
            {

                return obj;
            }
            return obj;
        }
    }
}
