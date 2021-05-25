using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button buttonRestart = default;
    public Button buttonMenu = default;

    private void Awake()
    {
        buttonRestart.onClick.AddListener(() => GameManager.PrepareLevelAction?.Invoke());
    }
}
