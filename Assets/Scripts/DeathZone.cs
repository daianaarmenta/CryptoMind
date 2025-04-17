using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private MenuGameOver menuGameOver;

    void Start()
    {
        menuGameOver = FindFirstObjectByType<MenuGameOver>();
        if (menuGameOver == null)
        {
            Debug.LogWarning("⚠️ No se encontró MenuGameOver en la escena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && menuGameOver != null)
        {
            Debug.Log("☠️ Jugador cayó en la DeathZone");
            menuGameOver.MostrarGameOver(); // Este método debe ser público
        }
    }
}
