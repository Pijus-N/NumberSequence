using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Point;
    float cameraHeight;
    float cameraWidth;
    void Start()
    {
       GetCameraInfo();
        

        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadLevel(LevelData levelData)
    {
        
        for (int i = 0; i < levelData.level_data.Count ; i+=2)
        {
            KeyValuePair<float, float> convertedPointPosition = Utils.ConvertAndNormalizePoints(levelData.level_data[i], levelData.level_data[i + 1], cameraWidth,cameraHeight);
            Vector3 pointPosition = new Vector3(convertedPointPosition.Key, convertedPointPosition.Value, 0);
       
            Instantiate(Point, pointPosition, Quaternion.identity);
        } 

    }

    void GetCameraInfo()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            cameraHeight = mainCamera.orthographicSize * 2f;
            cameraWidth = cameraHeight * mainCamera.aspect;
            Instantiate(Point, new Vector3(cameraWidth / 2, cameraHeight / 2, 0), Quaternion.identity);
        }
        else
        {
            Debug.LogError("Main camera not found.");
        }
    }

  

    
}
