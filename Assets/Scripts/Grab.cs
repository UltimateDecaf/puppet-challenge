using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.PlasticSCM.Editor.WebApi.CredentialsResponse;


/* Created by...
 * 
 * This script handles the game logic of the GRAB game state:
 * - Changes the Virtual Camera
 * - Enables appropriate UI on Canvas
 * - Starts the grab minigame
 * - Handles all exit conditions (to WIN or SLAP state)
 */
public class Grab : BaseState
{
    [Header("References")]
    [SerializeField] private GameObject GrabCanvas;
    [SerializeField] private RectTransform Handle;
    [SerializeField] private CameraSwitcher cameraSwitcher;

    [Header("Values")]
    [SerializeField] private Vector2 handleTarget;
    [SerializeField] private float time;
    [SerializeField] private float pointerSpeed;
    [SerializeField] private float winRangeMin = -100f;
    [SerializeField] private float winRangeMax = 100f;
    private bool isMoving = true;

    private void OnEnable()
    {
        cameraSwitcher.ActivateGrabCamera();
        GrabCanvas.SetActive(true);
    }

    private void OnDisable()
    {
        GrabCanvas.SetActive(false);
    }

    protected override void Update()
    {
        StopMovingWhenSpaceIsPressed();
        if (isMoving) 
        {
            float c = Mathf.PingPong(Time.time * pointerSpeed, 2); // Multiplied by pointerSpeed
            float r = MapValue(c, 0, 2, -1, 1);

            float newXPosition = handleTarget.x * r;
            Handle.anchoredPosition = new Vector2(newXPosition, -330); // Keeping Y position constant
        }
        else 
        {
            HandleResult();
        }
 
    
    }


    float MapValue(float v, float fromMin, float fromMax, float toMin, float toMax)
    {
        float scaledValue = (v - fromMin) / (fromMax - fromMin);
        float mappedValue = (scaledValue * (toMax - toMin)) + toMin;

        return mappedValue;

    }

    private void StopMovingWhenSpaceIsPressed()
    {
        if(Controls.Instance.buttonPressed) 
        {
            isMoving = false;
        }
    }

    private void HandleResult() 
    { 
        if(winRangeMin <= Handle.anchoredPosition.x && Handle.anchoredPosition.x <= winRangeMax) 
        {
            StateManager.Instance.UpdateGameState(StateManager.Instance.WinState);
        }
        else 
        {
            StateManager.Instance.UpdateGameState(StateManager.Instance.SlapState);
        }
    }

}       


    