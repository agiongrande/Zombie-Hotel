using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKey : PickupableItem
{
    protected override void GetItem(){
        base.GetItem();
        GameManager.Instance.ShowMessage("Has agarrado un objeto " + itemName);
    }

    protected override void UseItem(){
        GameManager.Instance.ShowMessage("Objeto: " + itemName);
    }
}
