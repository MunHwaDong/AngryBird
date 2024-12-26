using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyAudioSource : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private AudioSource _audioSource;
    private Pig _pig;
    
    void Start()
    {
        _pig = GetComponentInParent<Pig>();
        _audioSource = GetComponent<AudioSource>();

        _pig.onDestoryBehaviour += PlayDieSound;
    }

    void PlayDieSound(int dummy)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
