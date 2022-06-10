using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{

    [SerializeField] private GameObject weaponContainer;
    [SerializeField] private AudioSource weaponEquipSound;

    void Start()
    {
        weaponEquipSound.volume = SettingsManager.Instance.FxVolume;
    }
    public void ChangeWeapon(GameObject weapon){
        for (int i = 0; i < weaponContainer.transform.childCount; i++)
        {
            weaponContainer.transform.GetChild(i).transform.gameObject.SetActive(false);
        }
        weaponEquipSound.Play();
        weapon.transform.gameObject.SetActive(true);
        if (MisionManager.Instance.MisionLevel == 1){
            MisionManager.Instance.NextMision();
        }

    }

    public void ClearWeapon(){
        for (int i = 0; i < weaponContainer.transform.childCount; i++)
        {
            weaponContainer.transform.GetChild(i).transform.gameObject.SetActive(false);
        }
    }
}
