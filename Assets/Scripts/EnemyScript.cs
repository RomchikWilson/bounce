using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameStorage gameStorageSO = default;

    public float onStartVelocityEnemy = 1f;

    public GameObject ball = default;
    private float velocityEnemy;
    private Vector3 enemyPositionOld;
    private Vector3 enemyPosition;

    void OnEnable()
    {
        GameManager.IncreaseASpeedEnemyAction += IncreaseASpeed;
    }

    void Start()
    {
        ball = gameStorageSO.BallScript;
        velocityEnemy = UnityEngine.Random.Range(0.2f, onStartVelocityEnemy);
        enemyPosition = gameObject.transform.position;
    }

    void Update()
    {
        MoveToBall();
    }

    void MoveToBall()
    {
        var moveX = Mathf.Clamp(ball.transform.position.x, -4f, 4f);
        transform.position = Vector3.Lerp(transform.position, new Vector3(moveX, transform.position.y, transform.position.z), velocityEnemy * Time.deltaTime);
    }

    public void IncreaseASpeed()
    {
        velocityEnemy += 1;
    }
}
