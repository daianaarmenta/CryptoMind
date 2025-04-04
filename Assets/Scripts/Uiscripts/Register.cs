using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;
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
    private Label birthdateError;
    

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
        birthdateError = root.Q<Label>("birthdayError");
        birthdateError.text = "";


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
        birthdate.RegisterCallback<FocusOutEvent>(OnBirthdateUnfocused);
    }

    private void CambiarUI(ClickEvent evt){
        registerMenuGame.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void OnBirthdateUnfocused(FocusOutEvent evt){
        string raw = birthdate.value;

        if (System.DateTime.TryParseExact(
            raw,
            "dd/MM/yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out _))
        {
            birthdateError.text = ""; 
        }
        else
        {
            birthdateError.text = "Use the format dd/MM/yyy";
        }
    }


    [System.Serializable]
    public class Country
    {
        public string name;
    }

    [System.Serializable]
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
}
