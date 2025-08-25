using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{

    public InputSystem_Actions playerControls;
    public PlayerController playerController;
    public CameraControls cameraController;

    private InputAction move;
    private InputAction look;
    private InputAction primaryFire;
    private InputAction interact;
    private InputAction crouch;
    private InputAction jump;
    private InputAction previous;
    private InputAction next;
    private InputAction sprint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerControls = new InputSystem_Actions();
    }

    void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
        move.performed += Move;
        move.canceled += StopMove;

        look = playerControls.Player.Look;
        look.Enable();
        look.performed += Look;

        primaryFire = playerControls.Player.Attack;
        primaryFire.Enable();
        primaryFire.performed += Fire;

        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;

        crouch = playerControls.Player.Crouch;
        crouch.Enable();
        crouch.started += Crouch;
        crouch.canceled += StandUp;

        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;

        previous = playerControls.Player.Previous;
        previous.Enable();
        previous.performed += Previous;

        next = playerControls.Player.Next;
        next.Enable();
        next.performed += Next;

        sprint = playerControls.Player.Sprint;
        sprint.Enable();
        sprint.started += Sprint;
        sprint.canceled += StopSprint;
    }
    void OnDisable()
    {
        move.Disable();
        look.Disable();
        primaryFire.Disable();
        interact.Disable();
        crouch.Disable();
        jump.Disable();
        previous.Disable();
        next.Disable();
        sprint.Disable();
    }
    void StopMove(InputAction.CallbackContext callback)
    {
        playerController.getMoveDirection(Vector3.zero);
    }

    void Move(InputAction.CallbackContext callback)
    {
        Vector2 movementDirection = callback.ReadValue<Vector2>();
        playerController.getMoveDirection(movementDirection);
    }

    void Look(InputAction.CallbackContext callback)
    {
        Vector2 lookDirection = callback.ReadValue<Vector2>();
        cameraController.Look(lookDirection);
    }

    void Fire(InputAction.CallbackContext callback)
    {
        playerController.Fire();
    }

    void Interact(InputAction.CallbackContext callback)
    {
        Debug.Log("Interact");
    }

    void Crouch(InputAction.CallbackContext callback)
    {
        playerController.Crouch();
    }
    
    void StandUp(InputAction.CallbackContext callback)
    {
        playerController.StandUp();
    }
    void Jump(InputAction.CallbackContext callback)
    {
        playerController.Jump();
    }
    void Previous(InputAction.CallbackContext callback)
    {
        Debug.Log("Previous");
    }
    void Next(InputAction.CallbackContext callback)
    {
        Debug.Log("Next");
    }
    void Sprint(InputAction.CallbackContext callback)
    {
        playerController.Sprint();
    }
    void StopSprint(InputAction.CallbackContext callback)
    {
        playerController.StopSprint();
    }
    


}
