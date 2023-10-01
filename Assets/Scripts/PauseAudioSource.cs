using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PauseAudioSource : MonoBehaviour, IDependency<Pauser>
{

    private new AudioSource audioSource;
    private Pauser pauser;

    public void Construct(Pauser obj)
    {
        pauser = obj;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pauser.PauseStateChange += OnPauseStateChange;
    }
    private void OnDestroy()
    {
        pauser.PauseStateChange -= OnPauseStateChange;
    }

    private void OnPauseStateChange(bool pause)
    {
        

        if (pause == true) audioSource.Stop();

        if (pause == false) audioSource.Play();
    }
}
