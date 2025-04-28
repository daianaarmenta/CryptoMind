using UnityEngine;
using UnityEngine.UI; // For legacy Text
using TMPro;          // For TextMeshPro

public class LocalizedTextUniversal : MonoBehaviour
{
    [SerializeField] private string localizationKey;
    private Text legacyText;      // Legacy UnityEngine.UI.Text
    private TMP_Text tmpText;     // TextMeshProUGUI or TMP_Text

    void Awake()
    {
        // Try to get both components
        legacyText = GetComponent<Text>();
        tmpText = GetComponent<TMP_Text>();
    }

    void Start()
    {
        if (LanguageManager.instance == null)
        {
            Debug.LogWarning("LanguageManager not found.");
            return;
        }

        string localizedValue = LanguageManager.instance.GetText(localizationKey);

        if (legacyText != null)
        {
            legacyText.text = localizedValue;
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


