using UnityEngine;
using UnityEngine.InputSystem;

public class ScriptBird : MonoBehaviour
{
    public Rigidbody2D BirdBody;
    public float flapstrength = 15;

    public Animator animator;
    public float brasDuration = 0.2f;

    private float brasTimer = 0;

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
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
    }
}
