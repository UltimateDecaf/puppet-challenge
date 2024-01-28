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
    // In order to add audioClip:
    // 1. Create a SerializeField
    // 2. Insert your AudioClip from the Assets folder
    // 3. Write a method for playing it.
    // 4. Reference it in the appropriate section of the state script
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip testMusic;
    [SerializeField] private AudioClip testSuccessMusic;
    [SerializeField] private AudioClip drawingSound;

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
    }

    public void PlayTestSound() 
    {
        audioSource.PlayOneShot(testMusic);
    }
   
    public void PlayDrawingSound()
    {
        audioSource.PlayOneShot(drawingSound);
    }

    public void PlayBackgroundMusic()
    {
    }

}