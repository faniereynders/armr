using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_az
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string helper)
        {
            return string.IsNullOrEmpty(helper);
        }

        public static void SetDefault<T>(this T helper, T value)
        {
            if (helper == null)
            {
                helper = value;
            }
        }
    }
}
