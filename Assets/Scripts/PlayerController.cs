using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] List<GameObject> rows = new List<GameObject>(3);

    private InputManager inputManager;
    private Rigidbody rb;
    private int currentRow = 1;
    private float timer = 0;
    private float timerDelay = 0.5f;
    private float speed = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputManager = InputManager.instance;
    }

    private void Update()
    {
        if (inputManager.horizontal_ia.triggered)
        {
            ChangeRow();
        }

        MovePlayer();
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
}
