using UnityEngine;


/// <summary>
/// Menu audio controller class
/// </summary>
public class MenuAudioController : MonoBehaviour
{
    public AudioSource hoverSoundSource;  
    public AudioSource clickSoundSource;  

    // hover sound
    public void PlayHoverSound()
    {
        if (hoverSoundSource != null)
        {
            hoverSoundSource.Play();
        }
    }

    // click sound
    public void PlayClickSound()
    {
        if (clickSoundSource != null)
        {
            clickSoundSource.Play();
        }
    }
}
