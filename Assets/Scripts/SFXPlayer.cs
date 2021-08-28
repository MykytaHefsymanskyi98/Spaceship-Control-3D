using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] AudioClip levelCompletedSFX;
    [SerializeField] AudioClip crashSFX;

    AudioSource gameAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameAudioSource = GetComponent<AudioSource>();
    }

    public void PlayMainEngineSFX()
    {
        if (!gameAudioSource.isPlaying)
        {
            gameAudioSource.PlayOneShot(mainEngineSFX);
        }
    }

    public void StopMainEngineSFX()
    {
        gameAudioSource.Stop();
    }    

    public void PlayLevelCompletedSFX()
    {
        gameAudioSource.PlayOneShot(levelCompletedSFX);
    }

    public void PlayGameOverSFX()
    {
        gameAudioSource.PlayOneShot(crashSFX, 0.3f);
    }
}
