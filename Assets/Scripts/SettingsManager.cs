using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    public float MusicVolume;
    public float FxVolume;
    public float MouseSensibility;

    // Start is called before the first frame update
    void Awake()
       {
         if (Instance == null)
     {
         Instance = this;
     }
     else if (Instance != this)
     {
         Destroy(gameObject);
         return;
     }
     DontDestroyOnLoad(gameObject);
    } 

    public void SetMusicVolume(float value){
        MusicVolume = value;
    }

    public void SetFxVolume(float value){
        FxVolume = value;
    }

    public void SetMouseSensibility(float value){
        MouseSensibility = value;
    }


}
