using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class AudioManager : MonoBehaviour
{
    //Instance
   private static AudioManager instance;    
   public static AudioManager Instance 
    {   
        get 
        { 
            if (instance == null)
            { 
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned Audio Manager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }


    private AudioSource musicSource1;
    private AudioSource musicSource2;
    private AudioSource sfxSource;

    private bool firstMusicSourceIsPlaying;

    private void Awake()
    {

        //DontDestroyOnLoad(this.gameObject);

        musicSource1 = this.GetComponent<AudioSource>();
        musicSource2 = this.GetComponent<AudioSource>();
        sfxSource = this.GetComponent<AudioSource>();

        musicSource1.loop = true;
        musicSource2.loop = true;

        musicSource2.playOnAwake = false;
    }

    //Play Music
    public void PlayMusic(AudioClip musicClip)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource1 : musicSource2;

        activeSource.clip = musicClip;
        activeSource.Play();
        activeSource.volume = 1;
    }
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1f)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource1 : musicSource2;

        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }
    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1f)
    {
        //Checks which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource1 : musicSource2;
        AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSource2 : musicSource1;

        //Swap Source
        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

        // Set Audio fields and Start Coroutine
        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource,  newSource,  transitionTime));
    }

    // Fades
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
        float t = 0f;

        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            original.volume = (1 - (t / transitionTime));
            newSource.volume = (t / transitionTime);
            yield return null;
        }
        original.Stop();
    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float tranisitionTime)
    {
        if (!activeSource.isPlaying)
        {
            activeSource.Play();
        }
        float t = 0f;
        
        //Fade out
        for (t = 0; t < tranisitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (1 - (t / tranisitionTime));
            yield return null;
        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        //Fade In
        for (t = 0; t < tranisitionTime; t += Time.deltaTime)
        {
            activeSource.volume = ((t / tranisitionTime));
            yield return null;
        }
    }
        

    //Play Sound FX
    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }
    public void PlaySFX(AudioClip sfxClip, float volume)
    {
        sfxSource.PlayOneShot(sfxClip, volume); 
    }

    //Set Volume
    public void SetMusicVolume(float volume)
    {
        musicSource1.volume = volume;
        musicSource2.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}
