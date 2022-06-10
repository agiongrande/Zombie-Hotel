using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameTrigger : MonoBehaviour
{
   void OnTriggerStay(Collider other)
   {
       if (other.CompareTag("LastMisionCharacter")){
           MisionManager.Instance.NextMision();
       }
   }
}
