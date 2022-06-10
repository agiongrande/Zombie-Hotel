using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mision4 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")){
            MisionManager.Instance.NextMision();
            Destroy(this);
        }   
    }
}
