using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace E_Chikitsa_DBConfiguration
{
    public class GenericPopulator<T>
    {
        public async virtual Task<List<T>> PopulateList(SqlDataReader reader)
        {
            _ = reader ??
              throw new ArgumentNullException(nameof(reader));
            var results = new List<T>();
            if (Type.GetTypeCode(typeof(T)) != TypeCode.Object)
            {
                while (await reader.ReadAsync())
                {
                    results.Add((T)reader[0]);
                }
            }
            else
            {
                Func<SqlDataReader, T> readerRow = this.GetReader(reader);
                while (await reader.ReadAsync()) results.Add(readerRow(reader));
            }
            return results;
        }
        public async virtual Task<T> Populate(SqlDataReader reader)
        {
            _ = reader ??
              throw new ArgumentNullException(nameof(reader));
            var results =
              default(T);
            if (typeof(T) == typeof(string))
            {
                StringBuilder returnResult = new StringBuilder();
                while (await reader.ReadAsync())
                {
                    returnResult.Append(Convert.ToString(reader[0]));
                }
                results = (T)Convert.ChangeType(returnResult.ToString(), typeof(T));
            }
            else if (typeof(T) == typeof(bool?))
            {
                while (await reader.ReadAsync())
                {
                    if (reader[0].GetType() == typeof(DBNull)) results =
                      default(T);
                    else results = (T)reader[0];
                }
            }
            else if (Type.GetTypeCode(typeof(T)) != TypeCode.Object)
            {
                while (await reader.ReadAsync())
                {
                    results = (T)Convert.ChangeType(reader[0], typeof(T));
                }
            }
            else
            {
                results = (T)Activator.CreateInstance(typeof(T));
                Func<SqlDataReader, T> readRow = this.GetReader(reader);
                while (await reader.ReadAsync()) results = readRow(reader);
            }
            return results;
        }
        private Func<SqlDataReader, T> GetReader(SqlDataReader reader)
        {
            Delegate resDelegate;
            List<string> readerColumns = new List<string>();
            for (int index = 0; index < reader.FieldCount; index++) readerColumns.Add(reader.GetName(index));
            var readerParam = Expression.Parameter(typeof(SqlDataReader), "reader");
            var readerGetValue = typeof(SqlDataReader).GetMethod("GetValue");
            var dbNullExp = Expression.Field(expression: null, type: typeof(DBNull), fieldName: "Value");
            List<MemberBinding> memberBindings = new List<MemberBinding>();
            foreach (var prop in typeof(T).GetProperties())
            {
                object defaultValue = null;
                if (prop.PropertyType.IsValueType) defaultValue = Activator.CreateInstance(prop.PropertyType);
                else if (prop.PropertyType.Name.ToLower().Equals("string", StringComparison.Ordinal)) defaultValue = string.Empty;
                if (readerColumns.Contains(prop.Name))
                {
                    var indexExpression = Expression.Constant(reader.GetOrdinal(prop.Name));
                    var getValueExp = Expression.Call(readerParam, readerGetValue, new Expression[] {
            indexExpression
          });
                    var testExp = Expression.NotEqual(dbNullExp, getValueExp);
                    var ifTrue = Expression.Convert(getValueExp, prop.PropertyType);
                    var ifFalse = Expression.Convert(Expression.Constant(defaultValue), prop.PropertyType);
                    MemberInfo memberInfo = typeof(T).GetMember(prop.Name)[0];
                    MemberBinding mb = Expression.Bind(memberInfo, Expression.Condition(testExp, ifTrue, ifFalse));
                    memberBindings.Add(mb);
                }
            }
            var newItem = Expression.New(typeof(T));
            var memberInit = Expression.MemberInit(newItem, memberBindings);
            var lamda = Expression.Lambda<Func<SqlDataReader,
              T>>(memberInit, new ParameterExpression[] {
          readerParam
              });
            resDelegate = lamda.Compile();
            return (Func<SqlDataReader, T>)resDelegate;
        }
    }
}