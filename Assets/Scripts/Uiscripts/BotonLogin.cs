using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BotonLogin : MonoBehaviour
{
    private UIDocument loginMenu;
    private Button botonLogin;
    private Button botonReturnPassword;
    private TextField email;
    private TextField password;

    private void OnEnable()
    {
        loginMenu = GetComponent<UIDocument>();
        var root = loginMenu.rootVisualElement;
        botonLogin = root.Q<Button>("botonLogin");
        botonReturnPassword = root.Q<Button>("botonRecuperar");
        email = root.Q<TextField>("email");
        password = root.Q<TextField>("password");

        password.isPasswordField = true;
        email.Q("unity-text-input").style.color = new Color(0, 0, 0);
        password.Q("unity-text-input").style.color = new Color(0, 0, 0);
    }
}
