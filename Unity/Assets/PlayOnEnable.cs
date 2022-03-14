using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnEnable : MonoBehaviour
{
    public AudioSource aud;
    private void OnEnable()
    {
        aud.PlayOneShot(aud.clip);
    }
    private void Awake()
    {
        if (aud == null)
            aud = GetComponent<AudioSource>();
    }

}
