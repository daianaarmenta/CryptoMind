using System.Collections.Generic;
using UnityEngine;
using System.IO;
/*Autor: Emiliano Plata Cardona
 * Descripción: Clase que gestiona la carga y el manejo de archivos de idioma para la localización del juego.
 * Permite cargar archivos JSON con traducciones y acceder a las cadenas localizadas.
 */
[System.Serializable]
public class LanguageData
{
    public List<LocalizationItem> items;
}

[System.Serializable]
public class LocalizationItem
{
    public string key;
    public string value;
}

public class LanguageManager : MonoBehaviour
{
    private static LanguageManager _instance;
    public static LanguageManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<LanguageManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("LanguageManager");
                    _instance = obj.AddComponent<LanguageManager>();
                    DontDestroyOnLoad(obj);
                    
                    Debug.Log("LanguageManager created dynamically.");
                    _instance.LoadSystemLanguage();
                }
            }
            return _instance;
        }
    }

    private Dictionary<string, string> localizedText;
    private string currentLanguage = "en";

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; 
            DontDestroyOnLoad(gameObject); 

            Debug.Log("LanguageManager Awake called.");
            LoadSystemLanguage();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void LoadSystemLanguage()
    {
        string lang = GetSystemLanguage(); // Get the system language
        LoadLocalizedText(lang);
    }

    public string GetSystemLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Spanish:
                return "es";
            case SystemLanguage.English:
                return "en";
            default:
                return "en"; 
        }
    }

    private void LoadLocalizedText(string languageCode)
    {
        string path = Path.Combine(Application.streamingAssetsPath, languageCode + ".json");

        if (File.Exists(path))
        {
            Debug.Log("Found language file: " + path);

            string jsonData = File.ReadAllText(path);
            Debug.Log("Loaded JSON data: " + jsonData);

            LanguageData data = JsonUtility.FromJson<LanguageData>(jsonData);

            if (data != null && data.items != null)
            {
                Debug.Log($"Successfully parsed {data.items.Count} localization entries.");
                localizedText = new Dictionary<string, string>(); // Initialize the dictionary

                foreach (LocalizationItem item in data.items) 
                {
                    localizedText[item.key] = item.value;
                }
            }
            else
            {
                Debug.LogError("Failed to parse language JSON.");
            }
        }
        else
        {
            Debug.LogError("Localization file not found: " + path);
        }
    }

    public string GetText(string key)
    {
        if (localizedText != null && localizedText.ContainsKey(key))
            return localizedText[key];

        return key;
    }

    public string GetFormattedText(string key, params object[] args)
    {
        string raw = GetText(key); 
        return string.Format(raw, args);
    }

    public void SetLanguage(string languageCode)
    {
        currentLanguage = languageCode;
        LoadLocalizedText(languageCode);
    }
}
