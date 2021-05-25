using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private int countOfLiving = 3;

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.name != "Ball") return;

        Destroy(gameObject);

        if (--countOfLiving == 1)
        {
            print("U WIN!");
        }
    }

}
