using UnityEngine;
using UnityEngine.InputSystem;

public class ScriptBird : MonoBehaviour
{
    public Rigidbody2D BirdBody;
    public float flapstrength = 15;

    public Animator animator;
    public float brasDuration = 0.2f;

    private float brasTimer = 0;

    public float moveSpeed = 5;
    public float deadzone = -70;

    public LogicScript logic;

    public bool BirdIsAlive = true;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && BirdIsAlive == true)
        {
            Debug.Log("FLAP !");
            BirdBody.linearVelocity = Vector2.up * flapstrength;

            if (animator != null)
            {
                animator.Play("NageUp");
                brasTimer = brasDuration;
            }

        }

        if (brasTimer > 0)
        {
            brasTimer = brasTimer - Time.deltaTime;
            if (brasTimer <= 0 && animator != null)
            {
                animator.Play("Nage");
            }
        }
        if (BirdIsAlive == false)
        {
            transform.position = transform.position + (Vector3.left * moveSpeed * Time.deltaTime);

            if (transform.position.x < deadzone)
            {
                Destroy(gameObject);
                Debug.Log("Mario Deleted");

            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        BirdIsAlive = false;
    }
}

