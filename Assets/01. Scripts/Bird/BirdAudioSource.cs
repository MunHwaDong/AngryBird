using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAudioSource : MonoBehaviour
{
    enum BirdAudioSounds
    {
        FLY,
        SKILL
    }

    [SerializeField] private AudioClip[] _audioClips;
    private AudioSource _audioSource;
    
    private BirdController _birdController;

    void Start()
    {
        _birdController = GetComponentInParent<BirdController>();
        _audioSource = GetComponent<AudioSource>();

        _birdController.Bird.OnShot += PlayFlyAudio;
    }

    public void PlayFlyAudio(Vector3 dummy)
    {
        _audioSource.PlayOneShot(_audioClips[0]);
    }

    public void PlaySkillAudio()
    {
        _audioSource.PlayOneShot(_audioClips[1]);
    }
}
