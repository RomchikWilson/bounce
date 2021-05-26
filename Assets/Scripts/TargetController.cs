using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TargetController : MonoBehaviour
{
    [SerializeField] private GameStorage gameStorageSO = default;

    public int currencyCountTargets = 3;
    public List<TargetScript> targetScripts = new List<TargetScript>();

    public GameObject enemy = default;
    private int lvl = 1;

    public static Action DestryedTargetAction = default;

    private void OnEnable()
    {
        DestryedTargetAction += DestryedTarget;

        GameManager.PrepareLevelAction += PrepareLevel;
    }

    private void DestryedTarget()
    {
        currencyCountTargets--;

        if (currencyCountTargets == 0)
        {
            lvl++;
            UIController.SetLvlAction?.Invoke(lvl);

            GameManager.PrepareLevelAction?.Invoke(true);
            GameManager.IncreaseASpeedEnemyAction?.Invoke();
            gameStorageSO.GameState = GameState.OnStart;
        }
    }

    private void PrepareLevel(bool restoreTargets = false)
    {
        if (!restoreTargets) return;

        targetScripts.ForEach((_target) => _target.gameObject.SetActive(true));
        currencyCountTargets = 3;

        
    }
}
