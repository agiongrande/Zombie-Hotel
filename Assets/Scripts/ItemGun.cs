using System;
using UnityEngine;

public class ItemGun : PickupableItem
{
    public GameObject weaponInPlayer;

    public int damage;
    public float bulletsPerSecond;

    [SerializeField] private GameObject player;


    protected override void GetItem(){
        base.GetItem();
        GameManager.Instance.ShowMessage("Has agarrado un arma " + itemName);
        if (MisionManager.Instance.MisionLevel == 0){
            MisionManager.Instance.NextMision();
        }
    }

    protected override void UseItem(){
        player.GetComponent<PlayerController>().GunStats(damage,bulletsPerSecond);
        player.GetComponent<PlayerWeaponController>()?.ChangeWeapon(weaponInPlayer);
    }

    protected override void DesUseItem(){
        player.GetComponent<PlayerWeaponController>()?.ClearWeapon();
        player.GetComponent<PlayerController>()?.NoApuntar();
    }

    


}
