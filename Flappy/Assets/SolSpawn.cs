using UnityEngine;

public class SolSpawn : MonoBehaviour
{
    public GameObject sol;
    public float spawnrate = 2;
    private float timer = 0;
    public float heightOffset = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnSol();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnrate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnSol();
            timer = 0;
        }

    }
    void spawnSol()
    {
        Instantiate(sol, transform.position, transform.rotation);
    }
}
