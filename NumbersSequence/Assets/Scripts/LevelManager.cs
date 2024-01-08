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

    int currentPoint = 1;
    int pointsCount;
    List<GameObject> pointsInUse = new List<GameObject>();

    private void Awake()
    {
        GetCameraInfo();
    }
    private void OnEnable()
    {
        Actions.OnPointClicked += PointClicked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadLevel(LevelData levelData)
    {
        pointsCount = levelData.level_data.Count / 2;
        for (int i = 0; i < levelData.level_data.Count ; i+=2)
        {
            KeyValuePair<float, float> convertedPointPosition = Utils.ConvertAndNormalizePoints(levelData.level_data[i], levelData.level_data[i + 1], cameraWidth,cameraHeight);
            Vector3 pointPosition = new Vector3(convertedPointPosition.Key, convertedPointPosition.Value, 0);
            GameObject point = Instantiate(Point, pointPosition, Quaternion.identity);
            point.gameObject.GetComponent<Point>().SetInfo((i + 2) / 2);
            pointsInUse.Add(point);
            
        } 

    }

    void GetCameraInfo()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            cameraHeight = mainCamera.orthographicSize * 2f;
            cameraWidth = cameraHeight * mainCamera.aspect;
        }
        else
        {
            Debug.LogError("Main camera not found.");
        }
    }

    void PointClicked(int point)
    {
        if(pointsCount == point)
        {
            Debug.Log("Level finsihed");
        }

        if (point == currentPoint)
        {
            CorrectPointClicked();
            
        }
    }

    void CorrectPointClicked()
    {
        pointsInUse[currentPoint - 1].gameObject.GetComponent<Point>().ChangeStateToClicked();
        currentPoint += 1;
        

    }

  

    
}
