using System.Collections.Generic;
/*Autor: Emiliano Plata Cardona
 * Descripción: Clase que gestiona la estructura de datos para las preguntas y opciones del juego.
 * Contiene las clases PreguntaBase, Opcion y PreguntaData que representan la información de las preguntas y sus opciones.
 */
[System.Serializable]

public class PreguntaBase
{
    public int id_pregunta;
    public int id_nivel;
    public string texto_pregunta;
    public int dificultad;
}

[System.Serializable]

public class Opcion
{
    public int id_opcion;
    public int id_pregunta;
    public string texto_opcion;
    public bool es_correcta;
}

[System.Serializable]

public class PreguntaData
{
    public PreguntaBase pregunta;
    public List<Opcion> opciones; 
}

[System.Serializable]
public class PreguntaListWrapper
{
    public List<PreguntaData> items;
}

[System.Serializable]
public class RespuestaJugador
{
    public int id_usuario;
    public int id_pregunta;
    public int id_opcion;
    public bool es_correcta;
    public int id_nivel;
}
