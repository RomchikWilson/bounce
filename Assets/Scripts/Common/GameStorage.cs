using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameStorage", fileName = "GameStorageSO")]
public class GameStorage : ScriptableObject
{
    [SerializeField] private GameState gameState = GameState.InMenu;
    [SerializeField] private float ballForce = 0f;


    public GameState GameState { get => gameState; set => gameState = value; }
    public float DeltaBallForce { get => ballForce; set => ballForce = value; }
    public GameObject BallScript { get; set; }
}
