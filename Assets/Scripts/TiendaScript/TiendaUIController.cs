using UnityEngine;
using UnityEngine.UIElements;
/*
Autor: Fernanda Pineda
Este codigo es para superponer la tienda al juego y permitir la compra de mejoras
*/
public class TiendaUIController : MonoBehaviour
{
    private Label monedasLabel;
    private Button botonVida;
    private Button botonMejora;
    private Button botonCarta;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        root.schedule.Execute(() =>
        {
            monedasLabel = root.Q<Label>("monedasLabel");
            botonVida = root.Q<Button>("botonVida");
            botonMejora = root.Q<Button>("botonMejora");
            botonCarta = root.Q<Button>("botonCarta");

            if (monedasLabel == null) Debug.LogError("❌ No se encontró 'monedasLabel'");
            if (botonVida == null) Debug.LogError("❌ No se encontró 'botonVida'");
            if (botonMejora == null) Debug.LogError("❌ No se encontró 'botonMejora'");
            if (botonCarta == null) Debug.LogError("❌ No se encontró 'botonCarta'");

            if (GameManager.Instance != null && monedasLabel != null)
            {
                monedasLabel.text = GameManager.Instance.PuntosTotales.ToString();
                Debug.Log("✅ Monedas actuales en tienda: " + GameManager.Instance.PuntosTotales);
            }
            else
            {
                Debug.LogError("❌ GameManager.Instance es NULL en la tienda.");
            }

            if (botonVida != null) botonVida.clicked += () => Comprar(100, "Vida");
            if (botonMejora != null) botonMejora.clicked += () => Comprar(150, "Mejora");
            if (botonCarta != null) botonCarta.clicked += () => Comprar(200, "Carta");

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
            monedasLabel.text = GameManager.Instance.PuntosTotales.ToString();
        }
    }
}
