using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceInteractive;

public class InteractObject : MonoBehaviour, IInteractable
{
    [SerializeField] private int misionLevel;
    [SerializeField] private string objectRequired;

    [SerializeField] private PickupableItem objectGiven;
    [SerializeField] private AudioSource sound;

    public void OnInteract(){
        if (MisionManager.Instance.MisionLevel == misionLevel){
            if (objectRequired != ""){
                if (!InventoryManager.Instance.HasItem(objectRequired)){
                    GameManager.Instance.ShowMessage("No tenés el objeto requerido: "+objectRequired);
                    return;
                }
            }

            if (objectGiven){
                InventoryManager.Instance.GetItem(objectGiven);
                GameManager.Instance.ShowMessage("Has recibido un objeto: "+objectGiven.itemName);
            }
            MisionManager.Instance.NextMision();
            if (sound) sound.Stop();
            Destroy(this);
        }
    }
}
