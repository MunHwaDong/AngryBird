using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotAudioSource : MonoBehaviour
{
    enum SlingShotSounds
    {
        PULLING,
        SHOT
    }
    
    [SerializeField] private AudioClip[] audioClips;
    
    private SlingShotController _slingShotController;
    private AudioSource _audioSource;

    void Start()
    {
        _slingShotController = GetComponentInParent<SlingShotController>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void Init(Dragable drag)
    {
        drag.OnStartDrag += PlayDragSound;
        drag.OnShot += PlayShotSound;
    }

    void PlayDragSound()
    {
        _audioSource.PlayOneShot(audioClips[0]);
    }

    void PlayShotSound(Vector3 dummy)
    {
        _audioSource.PlayOneShot(audioClips[1]);
    }
}
