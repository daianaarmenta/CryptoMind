using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PreguntaManagerBase : MonoBehaviour
{
    public static PreguntaManagerBase instance;
    [Header("Nivel 5")]
    [SerializeField] private EnemigoSeguidorNivel5 enemigo;
    [SerializeField] private float tiempoMaximoRespuesta = 15f;
    private Coroutine cuentaRegresivaPregunta;
    private int checkpointsPasados = 0;
    private int totalCheckpoints = 15;


    [Header("UI")]
    public TextMeshProUGUI preguntaTextoUI;
    public Button[] botonesRespuestas;
    public GameObject panelPregunta;
    public GameObject mensajeRespuesta;
    public TextMeshProUGUI mensajeTexto;

    [Header("Timing")]
    public float tiempoMensaje = 2f;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    /*public IEnumerator CargarPreguntaPorId(int id)
    {
        string url = "http://44.210.242.220:8080/unity/pregunta?id=" + id;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            PreguntaData pregunta = JsonUtility.FromJson<PreguntaData>(request.downloadHandler.text);
            MostrarPregunta(pregunta);
        }
        else
        {
            Debug.LogError("❌ Error al cargar pregunta con ID: " + id + " - " + request.error);
        }
    }*/

    /*public IEnumerator CargarPreguntaPorId(int id)
    {
        yield return new WaitForSeconds(0.5f); // simulate delay

        TextAsset jsonFile = Resources.Load<TextAsset>("preguntas_mock_completo");
        if (jsonFile == null)
        {
            Debug.LogError("No se encontró el archivo preguntas_mock.json en Resources.");
            yield break;
        }

        PreguntaListWrapper wrapper = JsonUtility.FromJson<PreguntaListWrapper>(jsonFile.text);
        PreguntaData pregunta = wrapper.items.Find(p => p.pregunta.id_pregunta == id);

        if (pregunta != null)
        {
            MostrarPregunta(pregunta);
        }
        else
        {
            Debug.LogWarning("Pregunta con ID " + id + " no encontrada en el JSON.");
        }
    }*/
    public IEnumerator CargarPreguntaPorId(int id)
{
    yield return new WaitForSeconds(0.5f); // simulate delay

    TextAsset jsonFile = Resources.Load<TextAsset>("preguntas_mock_completo");
    if (jsonFile == null)
    {
        Debug.LogError("No se encontró el archivo preguntas_mock.json en Resources.");
        yield break;
    }

    PreguntaListWrapper wrapper = JsonUtility.FromJson<PreguntaListWrapper>(jsonFile.text);
    PreguntaData pregunta = wrapper.items.Find(p => p.pregunta.id_pregunta == id);

    if (pregunta != null)
    {
        string escena = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (escena == "Nivel5" || escena.Contains("5"))
        {
            MostrarPreguntaNivel5(pregunta); // ⏱️ CON TIMER
        }
        else
        {
            MostrarPregunta(pregunta); // 🧠 NORMAL
        }
    }
    else
    {
        Debug.LogWarning("Pregunta con ID " + id + " no encontrada en el JSON.");
    }
}


    private void MostrarPregunta(PreguntaData actual)
    {
        preguntaTextoUI.text = actual.pregunta.texto_pregunta;
        Time.timeScale = 0f;
        panelPregunta.SetActive(true);

        for (int i = 0; i < botonesRespuestas.Length; i++)
        {
            if (i < actual.opciones.Count)
            {
                botonesRespuestas[i].gameObject.SetActive(true);
                botonesRespuestas[i].GetComponentInChildren<Text>().text = actual.opciones[i].texto_opcion;

                int index = i;
                botonesRespuestas[i].onClick.RemoveAllListeners();
                botonesRespuestas[i].onClick.AddListener(() => ComprobarRespuesta(actual, index));
            }
            else
            {
                botonesRespuestas[i].gameObject.SetActive(false);
            }
        }
    }

    private void ComprobarRespuesta(PreguntaData actual, int seleccion)
    {
        bool esCorrecta = actual.opciones[seleccion].es_correcta;
        string respuestaCorrecta = ObtenerRespuestaCorrectaTexto(actual);

        if (esCorrecta)
        {
            Debug.Log("✅ Correcta");
            GameManager.Instance?.SumarMonedas(100);
            MostrarMensaje("Correct! +100 coins", Color.green);
        }
        else
        {
            Debug.Log("❌ Incorrecta");
            MostrarMensaje("Incorrect!\n-1 life\nCorrect answer:\n" + respuestaCorrecta, Color.red);
            SaludPersonaje.instance?.PerderVida();
        }

        StartCoroutine(OcultarMensaje());
    }

    private string ObtenerRespuestaCorrectaTexto(PreguntaData pregunta)
    {
        foreach (var opcion in pregunta.opciones)
        {
            if (opcion.es_correcta)
                return opcion.texto_opcion;
        }
        return "Unknown";
    }

    private void MostrarMensaje(string mensaje, Color color)
    {
        mensajeRespuesta.SetActive(true);
        mensajeTexto.text = mensaje;
        mensajeTexto.color = color;
    }
    private IEnumerator OcultarMensaje()
{
    yield return new WaitForSecondsRealtime(tiempoMensaje);

    mensajeRespuesta.SetActive(false);
    panelPregunta.SetActive(false);
    Time.timeScale = 1f;

    string escena = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    if (escena == "Nivel5" || escena.Contains("5"))
    {
        checkpointsPasados++;

        int faltan = totalCheckpoints - checkpointsPasados;
        Debug.Log($"📍 Checkpoints: {checkpointsPasados}/{totalCheckpoints} — Te faltan {faltan} para eliminar al enemigo.");

        // 🟩 Si ya completaste todos los checkpoints, eliminar al enemigo
        if (checkpointsPasados >= totalCheckpoints)
        {
            if (enemigo != null)
            {
                enemigo.Morir();
            }
        }
    }

    if (escena != "Nivel5" && !escena.Contains("5"))
    {
        alien controlador = UnityEngine.Object.FindAnyObjectByType<alien>();
        if (controlador != null)
        {
            controlador.AumentarCheckpoints();
        }
        else
        {
            Debug.LogWarning("No se encontró una instancia de alien.");
        }
    }
}

    /*private IEnumerator OcultarMensaje()
    {
        yield return new WaitForSecondsRealtime(tiempoMensaje);

        mensajeRespuesta.SetActive(false);
        panelPregunta.SetActive(false);
        Time.timeScale = 1f;
        

        alien controlador = UnityEngine.Object.FindAnyObjectByType<alien>();
        if (controlador != null)
        {
            controlador.AumentarCheckpoints();
        }
        else
        {
            Debug.LogWarning("No se encontró una instancia de alien.");
        }
    }*/
private void MostrarPreguntaNivel5(PreguntaData actual)
{
    preguntaTextoUI.text = actual.pregunta.texto_pregunta;
    Time.timeScale = 0f;
    panelPregunta.SetActive(true);

    enemigo?.Detener();

    if (cuentaRegresivaPregunta != null)
        StopCoroutine(cuentaRegresivaPregunta);

    mensajeRespuesta.SetActive(false);
    tiempoAgotado = false;
    cuentaRegresivaPregunta = StartCoroutine(TemporizadorNivel5(actual));

    for (int i = 0; i < botonesRespuestas.Length; i++)
    {
        if (i < actual.opciones.Count)
        {
            botonesRespuestas[i].gameObject.SetActive(true);
            botonesRespuestas[i].GetComponentInChildren<Text>().text = actual.opciones[i].texto_opcion;

            int index = i;
            botonesRespuestas[i].onClick.RemoveAllListeners();
            botonesRespuestas[i].onClick.AddListener(() => ComprobarRespuestaNivel5(actual, index));
        }
        else
        {
            botonesRespuestas[i].gameObject.SetActive(false);
        }
    }
}
private bool tiempoAgotado = false;

private IEnumerator TemporizadorNivel5(PreguntaData actual)
{
    // tiempoAgotado = false;
    float tiempoRestante = tiempoMaximoRespuesta;

    while (tiempoRestante > 0)
    {
        yield return new WaitForSecondsRealtime(1f);
        tiempoRestante--;
    }

    if (!tiempoAgotado)
    {
        tiempoAgotado = true;

        enemigo?.Acelerar();
        MostrarMensaje("⏱️ Time's up!", Color.red);

        yield return new WaitForSecondsRealtime(tiempoMensaje);

        panelPregunta.SetActive(false);
        Time.timeScale = 1f;
        enemigo?.Reanudar();
    }
}

/*private IEnumerator TemporizadorNivel5(PreguntaData actual)
{
    float tiempoRestante = tiempoMaximoRespuesta;
    while (tiempoRestante > 0)
    {
        yield return new WaitForSecondsRealtime(1f);
        tiempoRestante--;
    }

    enemigo?.Acelerar();
    MostrarMensaje("Time's up", Color.red);

    yield return new WaitForSecondsRealtime(tiempoMensaje);
    panelPregunta.SetActive(false);
    Time.timeScale = 1f;
    enemigo?.Reanudar();
}*/

/*private void ComprobarRespuestaNivel5(PreguntaData actual, int seleccion)
{
    if (cuentaRegresivaPregunta != null)
        StopCoroutine(cuentaRegresivaPregunta);

    bool esCorrecta = actual.opciones[seleccion].es_correcta;
    string respuestaCorrecta = ObtenerRespuestaCorrectaTexto(actual);

    if (esCorrecta)
    {
        GameManager.Instance?.SumarMonedas(100);
        enemigo?.ResetearVelocidad();
        MostrarMensaje("✅ Correcta +100 monedas", Color.green);
    }
    else
    {
        SaludPersonaje.instance?.PerderVida();
        enemigo?.Acelerar();
        MostrarMensaje("❌ Incorrecta\nRespuesta: " + respuestaCorrecta, Color.red);
    }

    StartCoroutine(OcultarMensaje());
}*/

private void ComprobarRespuestaNivel5(PreguntaData actual, int seleccion)
{
    if (cuentaRegresivaPregunta != null)
        StopCoroutine(cuentaRegresivaPregunta);

    bool esCorrecta = actual.opciones[seleccion].es_correcta;
    string respuestaCorrecta = ObtenerRespuestaCorrectaTexto(actual);

    if (esCorrecta)
    {
        enemigo?.ResetearVelocidad();
        MostrarMensaje("Correct! +100 coins", Color.green);
    }
    else
    {
        enemigo?.Acelerar();
        MostrarMensaje("Incorrect!\nCorrect answer:\n" + respuestaCorrecta, Color.red);
    }

    enemigo?.Reanudar(); // 🟢 AQUI SE VUELVE A ACTIVAR

    StartCoroutine(OcultarMensaje());
}


}