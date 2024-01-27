using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


/* Created by Lari Basangov, Jan Willem
 * 
 * This class receives inputs from the player
 * Other scripts can read the values from it.
 */
public class Controls: MonoBehaviour
{
    public static Controls Instance;
    
    private InputActions inputActions; //The object that defines what inputs do what

    //Values that are sent to the state scripts
    public bool isMoving { get; private set; } //Shows whether the player is holding the left mouse button in DRAW state
    public bool buttonPressed { get; private set; } // Shows whether the player has pressed the button in the GRAB state
    public Vector2 movementPoint { get; private set; } // value of the current position of the mouse
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        inputActions = new InputActions(); //Create an instance of the InputActions class
    }

    private void OnEnable()
    {
        inputActions.Enable(); //Enable the InputActions functionality 
        inputActions.Player.Start.performed += OnPlayerStartMoving; //these are to start listening for actions
        inputActions.Player.Start.canceled += OnPlayerStopMoving;   
        inputActions.Player.Drag.performed += OnPlayerDragging;     
        inputActions.Player.Grab.performed += OnGrabPerformed;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player.Start.performed -= OnPlayerStartMoving; //these are to unsubscribe methods (good practice)
        inputActions.Player.Start.canceled -= OnPlayerStopMoving;
        inputActions.Player.Drag.performed -= OnPlayerDragging;
        inputActions.Player.Grab.performed -= OnGrabPerformed;
    }

    private void OnPlayerStartMoving(InputAction.CallbackContext context) //Called when we start holding the left mouse button
    { 
        /* =====TODO=====
         * 
         */
        isMoving = context.ReadValueAsButton();

    }

    private void OnPlayerStopMoving(InputAction.CallbackContext context) //Called when the player releases the left mouse button
    {
        isMoving = context.ReadValueAsButton();
        
    }

    private void OnPlayerDragging(InputAction.CallbackContext context) //Called while the player is moving the mouse
    {
    
        
        movementPoint =  context.ReadValue<Vector2>();
        
    }

    private void OnGrabPerformed(InputAction.CallbackContext context) //Called while the player is moving the mouse
    {
        
         buttonPressed = context.ReadValueAsButton();
        
    }
}
