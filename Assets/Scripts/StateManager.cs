using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

/* Created by Lari Basangov
 * Based on Tarodev's Unity Tutorial: https://www.youtube.com/watch?v=4I0vonyqMi8
 * 
 * This class stores all states, stores state-related logic (referenced from other scripts(???),
 * and allows to update the state.
 */
public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;
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

    private void Start()
    {
        UpdateGameState(GameState.Draw);
    }

    public void UpdateGameState(GameState newState) 
    {
        switch (newState)
        {
            case GameState.Draw:
                //Logic for drawing
                break;
            case GameState.Reach: 
                //Logic for reaching
                break;
            case GameState.Grab: 
                //Logic for grabbing
                break;
            case GameState.Slap: 
                //Logic for slapping
                break;
            case GameState.Finish: 
                //Logic for finishing
                break;
            default: 
                throw new ArgumentOutOfRangeException(nameof(newState));
        }
    }

    public enum GameState { Draw, Reach, Grab, Slap, Finish }
}
