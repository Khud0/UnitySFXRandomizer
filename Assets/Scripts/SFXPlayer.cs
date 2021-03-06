﻿using UnityEngine;

// This SFXPlayer won't destroy itself when you load the next scene (level).
// Only one SFXPlayer can exist at the time, any new instance will be instantly destroyed.
// Since "instance" is a static member, 
//      SFXPlayer can (usually) be accessed from any other script in the game by using SFXPlayer.instance
//      Instead of FindObjectOfType<SFXPlayer>();

public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer instance = null;

    // Feel free to change this number to achieve desired results.
    [SerializeField]private int sfxSourceAmount = 5;    
    [SerializeField] private float sfxVolume = 1f;
    // Pitch is exaggerated in this example, normally you'd use 0.9f to 1.1f or similar.
    // Play with minPitch and maxPitch values in the Editor until you achieve the desired effect.
    [SerializeField] private float minPitch = .50f;
    [SerializeField] private float maxPitch = 1.50f;

    private AudioSource[] sfxSources;   // Sources that the script will loop through to avoid sound interruption.
    private int sfxSourceCurrent = 0;   // Index of AudioSource that will be used for next PlaySFX().
    

    
    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        #endregion

        // Instantiate the pool of AudioSources once, so that you don't have to re-create them all the time (saves performance).
        sfxSources = new AudioSource[sfxSourceAmount];
        for (int i=0; i<sfxSourceAmount; i++)
        {
            sfxSources[i] = gameObject.AddComponent<AudioSource>();
            sfxSources[i].playOnAwake = false;
            sfxSources[i].loop = false;
        }
    }

    // ("params" from Microsoft documentation: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/params):
    // You can send a comma-separated list of arguments of the type specified in the parameter declaration or an array of arguments of the specified type. 
    // You also can send no arguments. If you send no arguments, the length of the params list is zero.

    // In other words, both ways to use PlaySFX method with "params" keyword are viable:
    //          AudioClip[] sfxOptions;                 MusicPlayer.instance.PlaySFX(false, sfxOptions);
    // OR       AudioClip clip1; AudioClip clip2;       MusicPlayer.instance.PlaySFX(false, clip1, clip2);

    /// <summary>
    /// Chooses a random AudioClip from the given array (or from comma-separated arguments) and plays it with a random pitch.
    /// </summary>
    /// <param name="changePitch">If set to "false", then original pitch will be used</param>
    public void PlaySFX(bool changePitch, params AudioClip[] clips)
    {
        if (clips.Length == 0) { Debug.Log("Mehod \"PlaySFX\" requires at least one argument!"); return; }

        // Pick a random clip and randomize its pitch.
        int randomIndex = Random.Range(0, clips.Length);
        float newPitch = 1f;
        if (changePitch) newPitch = Random.Range(minPitch, maxPitch);
        AudioClip clipToPlay = clips[randomIndex];

        // Create a new AudioSource with its own pitch and play the sound effect with its help.
        AudioSource sfxSource = sfxSources[sfxSourceCurrent];
        sfxSource.clip = clipToPlay;
        sfxSource.pitch = newPitch;
        sfxSource.volume = sfxVolume;
        sfxSource.loop = false;
        sfxSource.Play();

        // Play every SFX on a new AudioSource to avoid sound interruption when multiple SFX are played at once.
        if (sfxSourceCurrent < sfxSourceAmount-1) sfxSourceCurrent += 1; 
        else sfxSourceCurrent = 0;
    }
}
    