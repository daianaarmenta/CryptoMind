/*Autor: Emiliano Plata Cardona
 * Descripción: Clase que gestiona la configuración del servidor para la API del juego.
 * Contiene la clase ServidorConfig que define la URL base y los endpoints específicos para las operaciones de inicio de sesión, registro, carga y guardado de progreso.
 */
public static class ServidorConfig
{
    // Base URL (puedes cambiar esta línea y todo se actualiza)
    public const string BaseUrl = "http://98.80.100.122:8080";

    // Endpoints específicos
    public static string Login => $"{BaseUrl}/unity/login";
    public static string Register => $"{BaseUrl}/unity/register";
    public static string GuardarProgreso => $"{BaseUrl}/unity/guardar-progreso";
    public static string GuardarProgresoYSalir=>$"{BaseUrl}/sesion/end";
    public static string PreguntaPorId(int id, string lang)=>$"{BaseUrl}/unity/pregunta/{lang}?id={id}";
    public static string RespuestaPregunta => $"{BaseUrl}/unity/pregunta/contestar";
}

