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
    public static Action ExitAction = default;
    public static Action increaseASpeedEnemyAction = default;

    private void Awake()
    {
        GameStartedAction?.Invoke();
    }
}
