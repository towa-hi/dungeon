using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private static MainMenuController ins;
    public static MainMenuController instance {get{return instance;}}

    [SerializeField] public GameObject mainMenu;
    [SerializeField] public GameObject loadMenu;
    [SerializeField] public GameObject settingsMenu;
    [SerializeField] public MenuState state;
    
    public enum MenuState
    {
        Main,
        Load,
        Settings,
        Off
    }

    private void Awake()
    {
        // boilerplate singleton code
        
        // if exists already
        if (ins != null && ins != this)
        {
            // kill self
            Destroy(this.gameObject);
            Debug.LogError("MainMenuController encountered duplicate singleton, deleted self");
            return;
        }

        ins = this;
        
        //make the gameobject for this persist across scenes
        DontDestroyOnLoad(this.gameObject);
        
        SetState(MenuState.Main);
    }

    public void SetState(MenuState newState)
    {
        state = newState;
        mainMenu.SetActive(false); 
        loadMenu.SetActive(false);
        settingsMenu.SetActive(false);
        switch (state)
        {
            case MenuState.Main:
                mainMenu.SetActive(true);
                break;
            case MenuState.Load:
                loadMenu.SetActive(true);
                break;
            case MenuState.Settings:
                settingsMenu.SetActive(true);
                break;
            case MenuState.Off:
                // keep submenus off
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    // button functions

    public void OnMainMenuLoadGame()
    {
        SetState(MenuState.Load);
    }

    public void OnMainMenuStartGame()
    {
        SetState(MenuState.Off);
    }

    public void OnMainMenuSettings()
    {
        SetState(MenuState.Settings);
    }

    public void OnReturnToMainMenu()
    {
        SetState(MenuState.Main);
    }

    public void OnQuitGame()
    {
        #if UNITY_STANDALONE
        Application.Quit();
        #endif
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #endif
    }
}
