using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{

    FMOD.Studio.Bus Master;
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;

    float masterVolume = 1f;
    float musicVolume = 0.5f;
    float SFXVolume = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {

        //To do: attach Bus paths from FMOD to variables Master, Music, SFX

        /*
        Master = FMODUnity.RuntimeManager.GetBus("PATH");
        Music = FMODUnity.RuntimeManager.GetBus("PATH");
        SFX = FMODUnity.RuntimeManager.GetBus("PATH");
        */

    }

    // Update is called once per frame
    void Update()
    {
        /*
        Music.setVolume(musicVolume);
        SFX.setVolume(SFXVolume);
        Master.setVolume(masterVolume);
        */
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        masterVolume = newMasterVolume;
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        musicVolume = newMusicVolume;
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
    }

}
