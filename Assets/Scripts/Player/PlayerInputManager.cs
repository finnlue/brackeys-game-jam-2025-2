using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{

    public InputSystem_Actions playerControls;
    public MovementController movementController;
    public GunController gunController;
    public CameraControls cameraController;

    public WeaponBluePrint test;

    private InputAction move;
    private InputAction look;
    private InputAction primaryFire;
    private InputAction reload;
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
        movementController = GetComponent<MovementController>();
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
        primaryFire.started += StartFire;
        primaryFire.canceled += StopFire;

        reload = playerControls.Player.Reload;
        reload.Enable();
        reload.performed += Reload;

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
        movementController.getMoveDirection(Vector3.zero);
    }

    void Move(InputAction.CallbackContext callback)
    {
        Vector2 movementDirection = callback.ReadValue<Vector2>();
        movementController.getMoveDirection(movementDirection);
    }

    void Look(InputAction.CallbackContext callback)
    {
        Vector2 lookDirection = callback.ReadValue<Vector2>();
        cameraController.Look(lookDirection);
    }

    void StartFire(InputAction.CallbackContext callback)
    {
        gunController.StartFire();
    }
    void StopFire(InputAction.CallbackContext callback)
    {
        gunController.StopFire();
    }

    void Reload(InputAction.CallbackContext callback)
    {
        StartCoroutine(gunController.Reload());
    }

    void Interact(InputAction.CallbackContext callback)
    {
        gunController.PickUpWeapon(test);
    }

    void Crouch(InputAction.CallbackContext callback)
    {
        movementController.Crouch();
    }
    
    void StandUp(InputAction.CallbackContext callback)
    {
        movementController.StandUp();
    }
    void Jump(InputAction.CallbackContext callback)
    {
        movementController.Jump();
    }
    void Previous(InputAction.CallbackContext callback)
    {
        gunController.prevoiusWeapon();
    }
    void Next(InputAction.CallbackContext callback)
    {
        gunController.NextWeapon();
    }
    void Sprint(InputAction.CallbackContext callback)
    {
        movementController.Sprint();
    }
    void StopSprint(InputAction.CallbackContext callback)
    {
        movementController.StopSprint();
    }
    


}
