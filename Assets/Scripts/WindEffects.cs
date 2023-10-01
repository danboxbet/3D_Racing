using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WindEffects : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (car.NormalizeLinearVelocity <= 0.5) audioSource.volume = 0;
        else
        {
            audioSource.volume = car.NormalizeLinearVelocity;
        }
    }
}
