using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LevelList levelList;
    [SerializeReference]private LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelList = SaveLoad.ReadJsonData();
        levelManager.LoadLevel(levelList.levels[1]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
