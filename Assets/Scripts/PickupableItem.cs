using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickupableItem : MonoBehaviour
{
    public string id;
    public string itemName;

    public objType objType;
    public Sprite img;
    public GameObject prefab;

    protected Collider _collider;
    private bool _isOnRange;

    private GameObject _target;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            if (_isOnRange) {
                GetItem();    
            }
        }
    }

    protected void Awake()
    {
        _collider = GetComponent<Collider>();
        _target = GameObject.Find("Interact");
    }


    protected void OnTriggerEnter (Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _isOnRange = true;
        GameManager.Instance.ShowPermanentMessage("Ves un objeto: " + this.itemName);
        
    }

    protected void OnTriggerExit(Collider other)
    {
        _isOnRange = false;
        GameManager.Instance.HidePermanentMessage();
    }

    protected virtual void GetItem(){
        _collider.enabled = false;
        _isOnRange = false;
        InventoryManager.Instance.GetItem(this);
    }

    protected virtual void UseItem(){
        
    }

    public void ItemUse(){
        UseItem();
    }

    public void ItemDesUse(){
        DesUseItem();
    }
    
    protected virtual void DesUseItem(){
        
    }
    }