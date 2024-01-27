using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private InputActions inputActions;
    private bool isMoving;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
     
        inputActions.Enable();
        inputActions.Player.Start.performed += OnPlayerStartMoving;
        inputActions.Player.Start.canceled += OnPlayerStopMoving;
        inputActions.Player.Drag.performed += OnPlayerDragging;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player.Start.performed -= OnPlayerStartMoving;
        inputActions.Player.Start.canceled -= OnPlayerStopMoving;
    }

    private void OnPlayerStartMoving(InputAction.CallbackContext context) 
    { 
        isMoving = context.ReadValueAsButton();
        Debug.Log("button pressed");
    }

    private void OnPlayerStopMoving(InputAction.CallbackContext context)
    {
        isMoving = context.ReadValueAsButton();
        Debug.Log("button released");
    }

    private void OnPlayerDragging(InputAction.CallbackContext context)
    {
        if(isMoving) 
        {
            Debug.Log(context.ReadValue<Vector2>());
        }
    }
}
