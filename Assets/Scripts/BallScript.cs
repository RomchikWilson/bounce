using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private GameStorage gameStorageSO = default;

    private bool ballIsActive;
    private Vector3 ballPosition;
    private Vector3 ballInitialForce;
    private Rigidbody rigidBody;

    private EnemyScript enemyScript;
    private BoxCollider playerBoxCollider;
    private Vector3 startPos = default;

    private Coroutine coroutine = default;

    // GameObject
    public GameObject playerObject;
    public GameObject enemy;
    public float maxArrowSizeX = 0f;
    public float minArrowSizeX = 0f;
    public float deltaArrowSize = 15f;
    public SpriteRenderer arrow;

    private void OnEnable()
    {
        GameManager.PrepareLevelAction += PrepareToLevel;
        GamePanelController.PointerUpAction += PushBall;
        GamePanelController.OnDragAction += ArrowUpdate;
    }

    private void OnDisable()
    {
        GameManager.PrepareLevelAction -= PrepareToLevel;
        GamePanelController.PointerUpAction -= PushBall;
        GamePanelController.OnDragAction -= ArrowUpdate;
    }

    void Awake()
    {
        gameStorageSO.BallScript = gameObject;
    }

    // используйте этот метод для инициализации
    void Start()
    {
        playerBoxCollider = playerObject.GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();
        startPos = transform.position;

        // переводим в неактивное состояние
        ballIsActive = false;
        playerBoxCollider.isTrigger = true;

        // запоминаем положение
        ballPosition = transform.position;
    }

    void Update()
    {
        DrawingAnArrow();
    }

    private void PushBall(float _distance)
    {
        if (gameStorageSO.GameState == GameState.OnStart)
        {

            gameStorageSO.GameState = GameState.InGame;
            // проверка состояния
            if (!ballIsActive)
            {
                // создаем силу
                //ballInitialForce = new Vector3(playerObject.transform.position.x * 100, 0f, 500.0f);
                ballInitialForce = transform.forward * gameStorageSO.DeltaBallForce * _distance;

                // сброс всех сил
                rigidBody.isKinematic = false;

                // применим силу
                rigidBody.AddForce(ballInitialForce);

                // зададим активное состояние
                ballIsActive = !ballIsActive;

                arrow.enabled = false;
            }

            if (!ballIsActive && playerObject != null)
            {
                // задаем новую позицию шарика
                ballPosition.z = playerObject.transform.position.z;

                // устанавливаем позицию шара
                transform.position = ballPosition;
            }

            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            coroutine = StartCoroutine(WaitToEnd());
        }
    }

    private void ArrowUpdate(float _distance)
    {
        arrow.size = new Vector2(Mathf.Clamp(_distance / deltaArrowSize, minArrowSizeX, maxArrowSizeX), arrow.size.y);
    }

    void DrawingAnArrow()
    {
        transform.LookAt(playerObject.transform);
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject == enemy) /////СРВ
        {
            GameManager.PrepareLevelAction?.Invoke(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(Tags.Player))
        {
            playerBoxCollider.isTrigger = false;
        }
    }

    IEnumerator WaitToEnd()
    {
        yield return new WaitForSecondsRealtime(2f);
        //playerBoxCollider.enabled = true;
        coroutine = null;
    }

    private void PrepareToLevel(bool restor)
    {
        gameStorageSO.GameState = GameState.OnStart;

        //Обновить стрелку
        arrow.enabled = true;

        //Обнулить шар
        ballIsActive = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        transform.position = startPos;

        //Обнулить игрока
        playerBoxCollider.isTrigger = true;
    }
}
