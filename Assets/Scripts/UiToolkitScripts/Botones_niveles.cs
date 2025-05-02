using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;
/*Autor: Emiliano Plata Cardona, Raúl Maldonado Pineda 
    * Descripción: Clase que gestiona la interfaz de selección de niveles en el juego.
 */
public class Botones_niveles : MonoBehaviour
{
    [SerializeField] GameObject seleccionNivel;
    [SerializeField] GameObject infoUI;
    private UIDocument menu; // Objeto de la UI en la escena
    private Button nivel0;    
    private Button nivel1;    
    private Button nivel2;
    private Button nivel3;
    private Button nivel4;
    private Button nivel5;
    private Button cerrar, info;
    private Label titulo;

    void Start()
    { 
        seleccionNivel.SetActive(true);
        infoUI.SetActive(false);
    }

    void OnEnable()
    {
        menu = GetComponent<UIDocument>(); // Obtener el documento UI
        var root = menu.rootVisualElement; 

        // Buscar los botones en el UXML por su nombre
        nivel0 = root.Q<Button>("n0");
        nivel1 = root.Q<Button>("n1");
        nivel2 = root.Q<Button>("n2"); 
        nivel3 = root.Q<Button>("n3");
        nivel4 = root.Q<Button>("n4");
        nivel5 = root.Q<Button>("n5");
        titulo = root.Q<Label>("Titulo");
        cerrar = root.Q<Button>("botonCerrar");
        info = root.Q<Button>("botonInfo");

        // Registrar eventos de clic
        nivel0.RegisterCallback<ClickEvent, string>(CambiarEscena, "Cutscene 0");
        nivel1.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel1");
        nivel2.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel2");
        nivel3.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel3");
        nivel4.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel4");
        nivel5.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel5");
        info.clicked += CambiarUIInfo; 
        cerrar.clicked += GuardarProgresoYSalir;

        TranslateUI();
    }

    private void CambiarUIInfo()
    {
        seleccionNivel.SetActive(false); // Ocultar el menú de selección de niveles
        infoUI.SetActive(true); // Mostrar la UI de información
    }

    private void GuardarProgresoYSalir()
    {
        Debug.Log("Guardado progreso en el servido");
        StartCoroutine(EnviarYSalir());
    }

    private IEnumerator EnviarYSalir()
    {
        var datos = new ProgresoJugador 
        {
            id_usuario = GameManagerBase.Instance.idUsuario,
            nombre = GameManagerBase.Instance.nombreUsuario,
            tokens = GameManagerBase.Instance.Monedas,
            puntaje = GameManagerBase.Instance.Puntaje,
            daño_bala = GameManagerBase.Instance.DañoBala,
            costo_mejora = GameManagerBase.Instance.CostoMejoraBala
        };

        string json = JsonUtility.ToJson(datos);

        UnityWebRequest request = new UnityWebRequest(ServidorConfig.GuardarProgresoYSalir, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Progreso guardado y sesion cerrada");
        }
        else 
        {
            Debug.Log("Error al guardar el porgreso");
        }

        request.Dispose();

        GameManagerBase.Instance.CerrarSesion();

    #if UNITY_WEBGL
        // En WebGL no puedes usar Application.Quit(), redireccionamos a la escena inicial (login)
        SceneManager.LoadScene("MenuInicio");
    #else
        Application.Quit(); // Para versiones de escritorio
    #endif
    }

    private void CambiarEscena(ClickEvent evt, string escena)
    {
        
        if (GameManagerBase.Instance != null)
        {
            GameManagerBase.Instance.ReiniciarVidas(); // Reiniciar vidas al cambiar de nivel
            Debug.Log("Vidas reiniciadas para el siguiente nivel.");
        }

        SceneManager.LoadScene(escena);
    }

    private void TranslateUI()
    {
        // Titles and labels
        titulo.text = LanguageManager.instance.GetText("select_level"); // "Selecciona un nivel"
    }
}
