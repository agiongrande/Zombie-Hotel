using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollow : MonoBehaviour
{
    private Animator _anim;

    private GameObject _target;

    private bool _followPlayer;
    [SerializeField] private float MaxDistance;
    [SerializeField] private float MinDistance;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _target = GameObject.Find("Personaje");
    }

    // Update is called once per frame
    void Update()
    {

        if (!_followPlayer) return;

        var distance = Vector3.Distance(transform.position, _target.transform.position);
        var direction = (_target.transform.position - transform.position).normalized;
        
        
        if (distance < MaxDistance && distance > MinDistance){
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 3);
            transform.position += direction * Time.deltaTime;
            _anim.SetBool("caminar",true);
                 
        } else {
            _anim.SetBool("caminar",false);
        }
        
        }


    public void ActivateFollow(){
        _followPlayer = true;
    }
    
}
