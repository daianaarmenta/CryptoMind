using UnityEngine;
using UnityEngine.UI; 

public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string localizationKey;
    private Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();
        if (LanguageManager.instance != null)
        {
            textComponent.text = LanguageManager.instance.GetText(localizationKey);
        }
    }
}
