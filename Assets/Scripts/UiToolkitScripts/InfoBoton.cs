using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InfoBoton : MonoBehaviour
{
    [SerializeField] GameObject seleccionNivel;
    [SerializeField] GameObject infoUI;
    private UIDocument menu;
    private Button regresar;

    private Label titulo, texto;
    void Start()
    {
        seleccionNivel.SetActive(true);
        infoUI.SetActive(false);
    }
    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;

        titulo = root.Q<Label>("Titulo");
        texto = root.Q<Label>("Texto");

        regresar = root.Q<Button>("Return");

        regresar.RegisterCallback<ClickEvent> (CambiarUI);

        TranslateUI();
    }

    private void CambiarUI(ClickEvent evt)
    {
        seleccionNivel.SetActive(true);
        infoUI.SetActive(false);
    }
    
    private void TranslateUI()
    {
        // Titles and labels
        titulo.text = LanguageManager.instance.GetText("about_cryptochicks_title");
        texto.text = LanguageManager.instance.GetText("about_cryptochicks_description");
    }
}
