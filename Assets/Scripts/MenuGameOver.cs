using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    private SaludPersonaje saludPersonaje;

    void Start()
    {
        saludPersonaje = GameObject.FindGameObjectWithTag("Player").GetComponent<SaludPersonaje>();
        saludPersonaje.MuerteJugador += ActiveMenu;
    }

    private void ActiveMenu(object sender, EventArgs e)
    {
        menuGameOver.SetActive(true);
    }

    public void Reiniciar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MenuNiveles(string nombre){
        SceneManager.LoadScene(nombre);
    }
}
