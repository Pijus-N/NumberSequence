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
    [SerializeField] private RopeRenderer ropeRenderer;
    [SerializeField] private Transform poolPosition;
    [SerializeField] private Transform pointsParent;
    private List<GameObject> pointsPool = new List<GameObject>();
    private int pointInUseIndex;
    float cameraHeight;
    float cameraWidth;

    int currentPoint;
    int pointsCount;


    private void Awake()
    {
        GetCameraInfo();
    }
    private void OnEnable()
    {
        Actions.OnPointClicked += PointClicked;
        Actions.OnLevelsLoaded += PoolPoints;
        Actions.OnLevelFinish += UnLoadLevel;
    }
    private void OnDisable()
    {
        Actions.OnPointClicked -= PointClicked;
        Actions.OnLevelsLoaded -= PoolPoints;
        Actions.OnLevelFinish -= UnLoadLevel;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void LoadLevel(LevelData levelData)
    {
        currentPoint = 1;
        pointInUseIndex = 0;
        pointsCount = levelData.level_data.Count / 2;
        for (int i = 0; i < levelData.level_data.Count; i += 2)
        {
            KeyValuePair<float, float> convertedPointPosition = Utils.ConvertAndNormalizePoints(levelData.level_data[i], levelData.level_data[i + 1], cameraWidth, cameraHeight);
            Vector3 pointPosition = new Vector3(convertedPointPosition.Key, convertedPointPosition.Value, 0);
            GameObject point = pointsPool[pointInUseIndex];
            point.transform.position = pointPosition;
            point.gameObject.GetComponent<Point>().SetInfo((i + 2) / 2);
            pointInUseIndex++;


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
        if (point == currentPoint)
        {
            CorrectPointClicked();
            if (pointsCount == point)
            {
                ropeRenderer.SetTheLastPoint();
                DrawLineBetweenPoints(currentPoint - 2, 0);
            }

        }
        
    }

    void CorrectPointClicked()
    {
        if (currentPoint != 1)
        {
            DrawLineBetweenPoints(currentPoint - 2, currentPoint - 1);
        }
        pointsPool[currentPoint - 1].gameObject.GetComponent<Point>().ChangeStateToClicked();
        currentPoint += 1;

    }

    void DrawLineBetweenPoints(int firstPointIndex, int secondPointIndex)
    {
        Vector3 point1 = new Vector3(pointsPool[firstPointIndex].transform.position.x, pointsPool[firstPointIndex].transform.position.y, 0);
        Vector3 point2 = new Vector3(pointsPool[secondPointIndex].transform.position.x, pointsPool[secondPointIndex].transform.position.y, 0);
        KeyValuePair<Vector3, Vector3> pointsPair = new KeyValuePair<Vector3, Vector3>(point1, point2);
        ropeRenderer.AddToQueue(pointsPair);
    }


    void PoolPoints(LevelList levelList)
    {
        int count = Utils.FindMaximumNumberOfPoints(levelList);
        for (int i = 0; i < count; i++)
        {
            GameObject point = Instantiate(Point, poolPosition.position, Quaternion.identity, pointsParent);
            pointsPool.Add(point);

        }
    }

    void UnLoadLevel()
    {
        foreach(GameObject point in pointsPool)
        {
            point.transform.position = poolPosition.position;
        }
        currentPoint = 1;
    }







}
