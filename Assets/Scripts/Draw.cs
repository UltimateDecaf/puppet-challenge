using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//TODO: Don't forget to add yourself in the credit!
/* Created by Lari Basangov,..
 * 
 * This is the implementation for the DRAW state
 * It READS values from Controls, and implements the game logic for DRAW phase.
 */
public class Draw : BaseState
{
    
    protected override void Start()
    {
        Debug.Log("Draw Started");
    }

    protected override void Update()
    {
        if (Controls.Instance.isMoving)
        {
            Debug.Log(Controls.Instance.movementPoint);
        }
    }
}
