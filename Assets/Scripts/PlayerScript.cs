using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float playerVelocity;
    private Vector3 playerPosition;
    public float boundary;

    private Vector3 startPos = default;

    private void OnEnable()
    {
        GameManager.PrepareLevelAction += PrepareToLevel;
    }

    void Start()
    {
        playerPosition = gameObject.transform.position;
        startPos = transform.position;
    }

    void Update()
    {
        // �������������� ��������
#if UNITY_EDITOR
        playerPosition.x += Input.GetAxis("Mouse X") * playerVelocity;
#else
        playerPosition.x += Input.touches[0].deltaPosition.x * playerVelocity;
#endif

        // �������� ������ �� �������
        if (playerPosition.x < -boundary)
        {
            playerPosition.x = -boundary;
        }
        else if (playerPosition.x > boundary)
        {
            playerPosition.x = boundary;
        }

        // ������� ������� ���������
        transform.position = playerPosition;
    }

    private void PrepareToLevel()
    {
        playerPosition.x = startPos.x;
        transform.position = startPos;
    }
}
