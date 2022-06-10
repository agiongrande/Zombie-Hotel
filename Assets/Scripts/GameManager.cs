using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{

    private Coroutine _coRoutine;
    public static GameManager Instance;
    

    [SerializeField] private TextMeshProUGUI gameMessage;
    

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

    public void ShowMessage(string textToShow){
        gameMessage.enabled=true;
        gameMessage.text = textToShow;
        if (this._coRoutine != null){
         StopCoroutine(this._coRoutine);
        }
        this._coRoutine = StartCoroutine (HideMessage());
    }

    public void ShowPermanentMessage(string textToShow){
        gameMessage.enabled=true;
        gameMessage.text = textToShow;
        if (this._coRoutine != null){
            StopCoroutine(this._coRoutine);
        }
    }

    public void HidePermanentMessage(){
        gameMessage.enabled=false;
    }

    IEnumerator HideMessage(){
    	while(true){
            yield return new WaitForSeconds (5.0f);
            gameMessage.enabled=false;
	    }
    }


    public void RestartGame(){
        DeleteNotDestroyItems();
        SceneManager.LoadScene(1);
    }

    private void DeleteNotDestroyItems(){
        Destroy(GameObject.Find("Personaje"));
        Destroy(GameObject.Find("Cameras"));
        Destroy(GameObject.Find("GameInterface"));
        Destroy(GameObject.Find("GameManager"));
        Destroy(GameObject.Find("MisionManager"));
    }

    public void BackToMainMenu(){
        DeleteNotDestroyItems();
        SceneManager.LoadScene(0);
    }
 
}
