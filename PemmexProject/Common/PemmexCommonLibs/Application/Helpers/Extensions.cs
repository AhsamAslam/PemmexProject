using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using PemmexCommonLibs.Domain.Common;

namespace PemmexCommonLibs.Application.Helpers
{
    [MemoryDiagnoser]
    public static class Extensions
    {
        [Benchmark]
        public static DateTime? ToDateTime2(this object date)
        {
            DateTime? d = null;
            DateTime d2;
            if (date is null or "")
                return null;

            bool success = DateTime.TryParse(date.ToString(), out d2);
            
            return success == true ?  d2 : d;
        }
        public static DateTime ToDateTime3(this object date)
        {
            DateTime d = new DateTime(1900,1,1);
            DateTime d2;
            if (date is null or "")
                return d;

            bool success = DateTime.TryParse(date.ToString(), out d2);

            return success == true ? d2 : d;
        }
        [Benchmark]
        public static string ToString2(this object data)
        {
            if (data == null)
                return string.Empty;

            return data.ToString().Trim();

        }
        [Benchmark]
        public static double ToDouble2(this object data)
        {
            if (data == null)
                return 0.0;

            return Convert.ToDouble(data.ToString().Trim());
        }
        [Benchmark]
        public static int ToInt2(this object data)
        {
            if (data == null)
                return 0;

            return Convert.ToInt32(data.ToString().Trim());
        }
        [Benchmark]
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        [Benchmark]
        public static IEnumerable<TreeItem<T>> GenerateTree<T, K>(
            this IEnumerable<T> collection,
            Func<T, K> idSelector,
            Func<T, K> parentIdSelector,
            K rootId = default(K))
        {
            foreach (var c in collection.Where(c => EqualityComparer<K>.Default.Equals(parentIdSelector(c), rootId)))
            {
                yield return new TreeItem<T>
                {
                    Item = c,
                    Children = collection.GenerateTree(idSelector, parentIdSelector, idSelector(c))
                };
            }
        }
        [Benchmark]
        public static TEnum ToEnum<TEnum>(this string strEnumValue, TEnum defaultValue)
        {
            if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
                return defaultValue;

            return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
        }
        [Benchmark]
        public static String convertEnumToString(this Enum eff)
        {
            return Enum.GetName(eff.GetType(), eff);
        }
        [Benchmark]
        public static string EnumDescription<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
        public static TEnum GetEnumValueFromDescription<TEnum>(this string description, TEnum defaultValue)
        {
            MemberInfo[] fis = typeof(TEnum).GetFields();

            foreach (var fi in fis)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0 && attributes[0].Description == description)
                    return (TEnum)Enum.Parse(typeof(TEnum), fi.Name);
            }

            return defaultValue;
        }
        [Benchmark]
        public static string FormatDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddHHmmss");
        }
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
