using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeValue : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;

    public void SetSoundsVolume(float soundVolume)
    {
        audioMixer.SetFloat("soundVolume", soundVolume);
    }

    public void SetMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("musicVolume", musicVolume);
    }

    public void ClearVolume()
    {
        audioMixer.ClearFloat("musicVolume");
    }
    public void SetVolume()
     {
         AudioListener.volume = musicSlider.value;
     }

}

 