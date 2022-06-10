using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public int slotNumber;
    public PickupableItem item;
    public bool empty;
    public bool inUse;

    public void OnPointerClick (PointerEventData pointerEventDAta){
        UseItem();
    }

    private void UseItem(){
        
        if (item != null) {
        
            var pickItem = item.GetComponent<PickupableItem>();
            if (inUse){
                pickItem.ItemDesUse();
                InventoryManager.Instance.DesEquipItem(slotNumber);  
                return;
            }
            
            pickItem.ItemUse();            
            if (pickItem.objType == objType.weapon) {
                InventoryManager.Instance.EquipItem(slotNumber);
                pickItem.ItemUse();  
                InventoryManager.Instance.HideInventory();
                return;
            }

            if  (pickItem.objType == objType.medicine){
                InventoryManager.Instance.EmptySlot(slotNumber);
            }
        }
    }

}