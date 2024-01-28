using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : BaseState
{
    [Header("References")]
    [SerializeField] private CameraSwitcher cameraSwitcher;
    private void OnEnable() 
    {
        AudioManager.Instance.CrunchySaysSuccess();
        cameraSwitcher.ActivateSlapWinCamera();
        Debug.Log("WIN STATE ENTERED");
    }
}
