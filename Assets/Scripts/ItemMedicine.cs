using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMedicine : PickupableItem
{
    [SerializeField] private int healAmount;
    [SerializeField] private GameObject player;

    protected override void GetItem(){
        base.GetItem();
        GameManager.Instance.ShowMessage("Has agarrado medicina.");
    }

    protected override void UseItem(){
        player.GetComponent<Health>()?.RecoveryHealth(healAmount);
        GameManager.Instance.ShowMessage("Has tomado medicina.");
    }

}

