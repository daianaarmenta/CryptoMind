/*Autor: Emiliano Plata Cardona
 * Descripción: Clase que gestiona la carga y el manejo de archivos de idioma para la localización del juego.
 * Permite cargar archivos JSON con traducciones y acceder a las cadenas localizadas.
 */
using System.Collections.Generic;
using UnityEngine;
using System; 
using System.IO;
using UnityEngine.Networking;
using System.Collections;

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
                    
                    //Debug.Log("✅ LanguageManager created dynamically.");
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

            //Debug.Log("✅ LanguageManager Awake called.");
            LoadSystemLanguage();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void LoadSystemLanguage()
    {
        string lang = GetSystemLanguage();
        StartCoroutine(LoadLocalizedText(lang));
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

    public IEnumerator LoadLocalizedText(string languageCode, Action onLoaded = null)
    {
        string fileName = languageCode + ".json";
        string path = Path.Combine(Application.streamingAssetsPath, fileName);

    #if UNITY_WEBGL && !UNITY_EDITOR
        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();
    #else
        string uri = "file://" + path;
        UnityWebRequest request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();
    #endif

        if (request.result == UnityWebRequest.Result.Success)
        {
            ProcessJson(request.downloadHandler.text);
            onLoaded?.Invoke();
        }
        else
        {
            //Debug.LogError("❌ Error al cargar archivo de idioma: " + request.error);
        }
    }

    private void ProcessJson(string jsonData)
    {
        LanguageData data = JsonUtility.FromJson<LanguageData>(jsonData);

        if (data != null && data.items != null)
        {
            localizedText = new Dictionary<string, string>();
            foreach (LocalizationItem item in data.items)
            {
                localizedText[item.key] = item.value;
            }
            //Debug.Log($"✅ {localizedText.Count} elementos de idioma cargados.");
        }
        else
        {
            //Debug.LogError("❌ Fallo al deserializar JSON de idioma.");
        }
    }

    public string GetLanguageFilePath(string fileName)
    {
    #if UNITY_WEBGL && !UNITY_EDITOR
        return Path.Combine(Application.streamingAssetsPath, fileName);
    #else
        return "file://" + Path.Combine(Application.streamingAssetsPath, fileName);
    #endif
    }
}
