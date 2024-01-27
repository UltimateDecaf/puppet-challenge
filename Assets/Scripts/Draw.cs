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
    [Header("Line")]
    [SerializeField] private LineRenderer lr; //The LineRender to draw the line the player's mouse follows in Draw phase
    [SerializeField] private LayerMask lm; //The layer the mouse collider is on
    [SerializeField] private GameObject hand; //The hand game object to draw the line from
    [SerializeField] private Transform handPos; //The hand game object to draw the line from
    [SerializeField] private Vector3 handOffset; //The offset of the hand game object from the world space
    [SerializeField] private float pointThreshold; //The minimum distance the mouse needs to be away from the last point on the line
    [SerializeField] private GameObject lineColliderGO; //Game Object for the line collider
    [SerializeField] private Collider lineCollider; //The collider for the line
    [SerializeField] private Vector3 cakePos;

    [Header("Cursor")]
    [SerializeField] private Vector3 mousePos = new Vector3(0, 0, 0); //The worldspace coordinates of the mouse
    [SerializeField] private RectTransform imageToMove;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private Vector2 cursorOffset = new Vector2(1280/2, 720/2);

    
    private void OnEnable()
    {
        canvas.gameObject.SetActive(true);
        NewMousePos();
        handPos = hand.GetComponent<Transform>();
    }

    private void OnDisable()
    {
        canvas.gameObject.SetActive(false); 
    }
    

    protected override void Start()
    {
        Debug.Log("Draw Started");
        lr.SetPosition(0, handPos.position); //Set the first point's position to the current game objects position
    }

    protected override void Update()
    {
        NewMousePos();
        //Debug.Log(Camera.main.WorldToScreenPoint(mousePos));
        //cursor.position = Camera.main.WorldToScreenPoint(mousePos) - cursorOffset;
        MoveObject();
        if (Controls.Instance.isMoving) //We are drawing
        {
            //NewMousePos(); //Updates the mouse position to the new one
            MoveObject();
            //Adds the new point to the line if the mouse has moved a certain distane
            float distance = Vector3.Distance(lr.GetPosition(lr.positionCount - 1), mousePos);
            if (distance >= pointThreshold)
            {
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, mousePos);
                Vector3 pointPos = lr.GetPosition(lr.positionCount - 1);
                float cakeDist = Vector3.Distance(pointPos, cakePos);
                if (cakeDist <= 1f)
                {
                    Debug.Log("self implement collision");
                }

            }
        }
    }

    private void NewMousePos()
    {
        //Get the worldspace coordinate of the mouse cursor
        Vector3 mp = new Vector3(); //Will be the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //The ray to find the mouse world position
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, lm)) //Gets the mouse world position by finding where the ray collides with a plane
        {
            mp = raycastHit.point;
            mp.z = 0f; //Set the z coordinate to 0
            //mp -= handOffset; //Gets the worldspace coordinate of the point taking the hand's offset into account

            //Return mouse position in worldspace
            mousePos = mp;
        }
        MoveObject();
    }

    private void MoveObject()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition += cursorOffset;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Camera.main.WorldToScreenPoint(mousePosition), Camera.main, out mousePosition);
        imageToMove.position = mousePosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided");
    }
}
