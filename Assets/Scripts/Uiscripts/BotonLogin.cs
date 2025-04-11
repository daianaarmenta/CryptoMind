using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BotonLogin : MonoBehaviour
{

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject loginMenuGame; 
    private UIDocument loginMenu;
    private Button botonLogin;
    private TextField email;
    private TextField password;
    private Button regresarEscene;

    [Serializable]
    public class DatosLogin
    {
        public string emailDatos;
        public string passwordDatos; 
    }

    private void OnEnable()
    {

        loginMenu = GetComponent<UIDocument>();
        var root = loginMenu.rootVisualElement;

        botonLogin = root.Q<Button>("botonLogin");
        regresarEscene = root.Q<Button>("botonRegresar");

        email = root.Q<TextField>("email");
        password = root.Q<TextField>("password");

        password.isPasswordField = true;
        email.Q("unity-text-input").style.color = new Color(0, 0, 0);
        password.Q("unity-text-input").style.color = new Color(0, 0, 0);

        regresarEscene.RegisterCallback<ClickEvent>(CambiarUI);
        botonLogin.clicked += EnviarDatos;
    }

    private void EnviarDatos()
    {
        if (!AllFieldsValidLogin())
        {
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

        UnityWebRequest request = UnityWebRequest.Post("http://3.235.251.180:8080/unity/register", datosJSON, "application/json");
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success){
            Debug.Log("Login correcto");
            Debug.Log("Respuesta: " + request.downloadHandler.text);
            SceneManager.LoadScene("Menu_juego");
        }
        else
        {
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
}
    
