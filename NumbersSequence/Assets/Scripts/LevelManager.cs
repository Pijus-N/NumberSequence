using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
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
    [SerializeField] private MatchOrthograhpicSize matchOrthograhpicSize;
    [SerializeField] private AnimationCurve ScaleFactorCurve;//Curve of scale factor according to the points amount in the level

    [SerializeField] private float imageWidth;
    [SerializeField] private float imageHeight;

    private List<GameObject> pointsPool = new List<GameObject>();
    private int pointInUseIndex;//currently picked up point for set up
    float cameraHeight;
    float cameraWidth;

    int currentPointToClick;//Current point to click
    int pointsCount;//points amount in a level

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
    /// <summary>
    /// Load a level
    /// </summary>
    /// <param name="levelData">level data of point for loading</param>
    public void LoadLevel(LevelData levelData)
    {

        currentPointToClick = 1;//Start clicking from the first point 

        pointInUseIndex = 0;//couldnt use i since i+=2
        pointsCount = levelData.level_data.Count / 2;//get points amount in a level from level_data
        for (int i = 0; i < levelData.level_data.Count; i += 2)
        {
            float scale_factor = ScaleFactorCurve.Evaluate(pointsCount); //Get scale factor depending on the number of points
            KeyValuePair<float, float> convertedPointPosition = Utils.ConvertPointToUnityCoordSpace(levelData.level_data[i], levelData.level_data[i + 1], scale_factor, imageWidth, imageHeight);
            matchOrthograhpicSize.SetOrthographicSize(imageWidth*scale_factor, imageHeight * scale_factor);
            Vector3 pointPosition = new Vector3(convertedPointPosition.Key, convertedPointPosition.Value, 0);
            GameObject point = pointsPool[pointInUseIndex];
            point.transform.position = pointPosition;
            point.gameObject.GetComponent<Point>().SetInfo((i + 2) / 2);
            pointInUseIndex++;
            point.SetActive(true);
        }
        //When all points are set up check if any of them are overlapping with number canvas
        for(int i =0; i<pointsCount; i++)
        {
            pointsPool[i].gameObject.GetComponent<Point>().CheckIfCanvasIsOverlaping();
        }

    }
    /// <summary>
    /// Set up camera values
    /// </summary>
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
    /// <summary>
    /// Point clicked
    /// </summary>
    /// <param name="point">number of a clicked point</param>
    void PointClicked(int point)
    {
        if (point == currentPointToClick)
        {
            CorrectPointClicked();
            if (pointsCount == point)
            {
                ropeRenderer.SetTheLastPoint();
                DrawLineBetweenPoints(currentPointToClick - 2, 0);
            }

        }
        
    }
    /// <summary>
    /// Logic if correct point is clicked
    /// </summary>
    void CorrectPointClicked()
    {
        if (currentPointToClick != 1)
        {
            DrawLineBetweenPoints(currentPointToClick - 2, currentPointToClick - 1);
        }
        pointsPool[currentPointToClick - 1].gameObject.GetComponent<Point>().ChangeStateToClicked();
        currentPointToClick += 1;

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
        for(int i =0; i< pointsCount; i++)
        {
            GameObject point = pointsPool[i];
            point.transform.position = poolPosition.position;
            point.gameObject.GetComponent<Point>().ResetAnimation();
            point.SetActive(false);
        }
        currentPointToClick = 1;
    }









}
