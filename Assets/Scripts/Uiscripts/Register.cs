using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class Register : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject registerMenuGame; 

    private UIDocument registerMenu;
    private Button botonRegister;
    private Button regresarEscene;
    private TextField nameUser;
    private TextField email;
    private TextField password;
    private TextField birthdate;
    private DropdownField countrys; 
    private DropdownField gender;
    private Label errorMessage;

    [Serializable]
    public class datosUsuario 
    {
        public string nombre;
        public string email;
        public string password;
        public string birthday;
        public string country;
        public string gender;
    }

    private void OnEnable()
    {
        registerMenu = GetComponent<UIDocument>();
        var root = registerMenu.rootVisualElement;

        botonRegister = root.Q<Button>("Register");
        regresarEscene = root.Q<Button>("botonReturn");

        nameUser = root.Q<TextField>("name");
        email = root.Q<TextField>("email");
        password = root.Q<TextField>("password");
        birthdate = root.Q<TextField>("birthday");
        countrys = root.Q<DropdownField>("pais");
        gender = root.Q<DropdownField>("gender");
        errorMessage = root.Q<Label>("errorMessage");
        errorMessage.text = "";


        password.isPasswordField = true; 
        nameUser.Q("unity-text-input").style.color = new Color(0, 0, 0);
        email.Q("unity-text-input").style.color = new Color(0, 0, 0);
        password.Q("unity-text-input").style.color = new Color(0, 0, 0);
        birthdate.Q("unity-text-input").style.color = new Color(0, 0, 0);

        List<Country> countriesData = PaisesLista();
        List<string> countryNames = countriesData.Select(c => c.name).ToList();
        countrys.choices = countryNames;
        
        if (countryNames.Count > 0)
        {
            countrys.value = countryNames[0];
        }
        
        gender.choices = new List<string>{
            "Female",
            "Male",
            "Non-binary",
            "Prefer not to say"
        };
        gender.value=gender.choices[0];
        
        regresarEscene.RegisterCallback<ClickEvent>(CambiarUI);
        botonRegister = root.Q<Button>("Register");
        botonRegister.clicked += EnviarDatos;

        nameUser.RegisterCallback<FocusOutEvent>(evt =>UnFocusedText(nameUser,"Name"));
        email.RegisterCallback<FocusOutEvent>(evt =>UnFocusedText(email, "Email"));
        password.RegisterCallback<FocusOutEvent>(evt =>UnFocusedText(password, "Password"));
        birthdate.RegisterCallback<FocusOutEvent>(OnBirthdateUnfocused);

    }

    private void EnviarDatos()
    {
        if (!AllFieldsValid())
        {
            MostrarMensaje("Some fields are empty", Color.red);
            Debug.LogWarning("Some fields are empty");
            return;
        }

        StartCoroutine(SubirDatos());
    }

    private IEnumerator SubirDatos()
    {
        datosUsuario datos = new datosUsuario{
            nombre = nameUser.value,
            email = email.value,
            password = password.value,
            birthday = FormatDateToMySQL(birthdate.value), 
            country = countrys.value,
            gender = gender.value
        };

        string datosJSON = JsonUtility.ToJson(datos);
        print(datosJSON);

        UnityWebRequest request = UnityWebRequest.Post("http://44.210.242.220:8080/unity/register", datosJSON, "application/json");
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success){
            Debug.Log("Mandado correctamente ");
            Debug.Log(request.downloadHandler.text);

            MostrarMensaje("Successful registration, changing to login", Color.green);

            yield return new WaitForSeconds(1f);
            registerMenuGame.SetActive(false);
            mainMenu.SetActive(true);
        } else{
            Debug.LogError("Failed to send: " + request.responseCode + "\n" + request.error);
            MostrarMensaje("Error registering" + request.downloadHandler.text, Color.red);
        }

        request.Dispose();
    }

    private void CambiarUI(ClickEvent evt){
        registerMenuGame.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void OnBirthdateUnfocused(FocusOutEvent evt){
        string raw = birthdate.value;

        if (DateTime.TryParseExact(
            raw,
            "dd/MM/yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out _))
        {

        }
        else
        {
            MostrarMensaje("Use the format dd/MM/yyy", Color.red);
        }
    }

    private void UnFocusedText(TextField field, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(field.value))
        {
            MostrarMensaje($"{fieldName} cannot be empty.", Color.red);
        }
    }

    [Serializable]
    public class Country
    {
        public string name;
    }

    [Serializable]
    public class CountryList
    {
        public List<Country> countries;
    }

    private List<Country> PaisesLista(){
        TextAsset file = Resources.Load<TextAsset>("countries");

        if(file == null){
            Debug.LogError("No se encontro el archivo");
            return new List<Country>();
        }

        string wrapped = file.text;
        CountryList lista = JsonUtility.FromJson<CountryList>(wrapped);
        return lista.countries;
    }

    private string FormatDateToMySQL(string input)
    {
        if (DateTime.TryParseExact(input, "dd/MM/yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out DateTime date))
        {
            return date.ToString("yyyy-MM-dd");
        }
        return "";
    }

    private bool AllFieldsValid()
    {
        return !string.IsNullOrWhiteSpace(nameUser.value)
            && !string.IsNullOrWhiteSpace(email.value)
            && !string.IsNullOrWhiteSpace(password.value)
            && !string.IsNullOrWhiteSpace(birthdate.value);
    }

    private void MostrarMensaje(string text, Color color)
    {
        errorMessage.style.color = new StyleColor(color);
        errorMessage.text = text;
        errorMessage.style.fontSize = 35;
    }
}
