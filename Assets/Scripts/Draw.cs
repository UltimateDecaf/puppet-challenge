using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//TODO: Don't forget to add yourself in the credit!
/* Created by Jan Willem
 * This is the implementation for the DRAW state
 * It READS values from Controls, and implements the game logic for DRAW phase.
 */
public class Draw : BaseState
{
    #region Variables

    [Header("Line")]
    [SerializeField] private LineRenderer lr; //The LineRender to draw the line the player's mouse follows in Draw phase
    [SerializeField] private LayerMask lm; //The layer the mouse collider is on
    [SerializeField] private Transform handPos; //The hand game object to draw the line from
    [SerializeField] private Vector3 handOffset; //The offset of the hand game object from the world space
    [SerializeField] private float pointThreshold; //The minimum distance the mouse needs to be away from the last point on the line
    [SerializeField] private Vector3 foodPos = new Vector3(6.76f, 3.22f, 0f); //Position of the food object
    [SerializeField] private bool canDraw = false; //Whether the player can start drawing
    [SerializeField] private bool drawing = false; //We are currently drawing

    [Header("Cursor")]
    [SerializeField] private Vector3 mousePos;
    [SerializeField] private bool onFood = false; //We are over the food
    [SerializeField] private Texture2D pencil; //New cursor image
    [Header("Mouse Detections")]
    public DrawMouseDetect puppet; //Mouse detection script on the puppet object
    public DrawMouseDetect food;   //Mouse detection script on the food object
    #endregion

    #region Setup and Shutdown
    private void OnEnable()
    {
        AudioManager.Instance.CrunchySaysGetMeCake();
        //Switch cursor to a pencil
        Vector2 hotspot = new Vector2(0, pencil.height);
        Cursor.SetCursor(pencil, hotspot, CursorMode.Auto);
        AudioManager.Instance.PlayDrawingSound();
    }

    private void OnDisable()
    {
        //Switch back to defualt cursor
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    protected override void Start()
    {
        Debug.Log("Draw Started");
        //lr.SetPosition(0, handPos.position); //Set the first point's position to the current game objects position
    }
    #endregion

    protected override void Update()
    {
        
        mousePos = NewMousePos(); //Get the mouse position in world coordinates

        //Update local bool flags
        canDraw = puppet.enter;
        onFood = food.enter;

        if (Controls.Instance.isMoving) //Left Click
        {
            if (canDraw) //Mouse Over
            {
                drawing = true; //Good to start drawing
                
            }
            else //Already drawing
            {
                drawing = true;
            }
        }
        else //No left click so no draw
        {
            drawing = false;
        }

        if (drawing) //Someone double check my logic please
        {
            NewPoint(); //Do line point stuff

            if (onFood) //We are over the food
            {
                AudioManager.Instance.StopDrawingSound();
                Debug.Log("Switch to Reach state");
                StateManager.Instance.UpdateGameState(StateManager.Instance.ReachState); //Switch to Reach state
            }
        }
    }

    #region Functions
    /// <summary>
    /// Returns the world space position of the cursor
    /// </summary>
    /// <returns>Vector3 World space coordinates of mouse</returns>
    private Vector3 NewMousePos()
    {
        //Get the worldspace coordinate of the mouse cursor
        Vector3 mp = new Vector3(); //Will be the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //The ray to find the mouse world position
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, lm)) //Gets the mouse world position by finding where the ray collides with a plane
        {
            mp = raycastHit.point - handOffset; //Point where it hits
            mp.z = 0f; //Set the z coordinate to 0
        }
        return mp; //Return it
    }

    private void NewPoint()
    {
        //Adds the new point to the line if the mouse has moved a certain distane
        float distance = Vector3.Distance(lr.GetPosition(lr.positionCount - 1), mousePos); //Calculate the distance from the world space mouse and the last point on the line
        if (distance >= pointThreshold) //Needs to have moved more than a certain distance
        {
            lr.positionCount++; //New point
            lr.SetPosition(lr.positionCount - 1, mousePos); //Set its position
        }
    }
    #endregion
}
