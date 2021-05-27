using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider soundsSlider;

    public void SetSoundsVolume()
    {
        audioMixer.SetFloat("soundsVolume", soundsSlider.value);
    }
    
    public void SetMusicVolume()
    {
        audioMixer.SetFloat("musicVolume", musicSlider.value);
    }
}
