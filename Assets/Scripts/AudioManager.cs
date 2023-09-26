using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audS;
    public AudioClip changeDimension;
    public AudioClip isGrounded;

    void Start()
    {
        audS = GetComponent<AudioSource>();
    }

    public void PlayChangeDimension()
    {
        audS.PlayOneShot(changeDimension);
    }
    public void PlayIsGrounded()
    {
        audS.PlayOneShot(isGrounded);
    }
}
