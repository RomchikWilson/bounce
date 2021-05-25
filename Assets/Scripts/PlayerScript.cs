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
#if UNITY_EDITOR
        playerPosition.x += Input.GetAxis("Mouse X") * playerVelocity;
#else
        playerPosition.x += Input.touches[0].deltaPosition.x * playerVelocity;
#endif

        // проверка выхода за границы
        if (playerPosition.x < -boundary)
        {
            playerPosition.x = -boundary;
        }
        else if (playerPosition.x > boundary)
        {
            playerPosition.x = boundary;
        }

        // обновим позицию платформы
        transform.position = playerPosition;
    }
}
