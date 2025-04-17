using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PreguntaManagerBase : MonoBehaviour
{
    public static PreguntaManagerBase instance;

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
            MostrarPregunta(pregunta);
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