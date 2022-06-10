using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class MisionManager : MonoBehaviour
{

    public static MisionManager Instance;
    public GameObject misionPanel;
    public GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialoguePanelText;
    [SerializeField] private TextMeshProUGUI misionText;

    [SerializeField] private GameObject MisionThreeBlock;

    [SerializeField] private Vector3 Scene2Position;
    [SerializeField] private Vector3 Scene3Position;
    public int MisionLevel = 0; 

    void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    } 


    public string MisionText(){
        switch (MisionLevel)
        {
            case 0:
                return "Tutorial: Agarra el arma sobre el escritorio. Camina hacia el arma y presiona la tecla ESPACIO";

            case 1:
                return "Tutorial: Equipa el arma. Aprieta la tecla TAB y has click sobre el arma";

            case 2:
                return "Tutorial: realiza tu primer disparo. Presioná click derecho para entrar en modo disparo y luego utiliza el click izquierdo para disparar";

            case 3:
                return "Comienza la aventura. Sal de la habitación y recorre el hotel. Recuerda: puedes interactuar con personas y objetos utilizando la tecla ESPACIO"; 
            
            case 4:
                return "'Bruno, ¿sos vos? ¿Podrás venir un momento?' - Ve a la habitación 509 a hablar con Merlina";        
            
            case 5:
                return "'Hace mucho no sé nada de Silvio. Podrás ir a su habitación a ver cómo está? Es la 502'";        
        
            case 6:
                return "'Quedé encerrado en el baño hace días. ¿Podrás conseguirme algo de agua? Sé que en una habitación del cuarto piso hay, pero no sé en cuál'";

            case 7:
                return "'¡Gracias! Si podés, agradecele a Merlina por la preocupación y avisale que todo está bien.'";
            case 8:
                return "'Qué bueno! Pero estoy preocupada por Rama. Hace varias horas salió del hotel a buscar provisiones. Para salir del hotel, tendrás que bajar por el ascensor. Para eso necesitarás una tarjeta especial, hay una en la habitación 521. Te doy esta llave para que puedas ingresar en esa habitación. Conseguí la tarjeta y vení a verme.'";
            case 9:
                return "'Excelente! Como sabés, casi ningún ascensor funciona. Pero con esa tarjeta, podés activar uno de los ascensores del cuarto piso. El problema es que el hotel está repleto de zombies por una alarma que no deja de sonar en la sala de seguridad. Deberías desactivarla y recién ahí salir del hotel y buscar a Rama. ¿Podrás?'";
            case 13:
                return "'Se me cayó una estantería encima y me lastimé la pierna. Por el ruido, vinieron los zombies. ¿Podrás acompañarme de regreso al hotel? Por favor, camina despacio y no te alejes demasiado.'";       
        }
        return "";
    }
    
    public string MisionTextShort(){
        switch (MisionLevel)
        {
            case 0:
                return "Agarra el arma sobre el escritorio (tecla ESPACIO para agarrar)";

            case 1:
                return "Equipa el arma. Has click sobre el arma";

            case 2:
                return "Dispara el arma. Click derecho y luego click izquierdo para disparar";

            case 3:
                return "Sal de la habitación y recorre el hotel"; 
            
            case 4:
                return "Ve a la habitación 509 a hablar con Merlina";        
            
            case 5:
                return "Ve a la habitación 502 para ver a Silvio";        
        
            case 6:
                return "Lleva agua a Silvio. Busca el agua en el cuarto piso";

            case 7:
                return "Avisale a Merlina que Silvio está bien";
            case 8:
                return "Buscá la tarjeta especial en la habitación 521 y volvé con Merlina";
            case 9:
                return "Usa un ascensor del cuarto piso (sólo uno funciona).";
            case 10:
                return "Desactiva la alarma en la sala de seguridad.";
            case 11:
                return "Sal del hotel por la puerta principal";
            case 12:
                return "Busca a Rama";
            case 13:
                return "Permite que Rama te siga hasta la puerta del hotel";       
        }
        return "";
    }

    public void NextMision(){
        MisionLevel++;
        if (MisionLevel == 3){
            Destroy(MisionThreeBlock);
        }
        if (MisionLevel == 10){
            PlayerController.Instance.NewPosition(Scene2Position);
              SceneManager.LoadScene("HotelLobby");
        }
        if (MisionLevel == 12){
            PlayerController.Instance.NewPosition(Scene3Position);
              SceneManager.LoadScene("HotelExterior");
        }
        if (MisionLevel == 13){
            GameObject.Find("LastMisionCharacter").GetComponent<CharacterFollow>().ActivateFollow();
        }
        if (MisionLevel == 14){
            dialoguePanel.SetActive(false);
            InventoryManager.Instance.WinGame();
        }
        ShowMisionText();
    }

    public void ShowMisionText(){
        if (MisionText() != "") {
            dialoguePanelText.text = MisionText();
            dialoguePanel.SetActive(true);
        }
    }

    public void UpdateMisionTextShort(){
        misionText.text = MisionTextShort();
    }


}
