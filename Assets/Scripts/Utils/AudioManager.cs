using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Created by Lari Basangov
 * 
 * This script manages the sounds for the game. Call sounds and music in your scripts using the methods from here.
 */
public class AudioManager : MonoBehaviour
{

    //IMPORTANT: create the GameObject for the AudioManager in the Main Menu scene!!! 
    // In order to add audioClip, you need to go to AudioManager GameObject and add an Audio Source component
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip testMusic;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        audioSource.PlayOneShot(testMusic);
    }
   


}