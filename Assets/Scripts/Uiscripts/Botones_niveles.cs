using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Botones_niveles : MonoBehaviour
{
    private UIDocument menu; // Objeto de la UI en la escena
    private Button botonRegreso;
    private Button nivel0;    
    private Button nivel1;    
    private Button nivel2;
    private Button nivel3;
    private Button nivel4;
    private Button nivel5;
    private Button tienda;

    void OnEnable()
    {
        menu = GetComponent<UIDocument>(); // Obtener el documento UI
        var root = menu.rootVisualElement; 

        // Buscar los botones en el UXML por su nombre
        botonRegreso = root.Q<Button>("botonRegresar");
        nivel0 = root.Q<Button>("n0");
        nivel1 = root.Q<Button>("n1");
        nivel2 = root.Q<Button>("n2"); 
        nivel3 = root.Q<Button>("n3");
        nivel4 = root.Q<Button>("n4");
        tienda = root.Q<Button>("botonTienda");

        // Registrar eventos de clic
        botonRegreso.RegisterCallback<ClickEvent, string>(CambiarEscena, "MenuInicio");
        nivel0.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel0");
        nivel1.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel1");
        nivel2.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel2");
        nivel3.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel3");
        nivel4.RegisterCallback<ClickEvent, string>(CambiarEscena, "Nivel4");
        tienda.RegisterCallback<ClickEvent, string>(CambiarEscena, "Tienda");
    }

        public void Reiniciar()
    {
        Time.timeScale = 1f; 
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarVidas();
            Debug.Log("🔁 Vidas reiniciadas.");
        }
        else
        {
            Debug.LogWarning("⚠️ GameManager no encontrado al reiniciar.");
        }

        // 🔄 Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void CambiarEscena(ClickEvent evt, string escena)
    {
        Reiniciar();

        // Si va a la tienda, guardar la escena actual
        if (escena == "Tienda")
        {
            botonesTienda.previousScene = SceneManager.GetActiveScene().name;
            Debug.Log("📌 Escena anterior guardada: " + botonesTienda.previousScene);
        }

        SceneManager.LoadScene(escena);
    }
}
