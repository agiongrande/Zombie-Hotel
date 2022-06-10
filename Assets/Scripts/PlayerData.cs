using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Player", fileName = "PlayerData", order =1)]
public class PlayerData : ScriptableObject
{
    public float speed;
    public float speedRun;
    
    public  KeyCode teclaAbajo;
     public  KeyCode teclaArriba;
     public  KeyCode inventoryKey;

     public  KeyCode teclaIzquierda;
     public  KeyCode teclaDerecha;
     public  KeyCode teclaCorrer;
    public  KeyCode teclaPegar;
    public  KeyCode teclaDisparar;
    public  KeyCode teclaApuntar;
    public  KeyCode teclaInteractuar;
    public  KeyCode teclaSacarMision;
    public  float bulletsPerSecond;
    public  int distanciaDisparo; 


    public  int modHeadShoot;
    public float interactRadius;
    public float raycastLength;
    
    


    }
