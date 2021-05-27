using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameStorage gameStorageSO = default;

    public static Action PrepareMainMenuAction = default;
    public static Action ExitAction = default;
    public static Action PrepareNewGame = default;
    public static Action<int> SetLvlAction = default;
    public static Action<Window> OpenWindowAction = default;

    public Button buttonNewGame = default;
    public Text textButtonNewGame = default;
    public Button buttonExit = default;

    public Button buttonRestart = default;
    public Button buttonMenu = default;

    public GameObject mainMenu = default;
    public GameObject gameMenu = default;

    public Text textLVL;

    private void OnEnable()
    {
        PrepareMainMenuAction += PrepareMainMenu;
        PrepareNewGame += PrepareGameMenu;
        SetLvlAction += SetLvl;
        ExitAction += Exit;
        OpenWindowAction += OpenWindow;
    }

    private void OnDisable()
    {
        PrepareMainMenuAction -= PrepareMainMenu;
        PrepareNewGame -= PrepareGameMenu;
        SetLvlAction -= SetLvl;
        ExitAction -= Exit;
        OpenWindowAction -= OpenWindow;
    }

    private void Awake()
    {
        gameStorageSO.GameState = GameState.InMenu;

        buttonNewGame.onClick.AddListener(() => { 
            PrepareNewGame?.Invoke();
            gameStorageSO.GameState = GameState.OnStart;
            OpenWindow(Window.Game);
        });
        buttonExit.onClick.AddListener(() => ExitAction?.Invoke());
        buttonMenu.onClick.AddListener(() => PrepareMainMenuAction?.Invoke());
        buttonRestart.onClick.AddListener(() => GameManager.PrepareLevelAction?.Invoke(true));
    }

    private void PrepareMainMenu()
    {
        textButtonNewGame.text = "Продолжить игру";
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
    }

    private void OpenWindow(Window window)
    {
        CloseAllWindow();
        switch (window)
        {
            case Window.Game:
                gameMenu.SetActive(true);
                break;
            case Window.Menu:
                gameMenu.SetActive(true);
                break;
        }
    }

    private void CloseAllWindow()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(false);
    }

    private void PrepareGameMenu()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void SetLvl(int lvl)
    {
        textLVL.text = $"LVL: {lvl}";
    }
}
