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

    public float leftGravityForce = 1f;
    public float rightGravityForce = 2f;

    public float chargeDuration = 1.5f; 
    public float dashForce = 20f;     
    public float flashSpeed = 0.1f;   
    public Color flashColor = Color.white;

    private float holdTimer = 0f;
    private bool isCharged = false;
    private Color baseColor;


    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        baseColor = spriteRenderer.color;
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

        if (BirdIsAlive && transform.position.x < -65)
        {
            Debug.Log("Mario out of bounds");
            BirdIsAlive = false;
            birdCollider.enabled = false;
            spriteRenderer.sortingOrder = 10;

            animator.Play("Dead");

            StartCoroutine(FreezeFrameThenFall());
        }

        if (BirdIsAlive && transform.position.x > 65)
        {
            Debug.Log("Mario out of bounds");
            BirdIsAlive = false;
            birdCollider.enabled = false;
            spriteRenderer.sortingOrder = 10;

            animator.Play("Dead");

            StartCoroutine(FreezeFrameThenFall());
        }

            if (animator != null)
        {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0); 
            if (BirdIsAlive && currentState.IsName("NageUp"))
            {
                BirdBody.AddForce(Vector2.right * rightGravityForce, ForceMode2D.Force);
            }
            if (BirdIsAlive && currentState.IsName("Nage"))
            {
                BirdBody.AddForce(Vector2.left * leftGravityForce, ForceMode2D.Force);
            }

        }

        if (Keyboard.current.spaceKey.isPressed && BirdIsAlive)
        {
            holdTimer += Time.deltaTime;
            Debug.Log("HoldTimer: " + holdTimer);

            if (holdTimer >= chargeDuration && !isCharged)
            {
                isCharged = true;
                StartCoroutine(BlinkWhileCharged());
                Debug.Log("Chargé !");
            }
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame && BirdIsAlive)
        {
            if (isCharged)
            {
                StartCoroutine(Dash());
            }

            holdTimer = 0f;
            isCharged = false;
            spriteRenderer.color = baseColor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!BirdIsAlive) return;

        BirdIsAlive = false;
        birdCollider.enabled = false;
        spriteRenderer.sortingOrder = 10;

        animator.Play("Dead");
        Debug.Log("Ouch");

        StartCoroutine(FreezeFrameThenFall());
    }

    private IEnumerator FreezeFrameThenFall()
    {
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(freezeDuration);

        Time.timeScale = 1f;


        BirdBody.linearVelocity = Vector2.up * bounceForce;
    }

    private IEnumerator BlinkWhileCharged()
    {
        Debug.Log("Blink coroutine started");
        while (isCharged)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashSpeed);
            spriteRenderer.color = baseColor;
            yield return new WaitForSeconds(flashSpeed);
        }
        spriteRenderer.color = baseColor;
    }

    private IEnumerator Dash()
    {
        Debug.Log("DASH !");
        BirdBody.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);

        // Optionnel : on désactive temporairement la gravité du courant vers la gauche
        float oldLeftForce = leftGravityForce;
        leftGravityForce = 0f;
        yield return new WaitForSeconds(0.5f); // Durée du dash
        leftGravityForce = oldLeftForce;
    }
}
