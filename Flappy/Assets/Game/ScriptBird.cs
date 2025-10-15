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
    public float deadSpeed = 7;
    public float deadzone = -70;

    private Collider2D birdCollider; // Référence au collider du bird

    public float freezeDuration = 1f;  // Durée de la pause
    public float bounceForce = 10f;  // Force du petit bond

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
        birdCollider.enabled = false; // Désactiver le Collider pour éviter que le bird interagisse avec l'environnement
    }

    private IEnumerator FreezeAndBounce()
    {
        // Geler le mouvement du Bird pendant une seconde
        BirdBody.velocity = Vector2.zero;

        // Attendre 1 seconde (congeler le bird)
        yield return new WaitForSeconds(freezeDuration);

        // Faire un petit bond vers le haut
        BirdBody.velocity = new Vector2(0, bounceForce);

        // Attendre un peu pour que le bond soit visible
        yield return new WaitForSeconds(0.5f);

        // Faire tomber le bird hors du champ
        BirdBody.velocity = new Vector2(-deadSpeed, BirdBody.velocity.y);
    }
}

