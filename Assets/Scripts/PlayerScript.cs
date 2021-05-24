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
        // горизонтальное движение
        playerPosition.x += Input.GetAxis("Horizontal") * playerVelocity;

        // проверка выхода за границы
        if (playerPosition.x < -boundary || playerPosition.x > boundary)
        {
            return;
        }

        // обновим позицию платформы
        transform.position = playerPosition;
    }
}
