using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer instance = null;

    [SerializeField] private float sfxVolume = 1f;
    // Pitch is exaggerated in this example, normally you'd use 0.9f to 1.1f or similar.
    // Play with minPitch and maxPitch values in the Editor until you achieve the desired effect.
    [SerializeField] private float minPitch = .50f;
    [SerializeField] private float maxPitch = 1.50f;
    private readonly float desctructionDelay = 0.25f;

    // This SFXPlayer won't destroy itself when you load the next scene (level).
    // Only one SFXPlayer can exist at the time, any new instance will be instantly destroyed.
    // Since "instance" is a static member, 
    //      SFXPlayer can (usually) be accessed from any other script in the game by using SFXPlayer.instance
    //      Instead of FindObjectOfType<SFXPlayer>();
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
    }

    // ("params" from Microsoft documentation: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/params):
    // You can send a comma-separated list of arguments of the type specified in the parameter declaration or an array of arguments of the specified type. 
    // You also can send no arguments. If you send no arguments, the length of the params list is zero.

    // In other words, both ways to use PlaySFX method with "params" keyword are viable:
    //          AudioClip[] sfxOptions;                 MusicPlayer.instance.PlaySFX(sfxOptions);
    // OR       AudioClip clip1; AudioClip clip2;       MusicPlayer.instance.PlaySFX(clip1, clip2);

    /// <summary>
    /// Chooses a random AudioClip from the given array (or from comma-separated arguments) and plays it with a random pitch.
    /// </summary>
    public void PlaySFX(params AudioClip[] clips)
    {
        if (clips.Length == 0) { Debug.Log("Mehod \"PlaySFX\" requires at least one argument!"); return; }

        // Pick a random clip and randomize its pitch.
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(minPitch, maxPitch);
        AudioClip clipToPlay = clips[randomIndex];

        // Create a new AudioSource with its own pitch and play the sound effect with its help.
        AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.clip = clipToPlay;
        sfxSource.pitch = randomPitch;
        sfxSource.volume = sfxVolume;
        sfxSource.loop = false;
        sfxSource.Play();

        // Since we will no longer use it, destroy the AudioSource after the clip has finished playing. The delay is added "just in case".
        Destroy(sfxSource, clipToPlay.length + desctructionDelay);
    }

    // ("params" from Microsoft documentation: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/params):
    // No additional parameters are permitted after the params keyword in a method declaration, and only one params keyword is permitted in a method declaration.

    // You can't make PlaySFX(params AudioClip[] clips, bool RANDOMIZE_PITCH), that's why this is a separate method.

    /// <summary>
    /// Chooses a random AudioClip from the given array (or from comma-separated arguments) and plays it.
    /// </summary>
    public void PlaySFXNoPitch(params AudioClip[] clips) 
    {
        if (clips.Length == 0) { Debug.Log("Mehod \"PlaySFXNoPitch\" requires at least one argument!"); return; }

        // Pick a random clip to play.
        int randomIndex = Random.Range(0, clips.Length);
        AudioClip clipToPlay = clips[randomIndex];
        
        // Create a new AudioSource and play the sound effect with its help.
        AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.clip = clipToPlay;
        sfxSource.volume = sfxVolume;
        sfxSource.loop = false;
        sfxSource.Play();
    }
}
    