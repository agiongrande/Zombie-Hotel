using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum objType{
    weapon,
    medicine,
    key,
    water,
    SpecialKey
}

public class InventoryManager : MonoBehaviour
{
public static InventoryManager Instance;

[SerializeField] private Sprite emptyImg;

private InventorySlot[] slot;

public bool pauseAction;
[SerializeField] private GameObject winPanel;

private int _slotWeapon = -1;
[SerializeField] private GameObject InventoryContainer;


[SerializeField] private GameObject player;

[SerializeField] private GameObject graphicInventory;

private int _slotsTotal;

    private void Awake()
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


        _slotsTotal = InventoryContainer.transform.childCount;

        slot = new InventorySlot[_slotsTotal];

        for (int i = 0; i < _slotsTotal; i++)
        {
            slot[i] =  InventoryContainer.transform.GetChild(i).gameObject.GetComponent<InventorySlot>();
            slot[i].empty = true;
        }
        player.GetComponent<PlayerController>().OnInventory += OnInventoryHandler;
    }

    public void GetItem(PickupableItem itemToPickup)
    {
        var empty = searchEmptySlot();
        if (empty == _slotsTotal){
            GameManager.Instance.ShowMessage("No hay lugar en el inventario");
            return;    
        }
        slot[empty].empty = false;
        slot[empty].item = itemToPickup;

        itemToPickup.gameObject.SetActive(false);
        slot[empty].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = itemToPickup.img;
    }

    private int searchEmptySlot(){
        for (int i = 0; i < _slotsTotal; i++)
        {
            if (slot[i].empty == true){
                return i;
            }
        }
        return _slotsTotal;
    }

    public bool useItemForType(objType _objType){
        for (int i = 0; i < _slotsTotal; i++)
        {
            if (slot[i].item.objType == _objType){
                EmptySlot(i);
                return true;
            }
        }
        return false;
    }
    
    public void EmptySlot(int slotNumber){
        slot[slotNumber].empty = true;
        slot[slotNumber].item = null;
        slot[slotNumber].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = emptyImg;
        slot[slotNumber].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
    
    }

    public void EquipItem(int slotNumber){
        if (slot[slotNumber].item.objType == objType.weapon){
            if (_slotWeapon >= 0){
                DesEquipItem(_slotWeapon);
            }
            slot[slotNumber].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);    
            _slotWeapon = slotNumber;
            slot[slotNumber].inUse = true;
        }
    }

    public void DesEquipItem(int slotNumber){
        if (slot[slotNumber].item.objType == objType.weapon){
            slot[slotNumber].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);    
            player.GetComponent<PlayerController>().GunStats(0,0);
            _slotWeapon = -1;
            slot[slotNumber].inUse = false;
        }        
    }

    public void HideInventory(){
        OnInventoryHandler();
    }

    public bool HasItem(string item){
        
        for (int i = 0; i < _slotsTotal; i++)
        {
            if (!slot[i].empty){
                if (slot[i].item.id == item){
                    if (slot[i].item.objType == objType.water) {
                        GameManager.Instance.ShowMessage("Has usado el objeto: "+ slot[i].item.itemName);
                        EmptySlot(i);
                    }
                    return true;
                }
            }
        }
        return false;
    }


    private void OnInventoryHandler(){
        if (graphicInventory.activeSelf){
            graphicInventory.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseAction = false;
        } else{
            graphicInventory.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            MisionManager.Instance.UpdateMisionTextShort();
            Cursor.visible = true;
            pauseAction = true;
        }
        
    }


    public void DisableDialogue(){
        var _dialog = this.transform.Find("DialoguePanel");
        if (_dialog != null){
            if (_dialog.gameObject.activeSelf){
                _dialog.gameObject.SetActive(false);
            }
        }
    }

    public void WinGame(){
        winPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
