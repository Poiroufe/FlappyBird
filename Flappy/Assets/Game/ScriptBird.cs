using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ScriptBird : MonoBehaviour
{
    public Rigidbody2D BirdBody;
    public float flapstrength = 15;

    public Animator animator;
    public float brasDuration = 0.2f;

    private float brasTimer = 0;

    public float moveSpeed = 5;
    public float deadSpeed = 7;
    public float deadzone = -70;

    public Collider2D birdCollider;

    public SpriteRenderer spriteRenderer;
    public Sprite activeSprite;

    public float freezeDuration = 1f;
    public float bounceForce = 10f;

    public LogicScript logic;

    public bool BirdIsAlive = true;


    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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

        if (brasTimer > 0 && BirdIsAlive == true)
        {
            brasTimer = brasTimer - Time.deltaTime;
            if (brasTimer <= 0 && animator != null)
            {
                animator.Play("Nage");
            }
        }
        if (BirdIsAlive == false)
        {
            transform.position = transform.position + (Vector3.left * deadSpeed * Time.deltaTime);

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
        birdCollider.enabled = false; // D�sactiver le Collider pour �viter que le bird interagisse avec l'environnement
        animator.Play("Dead");
    }

}

