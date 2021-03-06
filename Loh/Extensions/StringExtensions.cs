﻿using System;
using System.Globalization;

namespace Loh.Extensions
{
    public static class StringExtensions
    {
        public static string CapitalizeFirstLetter(this String input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) +
                   input.Substring(1, input.Length - 1).ToLower(CultureInfo.CurrentCulture);
        }
    }
}
