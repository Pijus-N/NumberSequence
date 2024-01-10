using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LevelList levelList;
    [SerializeReference] private LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelList = SaveLoad.ReadJsonData();
        Actions.OnLevelsLoaded(levelList);

    }
    /// <summary>
    /// Load level according to the index
    /// </summary>
    /// <param name="levelIndex">index of a level to load</param>
    public void LoadLevel(int levelIndex)
    {
        levelManager.LoadLevel(levelList.levels[levelIndex]);
    }
}
