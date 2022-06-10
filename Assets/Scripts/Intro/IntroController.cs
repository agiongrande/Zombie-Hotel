using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class IntroController : MonoBehaviour


{
    MainMenuItem _currentMenu;
    [SerializeField] private string SceneToLoad;
    // Start is called before the first frame update
 
    void Awake()
    {
        var allMenus = GetComponentsInChildren<MainMenuItem>();
        foreach (var item in allMenus)
        {
            item.CloseMenu();
        }    
    }
    void Start()
    {
        if (_currentMenu != null){
            _currentMenu.OpenMenu();
        }
    }

    // Update is called once per frame
    public void ChangeMenu(MainMenuItem newMenu )
    {
        if (newMenu == null) return;

        if (_currentMenu != null){
            _currentMenu.CloseMenu();
        }
        _currentMenu = newMenu;
        _currentMenu.OpenMenu();
    }

    public void CloseMenu()
    {
        if (_currentMenu != null){
            _currentMenu.CloseMenu();
        }
    }

    public void StartGame(){
        SceneManager.LoadScene(SceneToLoad);
    }

    public void ExitGame(){
        #if UNITY_EDITOR 

        EditorApplication.ExitPlaymode();

        #else
            Application.Quit();
        #endif
    }

    public void SetMusicVolume(float value){
        SettingsManager.Instance.SetMusicVolume(value);
    }

    public void SetFxVolume(float value){
        SettingsManager.Instance.SetFxVolume(value);
    }


    public void SetMouseSensibility(float value){
        SettingsManager.Instance.SetMouseSensibility(value);
    }
}
