using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BotonLoginBase : MonoBehaviour
{

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject loginMenuGame; 

    //[SerializeField] private GameObject gameManagerPrefab;
    private UIDocument loginMenu;
    private Button botonLogin, regresarEscene;
    private TextField email, password;
    private Label errorMessage, serverLabel, titulo;

    [Serializable]
    public class DatosLogin
    {
        public string emailDatos;
        public string passwordDatos; 
    }

    [Serializable]
    public class RespuestaLogin
    {
        public int id_usuario; 
        public string nombre;
        public int tokens;
        public int puntaje;
        public float daño_bala;
        public int costo_mejora;
    }

    void Start()
    {
        loginMenuGame.SetActive(false);
        mainMenu.SetActive(true);
        
    }

    private void OnEnable()
    {

        loginMenu = GetComponent<UIDocument>();
        var root = loginMenu.rootVisualElement;

        botonLogin = root.Q<Button>("botonLogin");
        regresarEscene = root.Q<Button>("botonRegresar");

        email = root.Q<TextField>("email");
        password = root.Q<TextField>("password");

        titulo = root.Q<Label>("tituloLogin");
        errorMessage = root.Q<Label>("errorMessage");
        errorMessage.text = "";
        serverLabel = root.Q<Label>("serverMessage");

        password.isPasswordField = true;
        email.Q("unity-text-input").style.color = new Color(0, 0, 0);
        password.Q("unity-text-input").style.color = new Color(0, 0, 0);

        regresarEscene.RegisterCallback<ClickEvent>(CambiarUI);
        botonLogin.clicked += EnviarDatos;

        TranslateUI();
    }

    private void EnviarDatos()
    {
        if (!AllFieldsValidLogin())
        {
            MostrarMensaje(Color.red, "error_empty_fields");
            Debug.LogWarning("Some fields are empty");
            return;
        }

        StartCoroutine(EnviarLoginJson());
        
    }

    private IEnumerator EnviarLoginJson()
    {
        DatosLogin datos = new DatosLogin{
            emailDatos = email.value,
            passwordDatos = password.value
        };

        string datosJSON = JsonUtility.ToJson(datos);
        print(datosJSON);

        UnityWebRequest request = UnityWebRequest.Post("http://44.210.242.220:8080/unity/login", datosJSON, "application/json");
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success){
            Debug.Log("Login correcto");
            Debug.Log("Respuesta: " + request.downloadHandler.text);

            /*if (GameManagerBase.Instance == null)
            {
                Instantiate(gameManagerPrefab);
                yield return null; // Esperar un frame por seguridad
            }*/

            RespuestaLogin datosUsuarios = JsonUtility.FromJson<RespuestaLogin>(request.downloadHandler.text);
           /* GameManagerBase.Instance.GuardarUsuario(datosUsuarios.id_usuario);
            GameManagerBase.Instance.GuardarNombreUsuario(datosUsuarios.nombre);
            GameManagerBase.Instance.SetCoinsFromServer(datosUsuarios.tokens);
            GameManagerBase.Instance.SetScoreFromServer(datosUsuarios.puntaje);
            GameManagerBase.Instance.SetBulletUpgradeFromServer(datosUsuarios.daño_bala, datosUsuarios.costo_mejora);*/

            MostrarMensaje( Color.green, "login_success");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Menu_juego");
        }
        else
        {
            MostrarMensaje(Color.red,"login_failed");
            serverLabel.text = request.downloadHandler.text;
            Debug.LogError("Login fallido: " + request.responseCode + " " + request.error);
        }

        request.Dispose();
    }

    private void CambiarUI(ClickEvent evt){
        loginMenuGame.SetActive(false);
        mainMenu.SetActive(true);
    }

    private bool AllFieldsValidLogin()
    {
        return !string.IsNullOrWhiteSpace(email.value)
            && !string.IsNullOrWhiteSpace(password.value);
    }


    private void MostrarMensaje(Color color, string label)
    {
        errorMessage.style.color = new StyleColor(color);
        errorMessage.text = LanguageManager.instance.GetText(label);
        errorMessage.style.fontSize = 35;
    }


    private void TranslateUI()
    {
        // Titles and labels
        titulo.text = LanguageManager.instance.GetText("login_title");
        email.label = LanguageManager.instance.GetText("email_label");
        password.label = LanguageManager.instance.GetText("password_label");

        // Button text
        botonLogin.text = LanguageManager.instance.GetText("login_button");

        email.textEdition.placeholder = LanguageManager.instance.GetText("email_placeholder");
        password.textEdition.placeholder = LanguageManager.instance.GetText("password_placeholder");

    }
}
