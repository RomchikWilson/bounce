using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetController : MonoBehaviour
{
    public int currencyCountTargets = 3;
    public List<TargetScript> targetScripts = new List<TargetScript>();

    public GameObject enemy;
    private EnemyScript enemyScript;
    private float velocityEnemy;

    public static Action DestryedTargetAction = default;

    private void awake()
    {
        velocityEnemy = enemy.GetComponent<EnemyScript>().velocityEnemy;
        enemyScript = enemy.GetComponent<EnemyScript>();
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
            GameManager.PrepareLevelAction?.Invoke();

            velocityEnemy += 0.5f;

            enemyScript.SetVelocityEnemy(UnityEngine.Random.Range(0.2f, velocityEnemy));
        }
    }

    private void PrepareLevel()
    {
        targetScripts.ForEach((_target) => _target.gameObject.SetActive(true));

        currencyCountTargets = 3;
    }
}
