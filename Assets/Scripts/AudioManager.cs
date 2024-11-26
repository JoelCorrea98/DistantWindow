using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audS;
    private AudioSource wallrunAudioSource;
    private AudioSource trampolineAudioSource;
    public AudioClip changeDimension;
    public AudioClip isGrounded;
    public AudioClip trampoline;
    public AudioClip glide;
    public AudioClip wallrun;
    public AudioClip pickeable;
    public AudioClip jump;


    void Start()
    {
        audS = GetComponent<AudioSource>();
        wallrunAudioSource = gameObject.AddComponent<AudioSource>();
        wallrunAudioSource.clip = wallrun;
        wallrunAudioSource.volume = 0.8f;
        trampolineAudioSource = gameObject.AddComponent<AudioSource>();
        trampolineAudioSource.clip = trampoline;
        trampolineAudioSource.volume = 0.9f;
    }

    public void PlayChangeDimension()
    {
        audS.PlayOneShot(changeDimension);
    }
    public void PlayIsGrounded()
    {
        audS.PlayOneShot(isGrounded);
    }
    public void PlayTrampoline()
    {
        trampolineAudioSource.PlayOneShot(trampoline);
    }
    public void PlayGlide()
    {
        audS.PlayOneShot(glide);
    }
    public void PlayWallrun()
    {
        if (wallrunAudioSource != null && !wallrunAudioSource.isPlaying)
        {
            wallrunAudioSource.Play();
            wallrunAudioSource.time = 0f;
            Debug.Log("Playing Wallrun Sound");
        }
    }
    public void StopWallrun()
    {
        if (wallrunAudioSource != null && wallrunAudioSource.isPlaying)
        {
            wallrunAudioSource.Pause();
        }
    }
    
    public void PlayPickeable()
    {
        audS.PlayOneShot(pickeable);
    }
    public void PlayJump()
    {
        audS.PlayOneShot(jump);
    }
}
