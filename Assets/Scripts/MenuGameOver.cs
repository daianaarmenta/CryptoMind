using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    private SaludPersonaje saludPersonaje;

    [Obsolete]
    void Start()
    {
        saludPersonaje = GameObject.FindGameObjectWithTag("Player").GetComponent<SaludPersonaje>();
        saludPersonaje.MuerteJugador += ActiveMenu;
    }

    [Obsolete]
    private void ActiveMenu(object sender, EventArgs e)
    {
        menuGameOver.SetActive(true);
        Rigidbody2D rb = saludPersonaje.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static; // Detiene todo movimiento y fuerzas
            Debug.Log("⛔ Movimiento del jugador congelado.");
        }
    }

    public void Reiniciar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MenuNiveles(string nombre){
        SceneManager.LoadScene(nombre);
    }
}
