using System;
using UnityEngine;
/*Autor: Emiliano Plata Cardona
 * Descripción: Clase que gestiona la validación de los datos de registro del usuario en el juego.
 * Contiene métodos para validar campos vacíos, fechas y correos electrónicos.
 */
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
