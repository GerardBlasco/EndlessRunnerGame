using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] List<GameObject> rows = new List<GameObject>(3);

    private InputManager inputManager;
    private Rigidbody rb;
    private int currentRow = 1;

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
        

        //Vector3 movementInput = new Vector3(horizontalMovement, 0f, 0f);

        //rb.velocity = movementInput;
    }

    public void ChangeRow()
    {
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
        transform.position = new Vector3(rows[currentRow].transform.position.x, transform.position.y, transform.position.z);

    }
}
