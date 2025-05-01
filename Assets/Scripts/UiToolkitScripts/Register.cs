using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject registerMenuGame; 
    [SerializeField] private GameObject loginMenu; 

    private UIDocument registerMenu;
    private Button botonRegister, regresarEscene;

    private TextField nameUser, email, password;

    private DropdownField date, month, year, gender, countrys;
    private Label errorMessage, titulo;

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

        titulo = root.Q<Label>("tituloGeneral");
        nameUser = root.Q<TextField>("name");
        email = root.Q<TextField>("email");
        password = root.Q<TextField>("password");
        date = root.Q<DropdownField>("Date");
        month = root.Q<DropdownField>("Month");
        year = root.Q<DropdownField>("Year");
        countrys = root.Q<DropdownField>("pais");
        gender = root.Q<DropdownField>("gender");
        errorMessage = root.Q<Label>("errorMessage");
        errorMessage.text = "";

        password.isPasswordField = true; 
        nameUser.Q("unity-text-input").style.color = new Color(0, 0, 0);
        email.Q("unity-text-input").style.color = new Color(0, 0, 0);
        password.Q("unity-text-input").style.color = new Color(0, 0, 0);

        List<Country> countriesData = PaisesLista();
        List<string> countryNames = countriesData.Select(c => c.name).ToList();
        countrys.choices = countryNames;
        if (countryNames.Count > 0)
        {
            countrys.value = countryNames[0];
        }

        gender.choices = new List<string>{ "Female", "Male", "Non-binary", "Prefer not to say" };
        gender.value = gender.choices[0];

        date.choices = Enumerable.Range(1, 31).Select(d => d.ToString()).ToList();
        month.choices = Enumerable.Range(1, 12).Select(m => m.ToString()).ToList();
        year.choices = Enumerable.Range(1980, 100).Select(y => y.ToString()).ToList();

        date.value = date.choices[0];
        month.value = month.choices[0];
        year.value = year.choices[0];

        regresarEscene.RegisterCallback<ClickEvent>(CambiarUI);
        botonRegister.clicked -= EnviarDatos;
        botonRegister.clicked += EnviarDatos;

        TranslateUI();
    }

    private void EnviarDatos()
    {
        if (!AllFieldsValid())
        {
            MostrarMensaje("Some fields are empty", Color.red, "error_empty_fields");
            return;
        }
        else if (!EsEmailValido(email.value))
        {
            MostrarMensaje("Invalid email format.", Color.red, "error_invalid_email");
            return;
        }
        else if(!IsDateValid(date.value, month.value, year.value))
        {
            MostrarMensaje("Invalid birthdate. Please enter a valid date.", Color.red, "error_invalid_date");
            return;
        }

        StartCoroutine(SubirDatos());
    }

    private IEnumerator SubirDatos()
    {
        string fullDate = FormatDateToMySQL(date.value, month.value, year.value);

        datosUsuario datos = new datosUsuario{
            nombre = nameUser.value,
            email = email.value,
            password = password.value,
            birthday = fullDate,
            country = countrys.value,
            gender = gender.value
        };

        string datosJSON = JsonUtility.ToJson(datos);

        UnityWebRequest request = UnityWebRequest.Post(ServidorConfig.Register, datosJSON, "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            MostrarMensaje("Successful registration, changing to login", Color.green,"register_success");
            yield return new WaitForSeconds(2f);
            registerMenuGame.SetActive(false);
            loginMenu.SetActive(true);
        }
        else
        {

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                MostrarMensaje("Server is unreachable or check your internet connection", Color.red, "error_server_unreachable");
                registerMenuGame.SetActive(true);
                mainMenu.SetActive(false);
                loginMenu.SetActive(false);
            }
            else
            {
                MostrarMensaje("Error registering: " + request.downloadHandler.text, Color.red, "error_register_failed");
            }

            //Debug.LogError("⚠️ Register failed: " + request.error + " | Code: " + request.responseCode);
        }

        request.Dispose();
    }

    private void CambiarUI(ClickEvent evt)
    {
        registerMenuGame.SetActive(false);
        mainMenu.SetActive(true);
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

    private List<Country> PaisesLista()
    {
        TextAsset file = Resources.Load<TextAsset>("countries");
        if (file == null)
        {
            Debug.LogError("No se encontró el archivo");
            return new List<Country>();
        }

        CountryList lista = JsonUtility.FromJson<CountryList>(file.text);
        return lista.countries;
    }

    private string FormatDateToMySQL(string d, string m, string y)
    {
        if (int.TryParse(d, out int day) && int.TryParse(m, out int month) && int.TryParse(y, out int year))
        {
            DateTime date = new DateTime(year, month, day);
            return date.ToString("yyyy-MM-dd");
        }
        return "";
    }

    private bool AllFieldsValid()
    {
        return !string.IsNullOrWhiteSpace(nameUser.value)
            && !string.IsNullOrWhiteSpace(email.value)
            && !string.IsNullOrWhiteSpace(password.value)
            && !string.IsNullOrWhiteSpace(date.value)
            && !string.IsNullOrWhiteSpace(month.value)
            && !string.IsNullOrWhiteSpace(year.value);
    }

    private bool EsEmailValido(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private void MostrarMensaje(string text, Color color, string label)
    {
        errorMessage.style.color = new StyleColor(color);
        errorMessage.text = LanguageManager.instance.GetText(label);
        errorMessage.style.fontSize = 30;
    }

    private bool IsDateValid(string day, string month, string year)
    {
        if (int.TryParse(day, out int d) &&
            int.TryParse(month, out int m) &&
            int.TryParse(year, out int y))
        {
            try
            {
                DateTime dt = new DateTime(y, m, d);
                return true; // Valid
            }
            catch (ArgumentOutOfRangeException)
            {
                return false; // Invalid
            }
        }
        return false;
    }


    private void TranslateUI()
    {

        // Titles and labels
        titulo.text = LanguageManager.instance.GetText("register_title");
        nameUser.label = LanguageManager.instance.GetText("name_label");
        email.label = LanguageManager.instance.GetText("email_label");
        password.label = LanguageManager.instance.GetText("password_label");
        date.label = LanguageManager.instance.GetText("date_label");
        month.label = LanguageManager.instance.GetText("month_label");
        year.label = LanguageManager.instance.GetText("year_label");
        countrys.label = LanguageManager.instance.GetText("country_label");
        gender.label = LanguageManager.instance.GetText("gender_label");
        
        // Button text
        botonRegister.text = LanguageManager.instance.GetText("register_button");

        nameUser.textEdition.placeholder = LanguageManager.instance.GetText("name_placeholder");
        email.textEdition.placeholder = LanguageManager.instance.GetText("email_placeholder");
        password.textEdition.placeholder = LanguageManager.instance.GetText("password_placeholder");

        // Translate Gender dropdown choices
        gender.choices = new List<string> {
            LanguageManager.instance.GetText("gender_female"),
            LanguageManager.instance.GetText("gender_male"),
            LanguageManager.instance.GetText("gender_non_binary"),
            LanguageManager.instance.GetText("gender_prefer_not_say")
        };
        gender.value = gender.choices[0];
    }

}