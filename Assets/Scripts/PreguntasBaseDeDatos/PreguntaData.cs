using System.Collections.Generic;
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