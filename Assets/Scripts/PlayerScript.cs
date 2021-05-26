using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private GameStorage gameStorageSO = default;

    public float playerVelocity;
    private Vector3 playerPosition;
    public float boundary;

    private Vector3 startPos = default;

    private void OnEnable()
    {
        GameManager.PrepareLevelAction += PrepareToLevel;
        GamePanelController.OnDragAction += MovePlayer;
    }

    private void OnDisable()
    {
        GameManager.PrepareLevelAction -= PrepareToLevel;
        GamePanelController.OnDragAction -= MovePlayer;
    }

    void Start()
    {
        playerPosition = gameObject.transform.position;
        startPos = transform.position;
    }

//    void Update()
//    {
//        if (gameStorageSO.GameState == GameState.InGame || gameStorageSO.GameState == GameState.OnStart)
//        {

//            // горизонтальное движение
//#if UNITY_EDITOR
//            playerPosition.x += Input.GetAxis("Mouse X") * playerVelocity;
//            playerPosition.z += Input.GetAxis("Mouse Y") * playerVelocity;
//#else
//        playerPosition.x += Input.touches[0].deltaPosition.x * playerVelocity;
//#endif

//            // проверка выхода за границы
//            if (playerPosition.x < -boundary)
//            {
//                playerPosition.x = -boundary;
//            }
//            else if (playerPosition.x > boundary)
//            {
//                playerPosition.x = boundary;
//            }

//            // проверка выхода за границы
//            if (playerPosition.z < -7.5f)
//            {
//                playerPosition.z = -7.5f;
//            }
//            else if (playerPosition.z > -1f)
//            {
//                playerPosition.z = -1f;
//            }

//            // обновим позицию платформы
//            transform.position = playerPosition;
//        }
//    }

    private void MovePlayer(float _distance)
    {
        // горизонтальное движение
#if UNITY_EDITOR
        playerPosition.x += Input.GetAxis("Mouse X") * playerVelocity;
        playerPosition.z += Input.GetAxis("Mouse Y") * playerVelocity;
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

        // проверка выхода за границы
        if (playerPosition.z < -7.5f)
        {
            playerPosition.z = -7.5f;
        }
        else if (playerPosition.z > -1f)
        {
            playerPosition.z = -1f;
        }

        // обновим позицию платформы
        transform.position = playerPosition;
    }

    private void PrepareToLevel(bool restore)
    {
        if (restore)
        {
            playerPosition.x = startPos.x;
            transform.position = startPos;
        }
    }
}
