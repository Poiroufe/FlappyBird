// 22/10/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab; // Le prefab de la bulle
    public float spawnInterval = 3f; // Intervalle entre les spawns
    public float spawnRangeX = 60f; // Plage horizontale pour le spawn
    public float spawnYOffset = -5f; // Décalage vertical pour spawner plus bas que la caméra

    private float timer;

    void Update()
    {
        // Incrémenter le timer
        timer += Time.deltaTime;

        // Vérifier si le temps écoulé dépasse l'intervalle de spawn
        if (timer >= spawnInterval)
        {
            SpawnBubble();
            timer = 0f; // Réinitialiser le timer
        }
    }

    void SpawnBubble()
    {
        // Vérifier si le prefab est assigné
        if (bubblePrefab == null)
        {
            Debug.LogError("Le prefab de bulle n'est pas assigné dans le script BubbleSpawner !");
            return;
        }

        // Calculer une position aléatoire pour le spawn
        float spawnX = UnityEngine.Random.Range(-spawnRangeX, spawnRangeX);
        float spawnY = -Camera.main.orthographicSize + spawnYOffset; // Bas de l'écran avec un décalage
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        // Instancier la bulle
        GameObject spawnedBubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);

        // Vérifier si l'instanciation a réussi
        if (spawnedBubble != null)
        {
            Debug.Log("Bulle spawnée à la position : " + spawnPosition);
        }
        else
        {
            Debug.LogError("Échec de l'instanciation de la bulle !");
        }
    }
}
