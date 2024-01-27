using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls: MonoBehaviour
{
    private InputActions inputActions; //The object that defines what inputs do what
    private bool isMoving; //Shows whether the player is holding the left mouse button

    private void Awake()
    {
        inputActions = new InputActions(); //Create an instance of the InputActions class
    }

    private void OnEnable()
    {
        inputActions.Enable(); //Enable the InputActions class
        inputActions.Player.Start.performed += OnPlayerStartMoving; //Add a function to call when the action happens
        inputActions.Player.Start.canceled += OnPlayerStopMoving;   //Add a function to call when the action happens
        inputActions.Player.Drag.performed += OnPlayerDragging;     //Add a function to call when the action happens
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player.Start.performed -= OnPlayerStartMoving;
        inputActions.Player.Start.canceled -= OnPlayerStopMoving;
    }

    private void OnPlayerStartMoving(InputAction.CallbackContext context) //Called when we start holding the left mouse button
    { 
        /* =====TODO=====
         * 
         */
        isMoving = context.ReadValueAsButton();
        Debug.Log("button pressed");
    }

    private void OnPlayerStopMoving(InputAction.CallbackContext context) //Called when the player releases the left mouse button
    {
        isMoving = context.ReadValueAsButton();
        Debug.Log("button released");
    }

    private void OnPlayerDragging(InputAction.CallbackContext context) //Called while the player is moving the mouse
    {
        if(isMoving) 
        {
            Debug.Log(context.ReadValue<Vector2>());
        }
    }
}
