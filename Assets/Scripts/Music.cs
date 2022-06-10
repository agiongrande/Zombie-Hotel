using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    public void SetMusicVolume(float value){
        SettingsManager.Instance.SetMusicVolume(value);
    }

    public void SetFxVolume(float value){
        SettingsManager.Instance.SetFxVolume(value);
    }


    public void SetMouseSensibility(float value){
        SettingsManager.Instance.SetMouseSensibility(value);
    }

    void Start()
    {
        music.volume = SettingsManager.Instance.MusicVolume;
        music.Play();
    }
}
