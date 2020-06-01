using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Stocky.Common
{
    public static class Extensions
    {

        public static string TrimAndToLower(this string value)
        {
            return value?.Trim().ToLower() ?? "";
        }

        public static bool IsNull(this object value)
        {
            return value == null;
        }

        public static bool IsNullOrEmpty(this object value)
        {
            if (value == null || value == string.Empty) return true;
            return false;
        }

        public static bool IsNullOrZero(this long value)
        {
            if (value == null || value == 0) return true;
            return false;
        }

        public static bool IsNullOrZero<T>(this List<T> arr)
        {
            if (arr.IsNull() || arr.Count == 0)
            {
                return true;
            }
            return false;
        }

        private static string GetDescription(this object value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description;
        }

        public static string GetDescription(this Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description;
        }
    }
}
