using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyScript : MonoBehaviour
{
    public float onStartVelocityEnemy = 1f;
    
    private GameObject ball;
    private float velocityEnemy;
    private Vector3 enemyPositionOld;
    private Vector3 enemyPosition;

    void OnEnable()
    {
        GameManager.increaseASpeedEnemyAction += increaseASpeed;
    }

    void Start()
    {
        velocityEnemy = UnityEngine.Random.Range(0.2f, onStartVelocityEnemy);
        ball = GameObject.Find("Ball");
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

    public void increaseASpeed()
    {
        velocityEnemy += 1;
    }
}
