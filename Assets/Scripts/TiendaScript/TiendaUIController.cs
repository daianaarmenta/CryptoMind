using UnityEngine;
using UnityEngine.UIElements;

/*
Autor: Fernanda Pineda
Este código es para superponer la tienda al juego y permitir la compra de mejoras
*/
public class TiendaUIController : MonoBehaviour
{
    private Label monedasLabel;
    private Button botonVida;
    private Button botonMejora;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        root.schedule.Execute(() =>
        {
            monedasLabel = root.Q<Label>("monedasLabel");
            botonVida = root.Q<Button>("botonVida");
            botonMejora = root.Q<Button>("botonMejora");

            if (monedasLabel == null) Debug.LogError("❌ No se encontró 'monedasLabel'");
            if (botonVida == null) Debug.LogError("❌ No se encontró 'botonVida'");
            if (botonMejora == null) Debug.LogError("❌ No se encontró 'botonMejora'");

            if (GameManager.Instance != null && monedasLabel != null)
            {
                monedasLabel.text = GameManager.Instance.Monedas.ToString(); // ✅ CAMBIO AQUÍ
                Debug.Log("✅ Monedas actuales en tienda: " + GameManager.Instance.Monedas); // ✅ CAMBIO AQUÍ
            }
            else
            {
                Debug.LogError("❌ GameManager.Instance es NULL en la tienda.");
            }

            if (botonVida != null) botonVida.clicked += () => Comprar(100, "Vida");
            if (botonMejora != null) botonMejora.clicked += () => Comprar(150, "Mejora");

        }).ExecuteLater(1);
    }

    void Comprar(int precio, string nombre)
    {
        if (GameManager.Instance != null && GameManager.Instance.GastarMonedas(precio))
        {
            Debug.Log($"✅ Compra realizada: {nombre} por {precio} monedas.");
            ActualizarMonedasUI();
        }
        else
        {
            Debug.Log($"❌ No tienes suficientes monedas para {nombre}.");
        }
    }

    void ActualizarMonedasUI()
    {
        if (monedasLabel != null)
        {
            monedasLabel.text = GameManager.Instance.Monedas.ToString();  
        }
    }
}
