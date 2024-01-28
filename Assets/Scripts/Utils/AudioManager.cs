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

    [Header("Sound FX")]
    [SerializeField] private AudioClip drawingSound;
    [SerializeField] private AudioClip stretchingSound;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip failSound;

    [Header("Crunchy's Soundbites")]
    [SerializeField] private AudioClip getMeCake;
    [SerializeField] private AudioClip OkayYes;
    [SerializeField] private AudioClip Reaching;
    [SerializeField] private AudioClip Slapped;
    [SerializeField] private AudioClip Success;
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) { audioSource.PlayOneShot( stretchingSound); }
         
    }

    public void PlayDrawingSound()
    {
        audioSource.PlayOneShot(drawingSound);
    }


    public void PlayStretchingSound()
    {
        audioSource.PlayOneShot(stretchingSound);
    }

    public void CrunchySaysGetMeCake()
    {
        audioSource.PlayOneShot(getMeCake);
    }

    public void CrunchySaysOkayYes()
    {
        audioSource.PlayOneShot(OkayYes);
    }

    public void CrunchySaysReaching() {  audioSource.PlayOneShot(Reaching);}

    public void CrunchySaysSlapped()
    {
        audioSource.PlayOneShot(Slapped);
    }

    public void CrunchySaysSuccess()
    {
        audioSource.PlayOneShot(Success);
    }
}