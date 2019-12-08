using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRecruiter : MonoBehaviour
{
    AudioManager audioManager;

    public void PlaySound(int i)
    {
        if (GameObject.Find("Audiomanager") != null)
        {
            audioManager = GameObject.Find("Audiomanager").GetComponent<AudioManager>();
            audioManager.SelectSound(i);
            audioManager.PlaySound();
        }
    }

    public void VolumeMusicOff()
    {
        if (GameObject.Find("Audiomanager") != null)
        {
            audioManager = GameObject.Find("Audiomanager").GetComponent<AudioManager>();
            audioManager.musicSource.volume = 0.0f;
        }
    }

    public void VolumeSoundsOff()
    {
        if (GameObject.Find("Audiomanager") != null)
        {
            audioManager = GameObject.Find("Audiomanager").GetComponent<AudioManager>();
            audioManager.soundSource.volume = 0.0f;
        }
    }

    public void VolumeMusicOn()
    {
        if (GameObject.Find("Audiomanager") != null)
        {
            audioManager = GameObject.Find("Audiomanager").GetComponent<AudioManager>();
            audioManager.musicSource.volume = 0.5f;
        }
    }

    public void VolumeSoundsOn()
    {
        if (GameObject.Find("Audiomanager") != null)
        {
            audioManager = GameObject.Find("Audiomanager").GetComponent<AudioManager>();
            audioManager.soundSource.volume = 0.6f;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
