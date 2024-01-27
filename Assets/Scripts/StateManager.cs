using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

/* Created by Lari Basangov
 * 
 * This class is responsible for enabling and disabling the appropriate states,
 * which is done in one method - UpdateGameState(newState);
 * 
 * Other scripts can call the said method to change the state.
 */
public class StateManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public GameObject DrawState;
    [SerializeField] public GameObject ReachState;
    [SerializeField] public GameObject GrabState;
    [SerializeField] public GameObject SlapState;
    [SerializeField] public GameObject WinState;


    public static StateManager Instance;

    private GameObject currentState;


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
    }

    // It is good to start with DRAW state.
    private void Start()
    {
        currentState = DrawState;
        UpdateGameState(currentState);
    }

    private void Update()
    {
        // Just a random code to check things work
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            UpdateGameState(ReachState);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UpdateGameState(DrawState);
        }
    }

    // This method disables the old state, and then enables the new state.
    public void UpdateGameState(GameObject newState) 
    {
       if(currentState != null)
        {
            currentState.SetActive(false);
        }

       currentState = newState;
       currentState.SetActive(true);
    }
}
