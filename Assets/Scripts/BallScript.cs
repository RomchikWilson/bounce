using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private bool ballIsActive;
    private Vector3 ballPosition;
    private Vector3 ballInitialForce;
    private Rigidbody rigidBody;
    
    private GameObject enemy;
    private EnemyScript enemyScript;

    // GameObject
    public GameObject playerObject;

    // используйте этот метод для инициализации
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        enemy = GameObject.Find("Enemy");
        enemyScript = enemy.GetComponent<EnemyScript>();

        // создаем силу
        ballInitialForce = new Vector3(75.0f, 0f, 300.0f);

        // переводим в неактивное состояние
        ballIsActive = false;

        // запоминаем положение
        ballPosition = transform.position;
    }

    void Update()
    {
        // проверка нажатия на пробел
        if (Input.GetButtonDown("Jump") == true)
        {
            // проверка состояния
            if (!ballIsActive)
            {
                // сброс всех сил
                rigidBody.isKinematic = false;
                // применим силу
                rigidBody.AddForce(ballInitialForce);
                // зададим активное состояние
                ballIsActive = !ballIsActive;
            }

            if (!ballIsActive && playerObject != null)
            {
                // задаем новую позицию шарика
                ballPosition.z = playerObject.transform.position.z;

                // устанавливаем позицию шара
                transform.position = ballPosition;
            }
        }
    }

    void OnCollisionExit(Collision coll)
    {
        enemyScript.SetVelocityEnemy(Random.Range(0.2f, enemyScript.velocityEnemy));
    }
}
