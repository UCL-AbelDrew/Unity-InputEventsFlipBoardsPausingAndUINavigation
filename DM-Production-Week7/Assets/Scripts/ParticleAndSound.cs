using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (ParticleSystem))]
[RequireComponent(typeof(AudioSource))]
public class ParticleAndSound : MonoBehaviour
{
    public ParticleSystem m_particleSystem;
    public AudioClip m_audioClip;
    public AudioSource m_audioSource;


    private void Start()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
        m_audioSource = GetComponent<AudioSource>();
       
    }

    public void PlayParticleAndAudio()
    {
        m_audioSource.clip = m_audioClip;
        m_audioSource.Play();
        m_particleSystem.Play();
       
    }

}
