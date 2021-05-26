using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static Action PrepareMainMenuAction = default;
    public static Action ExitAction = default;
    public static Action PrepareNewGame = default;

    public Button buttonNewGame = default;
    public Button buttonExit = default;

    public Button buttonRestart = default;
    public Button buttonMenu = default;

    public GameObject mainMenu = default;
    public GameObject gameMenu = default;

    private void Awake()
    {
        PrepareMainMenuAction += PrepareMainMenu;
        PrepareNewGame += PrepareGameMenu;
        ExitAction += Exit;

        buttonNewGame.onClick.AddListener(() => PrepareNewGame?.Invoke());
        buttonExit.onClick.AddListener(() => ExitAction?.Invoke());
        buttonMenu.onClick.AddListener(() => PrepareMainMenuAction?.Invoke());
        buttonRestart.onClick.AddListener(() => GameManager.PrepareLevelAction?.Invoke());
    }

    private void PrepareMainMenu()
    {
        mainMenu.SetActive(true);
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
}
