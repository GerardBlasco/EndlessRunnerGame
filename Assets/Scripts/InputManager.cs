using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField] InputActionAsset map;

    public InputAction horizontal_ia;
    public InputAction jump_ia;
    public InputAction crouch_ia;

    private void Awake()
    {
        instance = this;

        horizontal_ia = map.FindActionMap("Movement").FindAction("Horizontal");
        jump_ia = map.FindActionMap("Movement").FindAction("Jump");
        crouch_ia = map.FindActionMap("Movement").FindAction("Crouch");
    }

    private void Start()
    {
        map.Enable();
    }
}
