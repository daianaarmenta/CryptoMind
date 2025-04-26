using System;
using UnityEngine;
using UnityEngine.UIElements;

public class InfoBoton : MonoBehaviour
{
    [SerializeField] GameObject seleccionNivel;
    [SerializeField] GameObject infoUI;
    private UIDocument menu;
    private Button regresar;
    void Start()
    {
        seleccionNivel.SetActive(true);
        infoUI.SetActive(false);
    }
    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;

        regresar = root.Q<Button>("Return");

        regresar.RegisterCallback<ClickEvent> (CambiarUI);
    }

    private void CambiarUI(ClickEvent evt)
    {
        seleccionNivel.SetActive(true);
        infoUI.SetActive(false);
    }
}
