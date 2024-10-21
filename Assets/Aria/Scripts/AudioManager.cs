using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Source ------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource beepSource;
    [SerializeField] AudioSource spaceSource;



    [Header("------ Audio Clip ------")]
    public AudioClip backgroundMusic;
    public AudioClip foregroundMusic;
    public AudioClip warningIntercomm;
    public AudioClip taskComplete;
    public AudioClip openPuzzle;
    public AudioClip mouseClick;
    public AudioClip doubleDing;
    public AudioClip paper;


    private void Start()
    {
        spaceSource.clip = foregroundMusic;
        musicSource.clip = backgroundMusic;
        beepSource.clip = warningIntercomm;
        beepSource.Play();
        spaceSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    
}