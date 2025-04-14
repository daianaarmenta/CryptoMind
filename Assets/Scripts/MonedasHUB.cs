using UnityEngine;
using TMPro;

public class MonedasHUB : MonoBehaviour
{
    public TextMeshProUGUI monedasTexto;

    void Start()
    {
        if (monedasTexto == null)
        {
            Debug.LogError("❌ No se asignó el campo monedasTexto en el Inspector.");
        }
    }

    void Update()
    {
        if (monedasTexto != null)
        {
            monedasTexto.text = GameManager.Instance.Monedas.ToString();
        }
    }
}
