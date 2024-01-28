using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slap : BaseState
{
    [Header("References")]
    [SerializeField] private CameraSwitcher cameraSwitcher;
    private void OnEnable()
    {
        Debug.Log("SLAP STATE STARTED!");
        cameraSwitcher.ActivateDrawReachCamera();

    }
}
