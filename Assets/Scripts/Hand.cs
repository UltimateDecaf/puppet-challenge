using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public static event Action OnHandCollision;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Maze"))
        { 
            // Fail State
            OnHandCollision?.Invoke();
           StateManager.Instance.UpdateGameState(StateManager.Instance.RetractState);
        } else if (other.gameObject.layer == LayerMask.NameToLayer("Cake"))
        {
            StateManager.Instance.UpdateGameState(StateManager.Instance.WinState);
        }
    }
}
