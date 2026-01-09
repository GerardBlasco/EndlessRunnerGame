using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] List<GameObject> rows = new List<GameObject>(3);

    // ---------- MANAGERS ----------
    private InputManager inputManager;
    private ProgressionManager progressionManager;

    private Rigidbody rb;
    private int currentRow = 1;
    private float timer = 0;
    private float timerDelay = 0.5f;
    private float speed = 5f;
    private bool playerDead = false;
    [SerializeField] private LayerMask groundLayer;
    private float groundCheckDistance = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputManager = InputManager.instance;
        progressionManager = ProgressionManager.instance;
    }

    private void Update()
    {
        if (inputManager.horizontal_ia.triggered && !playerDead)
        {
            ChangeRow();
        }

        MovePlayer();

        if (inputManager.jump_ia.triggered && IsGrounded())
        {
            JumpPlayer();
        }
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
        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            PlayerCrashed();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            PlayerCrashed();
        }
    }

    public void PlayerCrashed() 
    {
        playerDead = true;

        progressionManager.EndGame();
    }

    bool IsGrounded()
    {
        // Raycast hacia abajo para detectar si estamos sobre el suelo
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundLayer);
    }
}
