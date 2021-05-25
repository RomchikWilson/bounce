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

    // ����������� ���� ����� ��� �������������
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        enemy = GameObject.Find("Enemy");
        enemyScript = enemy.GetComponent<EnemyScript>();

        // ������� ����
        ballInitialForce = new Vector3(75.0f, 0f, 300.0f);

        // ��������� � ���������� ���������
        ballIsActive = false;

        // ���������� ���������
        ballPosition = transform.position;
    }

    void Update()
    {
        // �������� ������� �� ������
        if (Input.GetButtonDown("Jump") == true)
        {
            // �������� ���������
            if (!ballIsActive)
            {
                // ����� ���� ���
                rigidBody.isKinematic = false;
                // �������� ����
                rigidBody.AddForce(ballInitialForce);
                // ������� �������� ���������
                ballIsActive = !ballIsActive;
            }

            if (!ballIsActive && playerObject != null)
            {
                // ������ ����� ������� ������
                ballPosition.z = playerObject.transform.position.z;

                // ������������� ������� ����
                transform.position = ballPosition;
            }
        }
    }

    void OnCollisionExit(Collision coll)
    {
        enemyScript.SetVelocityEnemy(Random.Range(0.2f, enemyScript.velocityEnemy));
    }
}
