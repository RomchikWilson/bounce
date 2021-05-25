using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private bool ballIsActive;
    private Vector3 ballPosition;
    private Vector3 ballInitialForce;
    private Rigidbody rigidBody;

    private EnemyScript enemyScript;
    private BoxCollider playerBoxCollider;
    private Vector3 startPos = default;

    // GameObject
    public GameObject playerObject;
    public GameObject enemy;
    public GameObject arrow;

    private void OnEnable()
    {
        GameManager.PrepareLevelAction += PrepareToLevel;
    }

    // ����������� ���� ����� ��� �������������
    void Start()
    {

        playerBoxCollider = playerObject.GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();
        enemyScript = enemy.GetComponent<EnemyScript>();
        startPos = transform.position;

        // ��������� � ���������� ���������
        ballIsActive = false;
        playerBoxCollider.enabled = false;

        // ���������� ���������
        ballPosition = transform.position;
    }

    void Update()
    {
        DrawingAnArrow();

        // �������� ������� �� ������
        if (Input.GetButtonDown("Jump") == true)
        {
            // �������� ���������
            if (!ballIsActive)
            {
                // ������� ����
                ballInitialForce = new Vector3(playerObject.transform.position.x * 100, 0f, 300.0f);

                // ����� ���� ���
                rigidBody.isKinematic = false;

                // �������� ����
                rigidBody.AddForce(ballInitialForce);

                // ������� �������� ���������
                ballIsActive = !ballIsActive;

                arrow.SetActive(false);
            }

            if (!ballIsActive && playerObject != null)
            {
                // ������ ����� ������� ������
                ballPosition.z = playerObject.transform.position.z;

                // ������������� ������� ����
                transform.position = ballPosition;
            }

            StartCoroutine(WaitToEnd());
        }
    }

    void DrawingAnArrow()
    {
        transform.LookAt(playerObject.transform);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
 
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startDirection = touchPosition - transform.position;
                    break;
 
                case TouchPhase.Moved:
                    var currentDirection = touchPosition - transform.position;
                    float angle = Vector2.SignedAngle(startDirection, currentDirection);
                    var euler = transform.eulerAngles;
                    euler.z += angle;
                    transform.eulerAngles = euler;
                    startDirection = currentDirection;
                break;
            }
        }

    }


    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject == enemy)
        {
            GameManager.PrepareLevelAction?.Invoke();
        };

        enemyScript.SetVelocityEnemy(Random.Range(0.2f, enemyScript.velocityEnemy));
    }

    IEnumerator WaitToEnd()
    {
        yield return new WaitForSecondsRealtime(2f);
        playerBoxCollider.enabled = true;
    }

    private void PrepareToLevel()
    {
        //�������� �������
        arrow.SetActive(true);

        //�������� ���
        ballIsActive = false;
        rigidBody.velocity = Vector3.zero;
        transform.position = startPos;

        //�������� ������
        playerBoxCollider.enabled = false;
    }
}
