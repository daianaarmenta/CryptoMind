using UnityEngine;

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
