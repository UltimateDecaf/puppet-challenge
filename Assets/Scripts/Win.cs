using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : BaseState
{
    [Header("References")]
    [SerializeField] private CameraSwitcher cameraSwitcher;
    [SerializeField] private GameObject winCanvas;
    private void OnEnable() 
    {
        winCanvas.SetActive(true);
        AudioManager.Instance.CrunchySaysSuccess();
        cameraSwitcher.ActivateSlapWinCamera();
        Debug.Log("WIN STATE ENTERED");
    }
}
