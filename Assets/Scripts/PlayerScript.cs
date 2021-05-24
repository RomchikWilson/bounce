using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float playerVelocity;
    private Vector3 playerPosition;
    public float boundary;  

    void Start()
    {
        playerPosition = gameObject.transform.position;
    }

    void Update()
    {
        // �������������� ��������
        playerPosition.x += Input.GetAxis("Horizontal") * playerVelocity;

        // �������� ������ �� �������
        if (playerPosition.x < -boundary || playerPosition.x > boundary)
        {
            return;
        }

        // ������� ������� ���������
        transform.position = playerPosition;
    }
}
