using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpecialAscensor : PickupableItem
{
    protected override void GetItem(){
        base.GetItem();
        GameManager.Instance.ShowMessage("Has agarrado la llave especial del ascensor");
    }

    protected override void UseItem(){
        GameManager.Instance.ShowMessage("Objeto: " + itemName);
    }
}
