using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Geschwindigkeit der Rotation in Grad pro Sekunde
    public Vector3 rotationSpeed = new Vector3(0, 50, 0);

    void Update()
    {
        // Rotiert das Objekt um seine eigene Achse (lokal)
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
    }
}