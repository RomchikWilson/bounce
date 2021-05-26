using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private bool isDie = false;

    [SerializeField] private GameStorage gameStorageSO = default;

    public bool IsDie => isDie;

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag != Tags.Ball) return;
        gameObject.SetActive(false);
        TargetController.DestryedTargetAction?.Invoke();
        isDie = true;

        GameManager.PrepareLevelAction?.Invoke(false);
        gameStorageSO.GameState = GameState.OnStart;
    }

    public void Config()
    {
        isDie = false;
        gameObject.SetActive(true);
    }
}
