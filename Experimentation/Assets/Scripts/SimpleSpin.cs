using UnityEngine;

public class SimpleSpin : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        // Rotate around the Z-axis (Vector3.forward) steadily
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}