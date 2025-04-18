using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Botones_niveles : MonoBehaviour
{
    private UIDocument menu; // Objeto de la UI en la escena
    private Button nivel0;    
    private Button nivel1;    
    private Button nivel2;
    private Button nivel3;
    private Button nivel4;
    private Button nivel5;
    private Button cerrar;

    void OnEnable()
    {
        menu = GetComponent<UIDocument>(); // Obtener el documento UI
        var root = menu.rootVisualElement; 

        // Buscar los botones en el UXML por su nombre
        nivel0 = root.Q<Button>("n0");
        nivel1 = root.Q<Button>("n1");
        nivel2 = root.Q<Button>("n2"); 
        nivel3 = root.Q<Button>("n3");
        nivel4 = root.Q<Button>("n4");
        cerrar = root.Q<Button>("botonCerrar");

        // Registrar eventos de clic
        nivel0.RegisterCallback<ClickEvent, string>(CambiarEscena, "Cutscene 0");
        nivel1.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel1");
        nivel2.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel2");
        nivel3.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel3");
        nivel4.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel4");
        cerrar.RegisterCallback<ClickEvent>(CerrarApp);
    }

    private void CerrarApp(ClickEvent evt)
    {
        Application.Quit();
        Debug.Log("Aplicacion cerrada =)");
    }

    private void CambiarEscena(ClickEvent evt, string escena)
    {

        SceneManager.LoadScene(escena);
    }
}
