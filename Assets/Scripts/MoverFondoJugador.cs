using UnityEngine;

public class MoverFondoJugador : MonoBehaviour
{
    [SerializeField] private Vector2 velocidadMovimiento;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D jugadorRB; // Speed of the background movement
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        jugadorRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        offset = jugadorRB.linearVelocity.x * 0.1f * velocidadMovimiento * Time.deltaTime;
        material.mainTextureOffset += offset;
        
    }
}
