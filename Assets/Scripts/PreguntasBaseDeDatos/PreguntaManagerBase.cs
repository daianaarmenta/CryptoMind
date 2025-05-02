using System;
using System.Collections;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
/*Autores:  Daiana Andrea Armenta Maya
            María Fernanda Pineda Pat 
            Emiliano Plata Cardona 
 * Descripción: Clase que gestiona las preguntas y respuestas del juego.
 * Controla la carga de preguntas desde la base de datos, la presentación de preguntas y respuestas,
 * y la verificación de respuestas correctas o incorrectas.
 */

public class PreguntaManagerBase : MonoBehaviour
{
    public static PreguntaManagerBase instance;

    [Header("Nivel 5")]
    [SerializeField] private EnemigoSeguidorNivel5 enemigo;
    [SerializeField] private float tiempoMaximoRespuesta = 15f;
    private Coroutine cuentaRegresivaPregunta;
    private int respuestasCorrectas = 0;
    private MenuGameOver  menuGameOver;
    private bool tiempoAgotado = false;
    public GameObject panelMensajeFinal;
    public TextMeshProUGUI textoResultadoFinal;
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

    public IEnumerator CargarPreguntaPorIdWeb(int id)
    {
        yield return new WaitForSeconds(0.2f);

        string lang = LanguageManager.instance.GetSystemLanguage();


        UnityWebRequest request = UnityWebRequest.Get(ServidorConfig.PreguntaPorId(id, lang));
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            PreguntaData pregunta = JsonUtility.FromJson<PreguntaData>(request.downloadHandler.text);
            MostrarPregunta(pregunta);
        }
        else
        {
            Debug.LogError($"Error al cargar pregunta con ID {id} en idioma {lang}: {request.error} " );
        }
    }

    
    private void Start()
    {
        respuestasCorrectas = 0;
        botonSkip.gameObject.SetActive(false);
        botonSkip.onClick.AddListener(SkipMensaje);
        menuGameOver = FindFirstObjectByType<MenuGameOver>();
    }

    /*public IEnumerator CargarPreguntaPorIdJSON(int id)
    {
        string lang = LanguageManager.instance.GetSystemLanguage();
        string fileName = lang == "es" ? "preguntas_es.json" : "preguntas_mock_completo.json";
        string path = LanguageManager.instance.GetLanguageFilePath(fileName);

        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("❌ Error al cargar preguntas: " + request.error);
            yield break;
        }

        PreguntaListWrapper wrapper = JsonUtility.FromJson<PreguntaListWrapper>(request.downloadHandler.text);

        string escena = SceneManager.GetActiveScene().name;
        if (escena == "Nivel5" || escena.Contains("5"))
        {
            int idRandom = UnityEngine.Random.Range(0, wrapper.items.Count);
            PreguntaData preguntaAleatoria = wrapper.items[idRandom];
            MostrarPregunta(preguntaAleatoria);
        }
        else
        {
            PreguntaData pregunta = wrapper.items.Find(p => p.pregunta.id_pregunta == id);
            MostrarPregunta(pregunta);
        }
    }*/



    private void MostrarPregunta(PreguntaData actual)
    {
        preguntaTextoUI.text = actual.pregunta.texto_pregunta;
        Time.timeScale = 0f; //pausa el tiempo del juego
        panelPregunta.SetActive(true);

        string escena = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (escena == "Nivel5" || escena.Contains("5"))
        {
            enemigo?.Detener();
            if (cuentaRegresivaPregunta != null) 
            {
                StopCoroutine(cuentaRegresivaPregunta);
            }

            mensajeRespuesta.SetActive(false);
            tiempoAgotado = false;
            cuentaRegresivaPregunta = StartCoroutine(TemporizadorNivel5(actual));
            MostrarOpciones(actual, ComprobarRespuesta);
        }
        else
        {
            MostrarOpciones(actual, ComprobarRespuesta);
        }

    }

    private void MostrarOpciones(PreguntaData actual, Action<PreguntaData, int> comprobarRespuesta)
    {
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
        string escena = SceneManager.GetActiveScene().name;
        bool esNivel5 = escena == "Nivel5" || escena.Contains("5");

        RespuestaJugador respuesta = new RespuestaJugador
        {
            id_usuario = GameManagerBase.Instance.idUsuario,
            id_pregunta = actual.pregunta.id_pregunta,
            id_opcion = actual.opciones[seleccion].id_opcion,
            es_correcta = esCorrecta,
            id_nivel = actual.pregunta.id_nivel
        };

        StartCoroutine(EnviarRespuestaAlServidor(respuesta));

        if (esNivel5 && cuentaRegresivaPregunta != null) //verifica que sea el nivel 5 y que la cuenta regresiva no sea nula
        {

            StopCoroutine(cuentaRegresivaPregunta);
            audioSource.Stop();
            audioSource.loop = false;
        }

        if (esCorrecta)
        {
            if (esNivel5)
            {
                GameManagerBase.Instance?.SumarMonedas(100);
                enemigo?.ResetearVelocidad();
                MostrarMensaje(LanguageManager.instance.GetText("correct_reward"), Color.green);
                audioSource.PlayOneShot(audioClipCorrecto);
                respuestasCorrectas++;
                Debug.Log("Respuestas correctas: " + respuestasCorrectas);
            }
            else
            {
                GameManagerBase.Instance?.SumarMonedas(100);
                MostrarMensaje(LanguageManager.instance.GetText("correct_reward"), Color.green);
                audioSource.PlayOneShot(audioClipCorrecto);
            }

        }
        else
        {
            if (esNivel5)
            {
                enemigo?.Acelerar();
                MostrarMensaje(LanguageManager.instance.GetText("incorrect_no_life") + respuestaCorrecta, Color.red);
                audioSource.PlayOneShot(audioClipIncorrecto);
            }
            else
            {
                MostrarMensaje(LanguageManager.instance.GetText("incorrect_with_life") + respuestaCorrecta, Color.red);
                audioSource.PlayOneShot(audioClipIncorrecto);
                SaludPersonaje.instance?.PerderVida();
            }
        }
        if (esNivel5)
        {
            enemigo?.Reanudar(); 
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
        {
            StopCoroutine(esperaOcultarMensaje);
        }

        mensajeRespuesta.SetActive(false);
        botonSkip.gameObject.SetActive(false);
        panelPregunta.SetActive(false);
        Time.timeScale = 1f; //se reanuda el tiempo del juego 

        string escena = SceneManager.GetActiveScene().name;
        Alien controlador = FindAnyObjectByType<Alien>();

        if (escena == "Nivel5" || escena.Contains("5"))
        {
            controlador?.AumentarCheckpoints(); //aumenta los checkpoints desde el alien

            if (controlador.checkpointTerminado >= controlador.cantidadCheckpoint)
            {
                StartCoroutine(MostrarFinalyDecidir());
            }
        } 
        else
        {
            controlador?.AumentarCheckpoints();
        }
    }

    private IEnumerator MostrarFinalyDecidir()
    {
        panelMensajeFinal.SetActive(true);
        string resultado = respuestasCorrectas >= 10 ? LanguageManager.instance.GetText("did_it") : LanguageManager.instance.GetText("failed");

        textoResultadoFinal.text = LanguageManager.instance.GetFormattedText(
            "quiz_result",
            resultado,
            respuestasCorrectas
        );


        yield return new WaitForSecondsRealtime(4f); // Espera 4 segundos

        panelMensajeFinal.SetActive(false);

        if (respuestasCorrectas >= 10)
        {
            enemigo?.Morir();
        }
        else
        {
            menuGameOver.MostrarGameOver();
        }
    }
    private IEnumerator OcultarMensaje()
    {
        yield return new WaitForSecondsRealtime(tiempoMensaje);
        SkipMensaje();
    }

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
            MostrarMensaje(LanguageManager.instance.GetText("times_up"), Color.red);
            audioSource.PlayOneShot(audioClipIncorrecto);

            yield return new WaitForSecondsRealtime(tiempoMensaje);

            panelPregunta.SetActive(false);
            Time.timeScale = 1f; //se reanuda el tiempo del juego
            enemigo?.Reanudar();
        }
    }

    private IEnumerator EnviarRespuestaAlServidor(RespuestaJugador respuesta)
    {
        string json = JsonUtility.ToJson(respuesta);

        UnityWebRequest request = new UnityWebRequest(ServidorConfig.RespuestaPregunta, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Respuesta enviada");
        }
        else
        {
            Debug.LogError("Error al enviar: " + request.error);
        }

        request.Dispose();

    }

}