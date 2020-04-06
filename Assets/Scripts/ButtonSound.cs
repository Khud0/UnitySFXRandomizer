using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public void ClickButton()
    {
        Debug.Log( string.Format("Button {0} is clicked!", this.ToString()) );
        PlayClickSFX();
    }



    #region Play SFX Using Array Argument

    // This example doesn't use different sound effects and instead just changes the pitch of a single one,
    // but I wanted to add this feature so that you can easily implement this code into a real game.
    [SerializeField] private AudioClip[] soundEffects = default;

    private void PlayClickSFX()
    {
        SFXPlayerIdea.instance.PlaySFX(true, soundEffects);
    }

    #endregion

    #region Play SFX Using Comma-Separated Arguments

    // This code is just as viable as the code above.
    /*
    [SerializeField] private AudioClip soundEffect1 = default;
    [SerializeField] private AudioClip soundEffect2 = default;

    private void PlayClickSFX()
    {
        SFXPlayer.instance.PlaySFX(true, soundEffect1, soundEffect2);
    }
    */

    #endregion
}
