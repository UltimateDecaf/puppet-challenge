using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used to preserve the instance of an AudioManager between the scenes
public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
