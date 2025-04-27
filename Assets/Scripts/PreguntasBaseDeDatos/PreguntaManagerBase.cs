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
    [SerializeField] private TMP_Text textoContador;
    [SerializeField] private Button botonSkip;

    [Header("Timing")]
    public float tiempoMensaje = 2f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipCorrecto;
    [SerializeField] private AudioClip audioClipIncorrecto;
    [SerializeField] private AudioClip audioReloj;

    private Coroutine esperaOcultarMensaje;

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
    
    private void Start()
    {
        checkpointsPasados = 0;
        botonSkip.gameObject.SetActive(false);
        botonSkip.onClick.AddListener(SkipMensaje);
    }

    public IEnumerator CargarPreguntaPorId(int id)
    {
        yield return new WaitForSeconds(0.5f); // simulate delay

        TextAsset jsonFile = Resources.Load<TextAsset>("preguntas_mock_completo");
        if (jsonFile == null)
        {
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
            //Debug.Log("✅ Correcta");
            GameManager.Instance?.SumarMonedas(100);
            MostrarMensaje("Correct! +100 coins", Color.green);
            audioSource.PlayOneShot(audioClipCorrecto);
        }
        else
        {
            //Debug.Log("❌ Incorrecta");
            MostrarMensaje("Incorrect!\n-1 life\nCorrect answer:\n" + respuestaCorrecta, Color.red);
            audioSource.PlayOneShot(audioClipIncorrecto);
            SaludPersonaje.instance?.PerderVida();
        }

        esperaOcultarMensaje = StartCoroutine(OcultarMensaje());
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
        botonSkip.gameObject.SetActive(true);
    }

    private void SkipMensaje()
    {
        if (esperaOcultarMensaje != null)
            StopCoroutine(esperaOcultarMensaje);

        mensajeRespuesta.SetActive(false);
        botonSkip.gameObject.SetActive(false);
        panelPregunta.SetActive(false);
        Time.timeScale = 1f;

        string escena = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (escena == "Nivel5" || escena.Contains("5"))
        {
            if (checkpointsPasados >= totalCheckpoints)
                enemigo?.Morir();
        }
        else
        {
            alien controlador = UnityEngine.Object.FindAnyObjectByType<alien>();
            controlador?.AumentarCheckpoints();
        }
    }
    private IEnumerator OcultarMensaje()
    {
        yield return new WaitForSecondsRealtime(tiempoMensaje);
        SkipMensaje();

    }

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
        float tiempoRestante = tiempoMaximoRespuesta;
        tiempoAgotado = false;
        audioSource.clip = audioReloj;
        audioSource.loop = true;
        audioSource.Play();

        while (tiempoRestante > 0)
        {
            int segundos = Mathf.CeilToInt(tiempoRestante);
            textoContador.text = $"00:{segundos:00}";

            if(segundos <= 5)
            {
                textoContador.color = Color.red; // Cambia el color a rojo cuando quedan 5 segundos o menos
            }
            else
            {
                textoContador.color = Color.white; // Cambia el color a blanco en otros casos
            }

            yield return new WaitForSecondsRealtime(1f);
            tiempoRestante--;
        }

        textoContador.text = "00:00";
        audioSource.Stop();
        audioSource.loop = false;

        if (!tiempoAgotado)
        {
            tiempoAgotado = true;

            enemigo?.Acelerar();
            MostrarMensaje("Time's up!", Color.red);
            audioSource.PlayOneShot(audioClipIncorrecto);

            yield return new WaitForSecondsRealtime(tiempoMensaje);

            panelPregunta.SetActive(false);
            Time.timeScale = 1f;
            enemigo?.Reanudar();
        }
    }


    private void ComprobarRespuestaNivel5(PreguntaData actual, int seleccion)
    {
        if (cuentaRegresivaPregunta != null)
            StopCoroutine(cuentaRegresivaPregunta);
        audioSource.Stop();
        audioSource.loop = false;

        bool esCorrecta = actual.opciones[seleccion].es_correcta;
        string respuestaCorrecta = ObtenerRespuestaCorrectaTexto(actual);

        if (esCorrecta)
        {
            GameManager.Instance?.SumarMonedas(100);
            enemigo?.ResetearVelocidad();
            MostrarMensaje("Correct! +100 coins", Color.green);
            audioSource.PlayOneShot(audioClipCorrecto);
        }
        else
        {
            enemigo?.Acelerar();
            MostrarMensaje("Incorrect!\nCorrect answer:\n" + respuestaCorrecta, Color.red);
            audioSource.PlayOneShot(audioClipIncorrecto);
        }
        checkpointsPasados++;
        Debug.Log($"✅ Checkpoints completados: {checkpointsPasados}/{totalCheckpoints}");
        enemigo?.Reanudar(); 
        esperaOcultarMensaje = StartCoroutine(OcultarMensaje());
    }


}