using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    private bool ballIsActive;
    private Vector3 ballPosition;
    private Vector3 ballInitialForce;
    private Rigidbody rigidBody;
    private bool mouseOn = default;

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
                ballInitialForce = new Vector3(playerObject.transform.position.x * 100, 0f, 500.0f);

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

        //Vector3 g1 = new Vector3(playerObject.transform.position.x, 0, playerObject.transform.position.z);
        //Vector3 g2 = new Vector3(transform.position.x, 0, transform.position.z);
        //SpriteRenderer sprRend = arrow.AddComponent<SpriteRenderer>();
        //sprRend.size = new Vector2(5, 1);

        //��������� ������� ������ �����
        //if (input.getmousebuttondown(0))
        //{
        //    //������ ����������� ����
        //    var ray = camera.main.screenpointtoray(input.mouseposition);
        //}

    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject == enemy)
        {
            GameManager.PrepareLevelAction?.Invoke();
        };
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
