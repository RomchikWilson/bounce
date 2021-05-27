using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameStorage gameStorageSO = default;

    public static Action ExitAction = default;
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
        SetLvlAction += SetLvl;
        ExitAction += Exit;
        OpenWindowAction += OpenWindow;
    }

    private void OnDisable()
    {
        SetLvlAction -= SetLvl;
        ExitAction -= Exit;
        OpenWindowAction -= OpenWindow;
    }

    private void Awake()
    {
        gameStorageSO.GameState = GameState.InMenu;

        buttonNewGame.onClick.AddListener(() => { 
            gameStorageSO.GameState = GameState.OnStart;
            OpenWindow(Window.Game);
        });
        buttonExit.onClick.AddListener(() => ExitAction?.Invoke());
        buttonMenu.onClick.AddListener(() => { OpenWindow(Window.Menu); });
        buttonRestart.onClick.AddListener(() => GameManager.PrepareLevelAction?.Invoke(true));
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
                textButtonNewGame.text = "���������� ����";
                mainMenu.SetActive(true);
                break;
        }
    }

    private void CloseAllWindow()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(false);
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
