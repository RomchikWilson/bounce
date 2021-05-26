using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TargetController : MonoBehaviour
{
    public int currencyCountTargets = 3;
    public List<TargetScript> targetScripts = new List<TargetScript>();

    public Text textLVL;
    public GameObject enemy = default;
    private int lvl = 1;

    public static Action DestryedTargetAction = default;

    private void awake()
    {
        textLVL.text = "LVL: " + lvl.ToString();
    }

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
            textLVL.text = "LVL: " + lvl.ToString();

            GameManager.PrepareLevelAction?.Invoke();
            GameManager.increaseASpeedEnemyAction?.Invoke();
        }
    }

    private void PrepareLevel()
    {
        targetScripts.ForEach((_target) => _target.gameObject.SetActive(true));

        currencyCountTargets = 3;
    }
}
