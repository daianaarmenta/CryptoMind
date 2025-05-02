using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;
/*Autor: Emiliano Plata Cardona
 * Descripción: Clase que gestiona la interfaz de registro del usuario en el juego.
 * Controla la interacción con los campos de entrada y la validación de datos.
 */
public class RegisterController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu; 
    [SerializeField] private GameObject registerMenuGame;

    private UIDocument registerMenu;
    private Button botonRegistrar, regresarEscena; 
    private TextField nombreUsuario, email, contraseña, cumpleaños;
    private DropdownField paises, genero;
    private Label nombreError, emailError, contraseñaError, cumpleañosError;

    private void OnEnable()
    {
        registerMenu = GetComponent<UIDocument>();
        var root = registerMenu.rootVisualElement;

        //Botones
        regresarEscena = root.Q<Button>("botonReturn");
        //Aqui deberia ir el boton de registro 

        //Textfield y errores
        nombreUsuario = root.Q<TextField>("name");
        nombreError = root.Q<Label>("nameError");

        email = root.Q<TextField>("email");
        emailError = root.Q<Label>("emailError");

        contraseña = root.Q<TextField>("password");
        contraseñaError = root.Q<Label>("passwordError");

        cumpleaños = root.Q<TextField>("birthday");
        cumpleañosError = root.Q<Label>("birthdayError");

        paises = root.Q<DropdownField>("pais");
        genero = root.Q<DropdownField>("gender");

        //Logica UI 
        Aspectos();
        LogicaDropdowns();
        Validacion();

        //Callbacks 
        regresarEscena.RegisterCallback<ClickEvent>(CambiarUI);
        //Boton para mandar los datos al servidor
    }

    private void Aspectos(){
        contraseña.isPasswordField = true;
        nombreUsuario.Q("unity-text-input").style.color = Color.black;
        email.Q("unity-text-input").style.color = Color.black;
        contraseña.Q("unity-text-input").style.color = Color.black;
        cumpleaños.Q("unity-text-input").style.color = Color.black;
    }

    private void LogicaDropdowns(){


        genero.choices = new List<string>{
            "Female",
            "Male",
            "Non-binary",
            "Prefer not to say"
        };
        genero.value=genero.choices[0];
    }

    private void Validacion(){

    }

    private void CambiarUI(ClickEvent evt){
        registerMenuGame.SetActive(false);
        mainMenu.SetActive(true);
    }
}
