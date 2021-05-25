using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float velocityEnemy = 1f;
    
    private GameObject ball;
    private float _velocityEnemy;
    private Vector3 enemyPositionOld;
    private Vector3 enemyPosition;

    void Start()
    {
        _velocityEnemy = Random.Range(0.2f, velocityEnemy);
        ball = GameObject.Find("Ball");
        enemyPosition = gameObject.transform.position;
    }

    void Update()
    {
        MoveToBall();
    }

    void MoveToBall()
    {
        //enemyPosition.x = ball.transform.position.x * _velocityEnemy;

        var moveX = Mathf.Clamp(ball.transform.position.x, -4f, 4f);
        transform.position = Vector3.Lerp(transform.position, new Vector3(moveX, transform.position.y, transform.position.z), _velocityEnemy * Time.deltaTime);

        //transform.position = enemyPosition;
    }

    public void SetVelocityEnemy(float velocity)
    {
        _velocityEnemy = velocity;
    }
}
