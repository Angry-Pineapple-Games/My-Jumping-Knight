using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public List<AudioClip> songs;
    public List<AudioClip> sounds;
    public AudioSource musicSource;
    public AudioSource soundSource;
    private static bool created = false;
    private string currentScene;

    /*Asigna la cancion indicada a la fuente de audio*/
    public void SelectSong(int i)
    {
        if (i < songs.Count) { musicSource.clip = songs[i]; }
    }

    /*Asigna el sonido indicado a la fuente de audio*/
    public void SelectSound(int i)
    {
        if (i < sounds.Count) { soundSource.clip = sounds[i]; }
    }

    /*Reproduce la cancion de la fuente de audio*/
    public void PlaySong()
    {
        musicSource.Play();
    }

    /*Reproduce el sonido de la fuente de audio*/
    public void PlaySound()
    {
        soundSource.Play();
    }

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        currentScene = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        /*Setup starting music*/
        switch (currentScene)
        {
            case "TitleScene":
                musicSource.clip = songs[0];
                PlaySong();
                break;
            default:
                break;
        }
    }

}

