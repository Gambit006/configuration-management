using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagement.Utility
{
    public static class ConvertType
    {
        public static object ConvertToType(string value, string type)
        {
            switch (type.ToLower())
            {
                case "string":
                    return value.ToString();
                case "int":
                    return Convert.ToInt32(value);
                case "bool":
                    return Convert.ToBoolean(Convert.ToInt32(value));
                case "double":
                    return Convert.ToDouble(value);
                default:
                    throw new Exception("Unknown type");
            }
        }
        public static string ConvertFromType(object value)
        {
            if (value is string)
            {
                return value.ToString();
            }
            else if (value is int)
            {
                return value.ToString();
            }
            else if (value is bool)
            {
                // Boole değerini 0 veya 1 olarak döndür
                return ((bool)value) ? "1" : "0";
            }
            else if (value is double)
            {
                return value.ToString();
            }
            else
            {
                throw new Exception("Unknown type");
            }
        }
    }
}
