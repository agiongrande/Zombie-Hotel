using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWater : PickupableItem
{



    protected override void GetItem(){
        base.GetItem();
        GameManager.Instance.ShowMessage("Has agarrado agua.");
    }

    protected override void UseItem(){
        GameManager.Instance.ShowMessage("Ofrecele el agua a quien lo necesite.");
    }
}

