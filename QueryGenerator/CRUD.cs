using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace QueryGenerator
{
    public class CRUD<TSource>
    {
        public static string Select(Expression<Func<TSource, bool>> propertyLambda = null)
        {
            var excludes = new List<String>();
            var set = string.Empty;
            var where = string.Empty;

            Type type = typeof(TSource);
            var columns = GetColumns(true);
            if (propertyLambda != null)
            {
                var expression = TrimedExperssion(propertyLambda.Body.ToString());
                where = GetWhereCondition(expression);
            }
            set = string.Join(",", columns);
            where = where.TrimEnd(',');
            where = string.IsNullOrWhiteSpace(where) ? string.Empty : string.Format(" WHERE {0}", where); //problem in formate
            return "SELECT " + set + " FROM " + GetTableName() + where;
        }
        public static string Insert()
        {
            var query = string.Empty;
            var columns = string.Join(",", GetColumns());
            var values = "@" + string.Join(",@", GetColumns());
            query = " INSERT INTO " + GetTableName();
            if (string.IsNullOrWhiteSpace(columns) || string.IsNullOrWhiteSpace(values))
            {
                throw new Exception("No column found in the model");
            }
            columns = "( " + columns.TrimStart(',') + " )";
            values = " VALUES ( " + values.TrimStart(',') + " )";
            return query + columns + values + "; Select cast(scope_identity() as int )";
        }
        public static string Update(Expression<Func<TSource, bool>> propertyLambda = null, Expression<Func<TSource, bool>> Excludes = null)
        {
            var whereExcludes = new List<string>();
            var updateExcludes = new List<string>();
            var primaryKeyColumns = new List<string>();
            var set = string.Empty;
            var where = string.Empty;
            Type type = typeof(TSource);
            var columns = GetColumns();
            if (Excludes != null) // problem in exclude
            {
                var expression = TrimedExperssion(Excludes.Body.ToString());
                updateExcludes = GetExcludes(expression);
                columns = columns.Except(updateExcludes).ToList();
            }
            if (propertyLambda != null)
            {
                var expression = TrimedExperssion(propertyLambda.Body.ToString());
                whereExcludes = GetExcludes(expression);
                where = GetWhereCondition(expression);
                columns = columns.Except(whereExcludes).ToList();
            }
            else
            {
                primaryKeyColumns = GetPrimaryKeyColumns();
                foreach (var v in primaryKeyColumns)
                {
                    where = string.Format("{0} {1}=@{1} AND", where, v);
                }
                where = where.TrimEnd('D').TrimEnd('N').TrimEnd('A');
            }
            foreach (var v in columns)
            {
                set = string.Format("{0} {1}=@{1},", set, v);
            }
            where = where.TrimEnd(',');
            set = set.TrimEnd(',');
            return "UPDATE " + GetTableName() + " SET " + set + " WHERE " + where + "; select cast (@@ROWCOUNT as int)";
        }
        public static string Delete(Expression<Func<TSource, bool>> propertyLambda = null)
        {
            var excludes = new List<string>();
            var set = string.Empty;
            var where = string.Empty;

            Type type = typeof(TSource);
            var columns = GetColumns();
            if (propertyLambda != null)
            {
                var expression = TrimedExperssion(propertyLambda.Body.ToString());
                excludes = GetExcludes(expression);
                where = GetWhereCondition(expression);
                columns = columns.Except(excludes).ToList();
            }
            else
            {
                excludes = GetPrimaryKeyColumns();
                foreach (var v in excludes)
                {
                    where = string.Format("{0} {1}=@{1} AND", where, v);
                }
                where = where.TrimEnd('D').TrimEnd('N').TrimEnd('A');
            }
            foreach (var v in columns)
            {
                set = string.Format("{0} {1}=@{1},", set, v);
            }
            where = where.TrimEnd(',');
            return "DELETE FROM " + GetTableName() + " WHERE " + where + "; Select cast(@@ROWCOUNT as int)"; //need to check
        }
        private static List<string> GetExcludes(string expression)
        {
            var excludes = new List<string>();
            expression = TrimedExperssion(expression);
            var words = expression.Split(' ');
            var reg = new Regex(@"^(([a-z0-9]+)\.[(a-z0-9)]+)$", RegexOptions.IgnoreCase);
            for (int i = 0; i < words.Count(); i++)
            {
                words[i] = words[i].Replace("{", "").Replace("(", "").Replace(")", "").Replace("}", "");
                var match = reg.Match(words[i]);
                if (match.Success)
                {
                    excludes.Add(match.Value.Split('.')[1]);
                }
            }
            excludes = excludes.Distinct().ToList();
            return excludes;
        }
        private static string GetWhereCondition(string text)
        {
            var pattern = @"(\= [a-z]+)(\.)";
            text = Regex.Replace(text, pattern, m => "= @" + m.Groups[3].Value);
            pattern = @"([a-z]+)(\.)";
            text = Regex.Replace(text, pattern, m => m.Groups[3].Value);
            return text;
        }
        private static bool IsValidPropertyType(Type type)
        {
            if (type == typeof(Nullable<Int32>) || type == typeof(Int32))
            {
                return true;
            }
            if (type == typeof(Nullable<bool>) || type == typeof(bool))
            {
                return true;
            }
            if (type == typeof(Nullable<DateTime>) || type == typeof(DateTime))
            {
                return true;
            }
            if (type == typeof(Nullable<decimal>) || type == typeof(decimal))
            {
                return true;
            }
            if (type == typeof(string))
                return true;
            if (type.IsGenericType)
                return false;
            if (type.IsArray)
                return true;

            return false;
        }
        private static List<String> GetColumns(bool includeAll = false)
        {
            var columns = new List<String>();
            var properties = typeof(TSource).GetProperties();
            foreach (var v in properties)
            {
                if (IsValidPropertyType(v.PropertyType))
                {
                    if (!includeAll)
                    {
                        var attribute = v.GetCustomAttributes(false);
                        var hasAttribute = attribute.FirstOrDefault(a => a.GetType() == typeof(PKeyAttribute) || a.GetType() == typeof(NotMappedAttribute));
                        if (hasAttribute == null)
                        {
                            columns.Add(v.Name);
                        }
                    }
                    else
                    {
                        columns.Add(v.Name);
                    }

                }
            }
            return columns;
        }

        private static List<string> GetPrimaryKeyColumns()
        {
            var columns = new List<string>();
            var properties = typeof(TSource).GetProperties();
            foreach (var v in properties)
            {
                if (IsValidPropertyType(v.PropertyType))
                {
                    var attribute = v.GetCustomAttributes(false);
                    var hasAttribute = attribute.FirstOrDefault(a => a.GetType() == typeof(PKeyAttribute));
                    if (hasAttribute == null)
                    {
                        columns.Add(v.Name);
                    }
                }
                else
                {
                    columns.Add(v.Name);
                }

            }
            return columns;
        }
        private static string GetTableName()
        {
            var modelAttribute = ((TableAttribute[])typeof(TSource).GetCustomAttributes(typeof(TableAttribute), false)).FirstOrDefault();
            if (modelAttribute == null)
            {
                throw new Exception("No table name found, check attribute name");
            }
            var tableName = string.Format("{0}.{1}", modelAttribute.Schema, modelAttribute.Name);

            return tableName;
        }
        private static string TrimedExperssion(string expression)
        {
            return expression.Replace("AndAlso", "AND").Replace("OrElse", "OR").Replace("Convert", "").Replace("==", "=");
        }

    }
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string tableName, string schemaName = "dbo")
        {
            Name = tableName;
            Schema = schemaName;
        }
        public string Name { get; set; }
        public string Schema { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class PKeyAttribute : Attribute
    {

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class NotMappedAttribute : Attribute
    {

    }

}
