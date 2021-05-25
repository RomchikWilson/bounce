using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static Action GameStartedAction = default;
    public static Action PrepareLevelAction = default;
    public static Action LevelStartAction = default;
    public static Action LeveFinishAction = default;

    private void Awake()
    {
        GameStartedAction?.Invoke();
    }
}
