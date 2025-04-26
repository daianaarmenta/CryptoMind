using System;
using UnityEngine;

public static class ValidacionRegistro 
{
    public static bool Vacio(string value){
        return string.IsNullOrEmpty(value);
    }

    public static string DiaValido(string date){
        if (DateTime.TryParseExact(date, "yyyy-MM-dd",
        System.Globalization.CultureInfo.InvariantCulture,
        System.Globalization.DateTimeStyles.None,
        out DateTime parsedDate)){
            return parsedDate.ToString("yyyy-MM-dd");
        }
        return "";
    }

    public static bool EmailValido(string email){
        return System.Text.RegularExpressions.Regex.IsMatch(
            email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase
            );
    }
}
