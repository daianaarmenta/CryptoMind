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

        // Espera un frame para asegurarse de que el UI esté listo
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
                ActualizarMonedasUI(); // ✅ Mostramos monedas al abrir tienda
                Debug.Log("✅ Monedas actuales en tienda: " + GameManager.Instance.Monedas);
            }
            else
            {
                Debug.LogError("❌ GameManager.Instance es NULL en la tienda.");
            }

            if (botonVida != null)
                botonVida.clicked += () => Comprar(100, "Vida");

            if (botonMejora != null)
                botonMejora.clicked += () => Comprar(150, "Mejora");

        }).ExecuteLater(1); // Se ejecuta tras 1 frame
    }

    void Comprar(int precio, string nombre)
    {
        // ⚠️ Verificar si intenta comprar vida sin necesitarla
        if (nombre == "Vida" && !GameManager.Instance.PuedeComprarVida())
        {
            Debug.Log("❌ Ya tienes el máximo de vidas. No puedes comprar más.");
            return;
        }

        if (GameManager.Instance != null && GameManager.Instance.GastarMonedas(precio))
        {
            Debug.Log($"✅ Compra realizada: {nombre} por {precio} monedas.");

            if (nombre == "Vida")
            {
                GameManager.Instance.ComprarVida();
                // (Opcional) Aquí podrías actualizar un HUD visual de vidas
            }

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

            // (Opcional) Desactiva botón si ya tienes 5 vidas
            if (botonVida != null)
            {
                botonVida.SetEnabled(GameManager.Instance.PuedeComprarVida());
            }
        }
    }

    // ✅ Opcional: si quieres que siempre esté actualizado en tiempo real
    void Update()
    {
        ActualizarMonedasUI();
    }
}
