using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private bool isDie = false;

    public bool IsDie => isDie;

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.name != "Ball") return;
        TargetController.DestryedTargetAction?.Invoke();

        isDie = true;
        gameObject.SetActive(false);
    }

    public void Config()
    {
        isDie = false;
        gameObject.SetActive(true);
    }
}
