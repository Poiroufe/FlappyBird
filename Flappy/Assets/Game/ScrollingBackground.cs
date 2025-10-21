using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public GameObject solPrefab; // Le prefab du Sol
    public float scrollSpeed = 5f; // Vitesse de défilement
    public int numberOfTiles = 10; // Nombre de tiles pour couvrir la largeur de la scène
    public float tileWidth = 5f; // Largeur d'une tile (assurez-vous que cela correspond à la taille réelle de votre sprite)

    private GameObject[] tiles;

    void Start()
    {
        // Crée les tiles et les positionne horizontalement
        tiles = new GameObject[numberOfTiles];
        for (int i = 0; i < numberOfTiles; i++)
        {
            Vector3 position = new Vector3(transform.position.x + i * tileWidth, transform.position.y, transform.position.z);
            tiles[i] = Instantiate(solPrefab, position, Quaternion.identity, transform);
        }
    }

    void Update()
    {
        // Déplace chaque tile vers la gauche
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

            // Si une tile sort de l'écran, la repositionner à la fin
            if (tiles[i].transform.position.x < transform.position.x - tileWidth)
            {
                float newX = tiles[i].transform.position.x + tileWidth * numberOfTiles;
                tiles[i].transform.position = new Vector3(newX, tiles[i].transform.position.y, tiles[i].transform.position.z);
            }
        }
    }
}