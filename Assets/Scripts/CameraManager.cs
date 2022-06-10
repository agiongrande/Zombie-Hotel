using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
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

}
