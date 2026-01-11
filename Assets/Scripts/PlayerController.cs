using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] List<GameObject> rows = new List<GameObject>(3);
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem impactParticles;

    // ---------- MANAGERS ----------
    private InputManager inputManager;
    private ProgressionManager progressionManager;
    private AudioManager audioManager;

    private Rigidbody rb;
    private int currentRow = 1;
    private float timer = 0;
    private float timerDelay = 0.5f;
    private float speed = 5f;
    private bool playerDead = false;
    [SerializeField] private float jumpSpeed = 7f;
    [SerializeField] private LayerMask groundLayer;
    private float groundCheckDistance = 1f;
    private CapsuleCollider hitbox;
    private float originalHitboxHeight;
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        hitbox = GetComponent<CapsuleCollider>();
        originalHitboxHeight = hitbox.height;
        inputManager = InputManager.instance;
        audioManager = AudioManager.instance;
        progressionManager = ProgressionManager.instance;
    }

    private void Update()
    {
        if (playerDead)
        {
            return;
        }

        SwipeControl();

        if (inputManager.horizontal_ia.triggered)
        {
            audioManager.PlaySFX(audioManager.pikminSwipe);
            ChangeRow();
        }

        MovePlayer();

        if (inputManager.jump_ia.triggered && IsGrounded())
        {
            audioManager.PlaySFX(audioManager.pikminJump);
            JumpPlayer();
        }

        if (inputManager.crouch_ia.triggered && IsGrounded())
        {
            audioManager.PlaySFX(audioManager.pikminRoll);
            StartCoroutine(CrouchPlayer());
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * -0.5f;
        }

        animator.SetBool("isGrounded", IsGrounded());
        animator.SetFloat("verticalSpeed", rb.velocity.y);
    }

    public void ChangeRow()
    {
        timer = 0f;

        float horizontalMovement = inputManager.horizontal_ia.ReadValue<float>();

        if (horizontalMovement > 0)
        {
            currentRow++;
        }
        else if (horizontalMovement < 0)
        {
            currentRow--;            
        }

        currentRow = Mathf.Clamp(currentRow, 0, 2);
    }

    public void MovePlayer()
    {
        if (timer < timerDelay)
        {
            timer += Time.deltaTime * speed;

            transform.position = Vector3.Lerp(transform.position,
            new Vector3(rows[currentRow].transform.position.x, transform.position.y, rows[currentRow].transform.position.z),
            timer);
        }
    }

    public void JumpPlayer()
    {
        rb.velocity += jumpSpeed * Vector3.up;
    }

    public IEnumerator CrouchPlayer()
    {
        hitbox.height = originalHitboxHeight * 0.5f;
        hitbox.center = new Vector3(0, -0.5f, 0);
        animator.SetBool("isRolling", true);

        yield return new WaitForSeconds(0.5f);

        animator.SetBool("isRolling", false);
        hitbox.height = originalHitboxHeight;
        hitbox.center = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            Instantiate(impactParticles, collision.contacts[0].point, new Quaternion(0, 0, 90, 0));

            PlayerCrashed();
        }

        if (collision.transform.CompareTag("Ground") && rb.velocity.y < 0)
        {
            Instantiate(impactParticles, collision.contacts[0].point, Quaternion.identity, collision.transform);
        }
    }

    public void PlayerCrashed() 
    {
        audioManager.PlaySFX(audioManager.pikminCrash);
        animator.SetBool("isDead", true);
        audioManager.PlaySFX(audioManager.pikminDeath);
        playerDead = true;

        StartCoroutine(progressionManager.EndGame());
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundLayer);
    }

    public bool IsDead()
    {
        return playerDead;
    }

    public void SwipeControl()
    {
        if (Input.touchCount > 0)
        {
            Touch inputTouch = Input.GetTouch(0);

            if (inputTouch.phase == TouchPhase.Began)
            {
                startTouchPosition = inputTouch.position;
            }

            if (inputTouch.phase == TouchPhase.Ended)
            {
                endTouchPosition = inputTouch.position;
                SwipeBehaviour();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
            SwipeBehaviour();
        }

        /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;
            audioManager.PlaySFX(audioManager.pikminSwipe);

            if (endTouchPosition.x < startTouchPosition.x)
            {
                currentRow--;
            }
            else if (endTouchPosition.x > startTouchPosition.x)
            {
                currentRow++;
            }

            timer = 0f;
            currentRow = Mathf.Clamp(currentRow, 0, 2);
        }*/
    }

    public void SwipeBehaviour()
    {
        Vector2 swipe = endTouchPosition - startTouchPosition;

        float swipeThreshold = 100f;

        if (Mathf.Abs(swipe.x) > swipeThreshold)
        {
            audioManager.PlaySFX(audioManager.pikminSwipe);

            if (endTouchPosition.x < startTouchPosition.x)
            {
                currentRow--;
            }
            else if (endTouchPosition.x > startTouchPosition.x)
            {
                currentRow++;
            }

            currentRow = Mathf.Clamp(currentRow, 0, 2);
            timer = 0f;
        }

        if (Mathf.Abs(swipe.y) > swipeThreshold)
        {
            if (endTouchPosition.y > startTouchPosition.y && IsGrounded())
            {
                audioManager.PlaySFX(audioManager.pikminJump);
                JumpPlayer();
            }
            else if (endTouchPosition.y < startTouchPosition.y && IsGrounded())
            {
                audioManager.PlaySFX(audioManager.pikminRoll);
                StartCoroutine(CrouchPlayer());
            }
        }
    }
}
