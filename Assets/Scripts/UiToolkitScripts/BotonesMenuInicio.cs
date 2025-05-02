using UnityEngine;
/*Autor: Emiliano Plata Cardona
    * Descripción: Clase que gestiona la interfaz de inicio de sesión y registro en el juego.
 */
public class BotonesMenuInicio : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu; 
    [SerializeField] private GameObject registerPanel;
    [SerializeField] private GameObject loginPanel;

    public void ShowLogin(){
        mainMenu.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void ShowRegister(){
        mainMenu.SetActive(false);
        registerPanel.SetActive(true);
    }
}
