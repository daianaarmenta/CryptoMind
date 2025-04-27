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
    private int respuestasCorrectas = 0;
    private MenuGameOver  menuGameOver;
    private bool tiempoAgotado = false;

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
        respuestasCorrectas = 0;
        botonSkip.gameObject.SetActive(false);
        botonSkip.onClick.AddListener(SkipMensaje);
        menuGameOver = FindFirstObjectByType<MenuGameOver>();
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
        string escena = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        bool esNivel5 = escena == "Nivel5" || escena.Contains("5");

        if (esNivel5 && cuentaRegresivaPregunta != null) 
        {

            StopCoroutine(cuentaRegresivaPregunta);
            audioSource.Stop();
            audioSource.loop = false;
        }

        if (esCorrecta)
        {
            if (esNivel5)
            {
                GameManager.Instance?.SumarMonedas(100);
                enemigo?.ResetearVelocidad();
                MostrarMensaje("Correct! +100 coins", Color.green);
                audioSource.PlayOneShot(audioClipCorrecto);
                respuestasCorrectas++;
            }
            else
            {
                GameManager.Instance?.SumarMonedas(100);
                MostrarMensaje("Correct! +100 coins", Color.green);
                audioSource.PlayOneShot(audioClipCorrecto);
            }

        }
        else
        {
            if (esNivel5)
            {
                enemigo?.Acelerar();
                MostrarMensaje("Incorrect!\nCorrect answer:\n" + respuestaCorrecta, Color.red);
                audioSource.PlayOneShot(audioClipIncorrecto);
            }
            else
            {
                MostrarMensaje("Incorrect!\n-1 life\nCorrect answer:\n" + respuestaCorrecta, Color.red);
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
        Time.timeScale = 1f;

        string escena = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Alien controlador = UnityEngine.Object.FindAnyObjectByType<Alien>();

        if (escena == "Nivel5" || escena.Contains("5"))
        {
            controlador?.AumentarCheckpoints(); //los manda al alien 

            if (controlador.checkpointTerminado >= controlador.cantidadCheckpoint)
            {
                if (respuestasCorrectas >= 10)
                {
                    enemigo?.Morir();
                }
                else
                {
                    menuGameOver.MostrarGameOver();
                }
            }
        } 
        else
        {
            controlador?.AumentarCheckpoints();
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
            MostrarMensaje("Time's up!", Color.red);
            audioSource.PlayOneShot(audioClipIncorrecto);

            yield return new WaitForSecondsRealtime(tiempoMensaje);

            panelPregunta.SetActive(false);
            Time.timeScale = 1f;
            enemigo?.Reanudar();
        }
    }


}