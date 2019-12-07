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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
