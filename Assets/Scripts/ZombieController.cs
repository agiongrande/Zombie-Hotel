using UnityEngine;
using UnityEngine.Events;
using System;

enum ZombieBody{
    Head = 0,
    Body=1
}

public class ZombieController : MonoBehaviour
{
    private int _rutina;
    private float _timer;
    private Animator _anim;

    [SerializeField] private AudioSource zombieScream;
    private Quaternion _angulo;
    private float _grado;
    private GameObject _target;
    private Health _health;
    private bool _attacking;
    private bool _dead;
    private bool _agresiveMode;

    


    [SerializeField] private Transform zombieView;

    [SerializeField] private LayerMask mask;

    [SerializeField] private ZombieData zombieData;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canvasLife;


    
    void OnTriggerEnter(Collider coll){
        if (coll.CompareTag("ManoHeroe")){
            Shoot(zombieData._hitDamage);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _target = GameObject.Find("Personaje");
        _health = GetComponent<Health>();
        zombieScream.volume = SettingsManager.Instance.FxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_dead){
            EnemyMove();
            if (canvasLife != null){
                canvasLife.transform.LookAt(transform.position + player.transform.rotation * Vector3.back, player.transform.rotation * Vector3.up);
            }
        }
    }

    void EnemyMove()
    {
        var distance = Vector3.Distance(transform.position, _target.transform.position);
        var direction = (_target.transform.position - transform.position).normalized;
        
        
        if (distance < zombieData.attackDistance) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * zombieData.speedRotate);
            _anim.SetBool("correr",false);
            _anim.SetBool("caminar",false);
            _anim.SetBool("atacar",true);
            _attacking = true;
        } else if ((distance < zombieData.chaseDistance || _agresiveMode) && !_attacking){
            RaycastHit hitData;

            if (Physics.Raycast(transform.position, direction, out hitData, zombieData.chaseDistance+100)){
                if (hitData.collider.CompareTag("Player")){

                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * zombieData.speedRotate);
                    transform.position += direction * Time.deltaTime * zombieData.speedRun;

                    _anim.SetBool("caminar",false);
                    _anim.SetBool("atacar",false);
                    _anim.SetBool("correr",true);
                    
                }
                }
        } else {
            _anim.SetBool("correr",false);
            _timer += 1 * Time.deltaTime;
            if (_timer>4){
                _rutina = UnityEngine.Random.Range(0,2);
                _timer=0;
            }
            switch (_rutina)
            {
                case 0:
                    _anim.SetBool("caminar",false);
                    break;
                case 1:
                    _grado = UnityEngine.Random.Range(0,360);
                    _angulo = Quaternion.Euler(0,_grado,0);
                    _rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, _angulo, 0.5f);
                    transform.Translate(Vector3.forward * zombieData.speed * Time.deltaTime);
                    _anim.SetBool("caminar",true);
                    break;
            }
    }}

    void finalAtaque(){
        _anim.SetBool("atacar",false);
        _attacking = false;
    }


    private void Shoot(int damage){
        if (!_dead){
            _health.TakeDamage(damage);
            _attacking = false;
            _anim.SetBool("atacar",false);
            zombieScream.Play();
            if (_health.GetHealth() == 0){
                _anim.SetBool("morir",true);
                _dead = true;      
            }
            _agresiveMode = true;
        }
    }

    public void ShootPublic(int damage){
        Shoot(damage);
    }

}
