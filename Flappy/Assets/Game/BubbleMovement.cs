// 22/10/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float verticalSpeed = 2f; // Vitesse verticale (montée)
    public float horizontalSpeed = 1f; // Vitesse horizontale vers la gauche
    public float oscillationAmplitude = 0.5f; // Amplitude de l'oscillation horizontale
    public float oscillationFrequency = 1f; // Fréquence de l'oscillation horizontale

    private float startX;

    void Start()
    {
        // Enregistrer la position de départ en X pour l'oscillation
        startX = transform.position.x;
    }

    void Update()
    {
        // Mouvement vertical (montée)
        transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime, Space.World);

        // Mouvement horizontal vers la gauche
        transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime, Space.World);

        // Oscillation horizontale
        float offsetX = Mathf.Sin(Time.time * oscillationFrequency) * oscillationAmplitude;
        transform.position = new Vector3(startX + offsetX, transform.position.y, transform.position.z);

        // Détruire la bulle si elle sort de l'écran
        if (transform.position.y > Camera.main.orthographicSize * 2)
        {
            Destroy(gameObject);
        }
    }
}
