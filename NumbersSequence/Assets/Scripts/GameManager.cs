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

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevel(int levelIndex)
    {
        levelManager.LoadLevel(levelList.levels[levelIndex]);

    }
}
