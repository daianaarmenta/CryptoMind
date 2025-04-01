using UnityEngine;

public class MenuControllerEmi : MonoBehaviour
{
    [SerializeField]
    private GameObject canvasMenu;

    [SerializeField]
    private GameObject uiLogin;
    
    [SerializeField]
    private GameObject uiRegistro;

    public void OnLoginPressed(){
        canvasMenu.SetActive(false);
        uiLogin.SetActive(true);
    }

    public void OnRegisterPressed(){
        canvasMenu.SetActive(false);
        uiRegistro.SetActive(true);
    }
}
