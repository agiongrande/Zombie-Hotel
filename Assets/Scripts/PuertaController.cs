using UnityEngine;
using UnityEngine.UI;
using InterfaceInteractive;

public class PuertaController : MonoBehaviour, IInteractable

{
    [SerializeField] private bool abierta;
    [SerializeField] private bool conLlave;
    [SerializeField] private AudioSource doorOpenAudio;
    [SerializeField] private AudioSource doorCloseAudio;
    Animator anim;

    [SerializeField] private string keyNeeded;
    private bool stateChange;
    
    public void OnInteract(){
        if (!conLlave){
            abierta=!abierta;
            MakeAction();
        } else {
            if (InventoryManager.Instance.HasItem(keyNeeded)){
                conLlave=false;
                GameManager.Instance.ShowMessage("Has usado la llave para destrabar la puerta");
            } else {
                GameManager.Instance.ShowMessage("No tenés la llave de la puerta");
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (abierta) MakeAction();
        doorOpenAudio.volume = SettingsManager.Instance.FxVolume;
        doorCloseAudio.volume = SettingsManager.Instance.FxVolume;
    }



    void MakeAction()
    {
        if (!abierta){
            anim?.SetBool("DoorOpen",false);  
            doorCloseAudio.Play();
        } else {
            anim?.SetBool("DoorOpen",true);
            doorOpenAudio.Play();
        }
    }

    public void OpenKey(){
        conLlave=false;
    }
}