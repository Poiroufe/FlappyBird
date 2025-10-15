using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadzone = -70;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed * Time.deltaTime);

        if (transform.position.x < deadzone)
        {
            Destroy(gameObject);
            Debug.Log("Pipe Deleted");
        }
    }
}
