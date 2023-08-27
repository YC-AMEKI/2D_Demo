using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    public Vector2 moveInput;
    public Vector2 dashDirection;
    public bool jumpButtonDown;
    public bool DashButtonHold;
    public PlayerInputControl inputControl;
    private PlayerController playerController;
    private Camera cam;
    private PlayerInput playerInput;

    public event Action Started;
    // Start is called before the first frame update
    public void RegistPlayer(PlayerController player)
    {
        playerController = player;
    }
    protected override void Awake()
    {
        base.Awake();
        inputControl = new PlayerInputControl();
        cam = Camera.main;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        inputControl.Enable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        inputControl.Disable();
    }

    private void Start()
    {
        //inputControl.GameInput.Dash.canceled += DashStart;
    }

    private void Update()
    {
        GetMoveInput();
        GetDashInput();
        GetDashDirection();
    }
    private void GetMoveInput()
    {
        moveInput = inputControl.GameInput.Move.ReadValue<Vector2>();
    }
    private void GetDashInput()
    {
        DashButtonHold = inputControl.GameInput.Dash.IsPressed();
    }
    private void GetDashDirection()
    {
        //Debug.Log(inputControl.GameInput.Look.);
        dashDirection = inputControl.GameInput.Look.ReadValue<Vector2>();
        //Debug.Log(Gamepad.current);
        if (Gamepad.current == null)
        {
            dashDirection = cam.ScreenToWorldPoint((Vector3)dashDirection);
            //Debug.Log(dashDirection);
        }

    }
    private void DashStart(InputAction.CallbackContext callbackContext)
    {
        Started?.Invoke();
    }

}
