using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]private Canvas canvas;

    private void OnEnable()
    {
        Actions.OnLevelFinish += ShowLevelMenu;
    }
    private void OnDisable()
    {
        Actions.OnLevelFinish -= ShowLevelMenu;
    }

    void ShowLevelMenu()
    {
        canvas.gameObject.SetActive(true);
    }
}
