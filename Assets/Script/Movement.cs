using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject hand; //The hand Game Object
    [SerializeField] private Transform handPos; //The position info of the hand Game Object
    [SerializeField] private float speed = 1f; //The speed at which the hand can move
    [SerializeField] private List<Vector3> trails = new List<Vector3>(); //Stores the vectors used to move the hand, so acts as trail that can be retraced back later
    [SerializeField] private float retractSpeed = -1f; //The speed at which to retract the hand

    //Pending variables
    [SerializeField] private bool movementLock = false; //Could be used to toggle whether the player can input movement
    // Start is called before the first frame update
    void Start()
    {
        handPos = hand.transform; //Get the transform component of the hand object
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement Input Logic
        /* QUESTIONS
         * 1. Do we shoot the hand back when there is no input?
         */
        Vector3 handVector = new Vector3(0, 0, 0); //Will store the values for what direction the hand is moving in

        if (!movementLock) //If we are not locked out of movement, then continue registering inputs
        {
            //Vertical
            if (Input.GetKey(KeyCode.W)) //Up
            {
                handVector.y += speed;
            }
            else if (Input.GetKey(KeyCode.S)) //Down
            {
                handVector.y -= speed;
            }
            //Horizontal
            if (Input.GetKey(KeyCode.A)) //Left
            {
                handVector.x -= speed;
            }
            else if (Input.GetKey(KeyCode.D)) //Right
            {
                handVector.x += speed;
            }
        }

        //This is to avoid putting the first bunch of vectors in the movement history since we are just standing still. Please let me know if there is a better way to do this
        if (handVector != Vector3.zero)
        {
            trails.Add(handVector); //Add the movement vetor for the hand on this frame so we can retrace its steps later
        }
        else //Movement vector is all 0 so no movement key is being pressed. So retract the hand
        {
            if (trails.Count > 0)
            {
                handVector = trails[trails.Count - 1]; //Set the movement vector to the last vector
                handVector *= retractSpeed;
                trails.RemoveAt(trails.Count - 1); //Remove it from the list since its been 
            }
        }
        handPos.position += handVector; //Add the movement vector to the new hand position
        hand.transform.position = handPos.position; //Applying the new position
        #endregion
    }
}
