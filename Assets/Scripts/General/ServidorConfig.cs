/*Autor: Emiliano Plata Cardona
 * Descripción: Clase que gestiona la configuración del servidor para la API del juego.
 * Contiene la clase ServidorConfig que define la URL base y los endpoints específicos para las operaciones de inicio de sesión, registro, carga y guardado de progreso.
 */
public static class ServidorConfig
{
    // Base URL (puedes cambiar esta línea y todo se actualiza)
    public const string BaseUrl = "http://18.232.125.185:8080";

    // Endpoints específicos
    public static string Login => $"{BaseUrl}/unity/login";
    public static string Register => $"{BaseUrl}/unity/register";
    public static string CargarProgreso => $"{BaseUrl}/unity/cargar-progreso";
    public static string GuardarProgreso => $"{BaseUrl}/unity/guardar-progreso";
}

