using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MenuController : MonoBehaviour
{
    public static Action NewGame = default;
    public static Action Exit = default;

    public Button buttonStart = default;
    public Button buttonExit = default;

    private void OnEnable()
    {
        NewGame += PlayPressed;
        Exit += ExitPressed;
    }

    private void Awake()
    {
        buttonStart.onClick.AddListener(() => NewGame?.Invoke());
        buttonExit.onClick.AddListener(() => Exit?.Invoke());
    }

    void PlayPressed()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    void ExitPressed()
    {
        Application.Quit();
    }
}
