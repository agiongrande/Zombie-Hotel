
using System;
using InterfaceInteractive;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour

{
    Animator anim;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject camera2;
    [SerializeField] private GameObject mira;
    [SerializeField] private GameObject pointOfView;
    [SerializeField] private int danoZombie;

    [SerializeField] private AudioSource shoot;

    [SerializeField] private LayerMask mask;

    [SerializeField] private Transform raycastPosition;
    
    [SerializeField] private LayerMask layerToInteractWith;

    [SerializeField] private UnityEvent OnApuntar;
    [SerializeField] private UnityEvent OnNoApuntar;

    private Health health;

    private Collider[] _interactablesFound = new Collider[1];

    private int _gunDamage;

    [SerializeField] private UnityEvent onDie;
    public event Action OnInventory;

    private float TimeBetweenBullets;

    private bool _apuntando;

    private float _timerShoot;
    private float _mouseSensibility;
    public static PlayerController Instance;

    // Start is called before the first frame update

void Awake()
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

}

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
        mira.SetActive(false);
        health = GetComponent<Health>();
        shoot.volume = SettingsManager.Instance.FxVolume;
        
        MisionManager.Instance.ShowMisionText();
        _mouseSensibility = SettingsManager.Instance.MouseSensibility;
    }

    public void NewPosition(Vector3 position){
        GetComponentInParent<Transform>().position = position;
    }

    void OnTriggerEnter(Collider coll){
        if (coll.CompareTag("ManoZombie")){
            health.TakeDamage(danoZombie);
            anim.SetBool("pegar",false);
            if (health.GetHealth() < 1){
                anim.SetBool("morir",true);
                onDie.Invoke();
                Cursor.lockState = CursorLockMode.None;
            }else{
                anim.SetBool("serGolpeado",true);
            }
        }
    }
    
private void TryInteract()
    {

        var overlappingInteractables = Physics.OverlapSphereNonAlloc(raycastPosition.position, playerData.interactRadius,
            _interactablesFound, layerToInteractWith);
        
        if (overlappingInteractables == 0)
            return;
        foreach (var obj in _interactablesFound)
        {
            if (obj.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.OnInteract();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        EventosTeclado();
    }

    void EventosTeclado(){

        if (Input.GetKey(playerData.teclaArriba) && !Input.GetKey(playerData.teclaCorrer)){
            anim.SetBool("caminar",true);
            transform.Translate(Vector3.forward * playerData.speed * Time.deltaTime);
        }
        if (!Input.GetKey(playerData.teclaArriba) && !Input.GetKey(playerData.teclaAbajo)){
            anim.SetBool("caminar",false);
        }
        if (Input.GetKey(playerData.teclaAbajo)){
            anim.SetBool("caminar",true);
            transform.Translate(Vector3.forward * -playerData.speed * Time.deltaTime);
        }
        if (Input.GetKey(playerData.teclaCorrer)){
            anim.SetBool("correr",true);
            transform.Translate(Vector3.forward * playerData.speedRun * Time.deltaTime);
        }
        if (!Input.GetKey(playerData.teclaCorrer)){
            anim.SetBool("correr",false);
        }
        if (Input.GetKeyDown(playerData.teclaInteractuar)){
            TryInteract();
        }
        if (Input.GetKeyDown(playerData.teclaSacarMision)){
            InventoryManager.Instance.DisableDialogue();
        }
        if (Input.GetKeyDown(playerData.inventoryKey)){
            OnInventory.Invoke();
        }
        if (Input.GetKey(playerData.teclaIzquierda)){
            transform.Translate(Vector3.left * playerData.speed * Time.deltaTime);
            anim.SetBool("caminarIzq",true);
        } else {
            anim.SetBool("caminarIzq",false);
        }
        if (Input.GetKey(playerData.teclaDerecha)){
            transform.Translate(Vector3.right * playerData.speed * Time.deltaTime);
            anim.SetBool("caminarDer",true);
        } else {
            anim.SetBool("caminarDer",false);
        }

        if (InventoryManager.Instance.pauseAction) return;

        if ((Input.GetKey(playerData.teclaDisparar) || Input.GetMouseButton(0)) && _apuntando){
            IniciarDisparo();
        }
        if (Input.GetKey(playerData.teclaPegar)){
            anim.SetBool("pegar",true);
        }
        if (Input.GetKeyDown(playerData.teclaApuntar) || Input.GetMouseButtonDown(1)){
            if (_gunDamage>0){
               Apuntar();
            }
        }

        transform.Rotate(0,_mouseSensibility*Input.GetAxis("Mouse X"),0);

        if (Input.GetAxis("Mouse Y")<0){
            pointOfView.transform.position = new Vector3(pointOfView.transform.position.x,Mathf.Max(transform.position.y-1,pointOfView.transform.position.y+Input.GetAxis("Mouse Y")),pointOfView.transform.position.z);
        } else {
            pointOfView.transform.position = new Vector3(pointOfView.transform.position.x,Mathf.Min(transform.position.y+4,pointOfView.transform.position.y+Input.GetAxis("Mouse Y")),pointOfView.transform.position.z);
        }

    }

    void Apuntar(){
        if (_apuntando){
            anim.SetBool("apuntar",false);
            OnApuntar.Invoke();
        } else {
            anim.SetBool("apuntar",true);
            OnNoApuntar.Invoke();
        }
        _apuntando = !_apuntando;
    }

    void IniciarDisparo(){
        if (_timerShoot < Time.time){
            anim.SetBool("disparar",true);
        }
    } 

    public void Disparar(){
        RaycastHit hitData;
        if (Physics.Raycast(camera2.transform.position, camera2.transform.forward, out hitData, playerData.distanciaDisparo,mask)){
            ZombieController zombie = hitData.collider.GetComponentInParent<ZombieController>();
            if (zombie != null){
                var damage = (hitData.collider.CompareTag("ZombieHead") ? playerData.modHeadShoot:1)*_gunDamage;
                zombie.ShootPublic(damage);  
            }
        }
        SonidoDisparo();
     }

     public void SonidoDisparo(){
         shoot.Play();
     }

    void FinalDisparar(){
        anim.SetBool("disparar",false);
        _timerShoot = Time.time + TimeBetweenBullets;
        if (MisionManager.Instance.MisionLevel == 2){
            MisionManager.Instance.NextMision();
        }
     }

    void FinalGolpeado(){
        anim.SetBool("serGolpeado",false);
    }
    void FinalPegar(){
        anim.SetBool("pegar",false);
    }

    public void NoApuntar(){
        if (_apuntando){
            Apuntar();
        }
    }

    public void GunStats(int damage, float bulletsPerSecond){
        _gunDamage = damage;
        TimeBetweenBullets = 1f / playerData.bulletsPerSecond;
    }
    
}
