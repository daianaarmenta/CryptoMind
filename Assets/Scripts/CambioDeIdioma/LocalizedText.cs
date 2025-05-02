using UnityEngine;
using UnityEngine.UI; // For legacy Text
using TMPro;          // For TextMeshPro

/*Autor: Emiliano Plata Cardona
Descripción: Este script se encarga de localizar y asignar texto en UI, 
   compatible tanto con UnityEngine.UI.Text como con TextMeshPro (TMP_Text).
*/
public class LocalizedTextUniversal : MonoBehaviour
{
    [SerializeField] private string localizationKey;
    private Text legacyText;      // Legacy UnityEngine.UI.Text
    private TMP_Text tmpText;     // TextMeshProUGUI or TMP_Text

    void Awake()
    {
        legacyText = GetComponent<Text>();
        tmpText = GetComponent<TMP_Text>();
    }

    void Start()
    {
         // Verifica que el LanguageManager esté disponible en la escena
        if (LanguageManager.instance == null)
        {
            Debug.LogWarning("LanguageManager not found.");
            return;
        }

        // Verifica que la clave de localización no esté vacía
        string localizedValue = LanguageManager.instance.GetText(localizationKey);

        if (legacyText != null)
        {
            legacyText.text = localizedValue; // Asigna el texto localizado al componente Text
        }
        else if (tmpText != null)
        {
            tmpText.text = localizedValue; 
        }
        else
        {
            Debug.LogError("LocalizedTextUniversal: No Text or TMP_Text component found on " + gameObject.name);
        }
    }
}


