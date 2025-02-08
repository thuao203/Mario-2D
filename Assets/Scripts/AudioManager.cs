using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------Audio Source----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource; 
    [Header("----------Audio Clip------------")]
    public AudioClip Background;
    public AudioClip MarioDeath;
    public AudioClip MarioEaten;
    public AudioClip PowerMario;
    public AudioClip EndGame;
    public AudioClip Pipe;
    public AudioClip SFX;
    private void Start()
    {
        musicSource.clip = Background;
        musicSource.Play();
    }
    
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
