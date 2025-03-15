using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public static bool OnGround {get; private set;}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnGround = false;
    }
}
