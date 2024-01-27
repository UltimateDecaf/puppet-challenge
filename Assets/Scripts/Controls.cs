using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls: MonoBehaviour
{
    #region
    //InputActions Variables
    [SerializeField] private InputActions inputActions; //The object that defines what inputs do what

    //Draw Phase Variables
    private bool isMoving; //Shows whether the player is holding the left mouse button
    [SerializeField] private LineRenderer lr; //The LineRender to draw the line the player's mouse follows in Draw phase
    [SerializeField] private LayerMask lm; //The layer the mouse collider is on
    [SerializeField] private GameObject hand; //The hand game object to draw the line from
    [SerializeField] private Vector3 handOffset = Vector3.zero; //The offset of the hand game object from the world space
    [SerializeField] private float pointThreshold = 1f; //The minimum distance the mouse needs to be away from the last point on the line
    #endregion

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

    private void OnDisable() //Disable and remove the functions when not needed
    {
        inputActions.Disable();
        inputActions.Player.Start.performed -= OnPlayerStartMoving;
        inputActions.Player.Start.canceled -= OnPlayerStopMoving;
    }

    private void OnPlayerStartMoving(InputAction.CallbackContext context) //Called when we start holding the left mouse button
    {
        Debug.Log("button pressed");
        isMoving = context.ReadValueAsButton(); //Shows we started drawing
        lr.SetPosition(0, hand.transform.position - handOffset); //Set the first point's position to the current game objects position
    }

    private void OnPlayerStopMoving(InputAction.CallbackContext context) //Called when the player releases the left mouse button
    {
        isMoving = context.ReadValueAsButton(); //Shows we released the left mouse button and stopped drawing
        Debug.Log("button released");
    }

    private void OnPlayerDragging(InputAction.CallbackContext context) //Called while the player is moving the mouse
    {
        if(isMoving) //If we are holding the left mouse button down
        {
            //Get the worldspace coordinate of the mouse cursor
            Vector3 mp = new Vector3(); //Will be the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //The ray to find the mouse world position
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, lm)) //Gets the mouse world position by finding where the ray collides with a plane
            {
                mp = raycastHit.point;
                mp.z = 0f; //Set the z coordinate to 0
                mp -= handOffset; //Gets the worldspace coordinate of the point taking the hand's offset into account
            }

            float distance = Vector3.Distance(lr.GetPosition(lr.positionCount - 1), mp); //get the distance from the mouse and the last point on the line
            if (distance >= pointThreshold) //Only add another point if it has moved more than the given threshold
            {
                lr.positionCount++; //Add a new point to the line
                lr.SetPosition(lr.positionCount - 1, mp); //Set the new point's position to the cursor's position
            }
        }
    }
}
