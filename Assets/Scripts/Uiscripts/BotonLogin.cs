using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BotonLogin : MonoBehaviour
{

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject loginMenuGame; 
    private UIDocument loginMenu;
    private Button botonLogin;
    private Button botonReturnPassword;
    private TextField email;
    private TextField password;
    private Button regresarEscene;

    private void OnEnable()
    {

        loginMenu = GetComponent<UIDocument>();
        var root = loginMenu.rootVisualElement;

        botonLogin = root.Q<Button>("botonLogin");
        botonReturnPassword = root.Q<Button>("botonRecuperar");
        regresarEscene = root.Q<Button>("botonRegresar");

        email = root.Q<TextField>("email");
        password = root.Q<TextField>("password");

        password.isPasswordField = true;
        email.Q("unity-text-input").style.color = new Color(0, 0, 0);
        password.Q("unity-text-input").style.color = new Color(0, 0, 0);

        regresarEscene.RegisterCallback<ClickEvent>(CambiarUI);
    }

    private void CambiarUI(ClickEvent evt){
        loginMenuGame.SetActive(false);
        mainMenu.SetActive(true);
    }
}
