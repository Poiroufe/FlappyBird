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
    public float deadzone = -50;

    public Collider2D birdCollider;

    public SpriteRenderer spriteRenderer;
    public Sprite activeSprite;

    public float freezeDuration = 1f;
    public float bounceForce = 10f;

    public LogicScript logic;

    public bool BirdIsAlive = true;

    public float leftGravityForce = 1f; // Force constante vers la gauche
    public float rightGravityForce = 2f; // Force constante vers la gauche


    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && BirdIsAlive)
        {
            Debug.Log("FLAP !");
            BirdBody.linearVelocity = Vector2.up * flapstrength;

            if (animator != null)
            {
                animator.Play("NageUp");
                brasTimer = brasDuration;
            }

        }

        if (brasTimer > 0 && BirdIsAlive)
        {
            brasTimer -= Time.deltaTime;
            if (brasTimer <= 0 && animator != null)
            {
                animator.Play("Nage");
            }
        }

        if (!BirdIsAlive && transform.position.y < deadzone)
        {
            Debug.Log("Mario Deleted");
            Destroy(gameObject);
            logic.gameOver();
        }

        if (animator != null)
        {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0); // 0 correspond à la première couche d'animation
            if (BirdIsAlive && currentState.IsName("NageUp"))
            {
                BirdBody.AddForce(Vector2.right * rightGravityForce, ForceMode2D.Force);
            }
            if (BirdIsAlive && currentState.IsName("Nage"))
            {
                BirdBody.AddForce(Vector2.left * leftGravityForce, ForceMode2D.Force);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!BirdIsAlive) return; // éviter les collisions multiples

        BirdIsAlive = false;
        birdCollider.enabled = false;
        spriteRenderer.sortingOrder = 10;

        animator.Play("Dead");
        Debug.Log("Ouch");

        // Démarre le freeze
        StartCoroutine(FreezeFrameThenFall());
    }

    private IEnumerator FreezeFrameThenFall()
    {
        // Fige tout le jeu
        Time.timeScale = 0f;

        // Attend une seconde réelle
        yield return new WaitForSecondsRealtime(freezeDuration);

        // Reprend le temps
        Time.timeScale = 1f;

        // (Optionnel) applique une légère impulsion vers le bas pour la chute dramatique
        BirdBody.linearVelocity = Vector2.up * bounceForce;
    }
}
