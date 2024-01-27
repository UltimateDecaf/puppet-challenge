using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMouse : MonoBehaviour
{
    #region Variables
    //Hand variables
    [SerializeField] private GameObject hand; //Hand game object in the scene

    //Mouse variables
    [SerializeField] private Vector3 mPos; //The position of the mouse cursor
    [SerializeField] private LayerMask mouseCollider; //The layer the collider for raycasting is on
    #endregion

    // Update is called once per frame
    void Update()
    {
        mPos = GetMousePos(); //Get the mouse position

        hand.transform.position = mPos; //Move the hand object to the mouse cursor
    }

    #region Function Definitions
    /// <summary>
    /// This functions returns a Vector3 of the mouse position
    /// </summary>
    /// <returns>Vector3: mouse position</returns>
    public Vector3 GetMousePos()
    {
        Vector3 mousePos = new Vector3(0, 0, 0); //Will store the position of the mouse cursor
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Create the ray from the camera mouse
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, mouseCollider)) //wait for it to hit
        {
            mousePos = hitInfo.point; //find where the ray collided to find the position of the mouse cursor
        }
        return mousePos; //Give back the cursor position
    }
    #endregion
}

