using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Retract : BaseState
{
    [Header("References")]
    [SerializeField] private CameraSwitcher cameraSwitcher;

    [Header("Spline")] 
    public SplineExtrude arm;
    public SplineAnimate handAnim;
    
    [Header("Slap Animation")]
    public float duration;

    private void OnEnable()
    {
        Debug.Log("Retract Start");
        cameraSwitcher.ActivateSlapWinCamera();
        
        //TODO: Add Screenshake FX
        
        //Retract Hand
        handAnim.StopAllCoroutines();
        handAnim.Duration = duration;
        StartCoroutine(RetractHand());
        
        //Retract Arm
        arm = GameObject.FindWithTag("Arm").GetComponent<SplineExtrude>();
        arm.StopAllCoroutines();
        StartCoroutine(RetractArm(arm.Range.y));
    }

    protected override void Update()
    {

    }

    IEnumerator RetractHand()
    {
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            handAnim.ElapsedTime = Mathf.Lerp(handAnim.ElapsedTime, 0, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        handAnim.ElapsedTime = 0f;
    }

    IEnumerator RetractArm(float from)
    {
        float elapsed = 0f;
        float lerpValue;

        while (elapsed < duration)
        {
            arm.Rebuild();
            lerpValue = Mathf.Lerp(from, 0, elapsed * 7 / duration);
            arm.Range = new Vector2(0, lerpValue);
            elapsed += Time.deltaTime;
            yield return null;
        }
        arm.Range = Vector2.zero;
        NextState();
    }

    private void NextState()
    {
        StateManager.Instance.UpdateGameState(StateManager.Instance.SlapState);
    }

    private void OnDisable()
    {
        
    }
}
