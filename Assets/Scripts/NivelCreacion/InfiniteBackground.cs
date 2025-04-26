using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public float length;
    public Transform cam;
    [SerializeField] private float fixedZ = 5f; // Z value to keep it behind the camera

    private void Update()
    {
        if (cam.position.x > transform.position.x + length)
        {
            // Get new position but preserve locked Z
            Vector3 newPos = transform.position + new Vector3(length * 2f, 0, 0);
            newPos.z = fixedZ;
            transform.position = newPos;
        }
    }
}
