using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Zombie", fileName = "ZombieData", order =0)]
public class ZombieData : ScriptableObject
{
    [SerializeField] public float speed;
    [SerializeField] public float speedRun;
    [SerializeField] public float speedRotate;
    [SerializeField] public float chaseDistance;
    [SerializeField] public float attackDistance;
    [SerializeField] public int _hitDamage;

}