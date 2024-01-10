using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject RopePrefab;
    [SerializeField] private Transform poolPosition;
    [SerializeField] private Transform ropeParent;
    [SerializeField] private float maxAnimationTime;
    [SerializeField] private float animationSpeed;
    Queue<KeyValuePair<Vector3, Vector3>> priorityQueue = new Queue<KeyValuePair<Vector3, Vector3>>();
    bool isPreviousLineFinished = true;
    List<GameObject> ropesPool = new List<GameObject>();
    int ropeInUseIndex = 0;
    bool isTheLastPoint = false;
    /// <summary>
    /// Add points to draw queue
    /// </summary>
    /// <param name="pointsPair">pair of points to draw rope between</param>
    public void AddToQueue(KeyValuePair<Vector3, Vector3> pointsPair)
    {
        priorityQueue.Enqueue(pointsPair);
        DrawFromQueue();
    }

    private void OnEnable()
    {
        Actions.OnLevelsLoaded += PoolRopes;
        Actions.OnLevelFinish += ResetRopes;
    }
    private void OnDisable()
    {
        Actions.OnLevelsLoaded -= PoolRopes;
        Actions.OnLevelFinish -= ResetRopes;
    }
    /// <summary>
    /// Animate line renderer as a rope
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveLineRenderer()
    {
        ropesPool[ropeInUseIndex].SetActive(true);
        lineRenderer = ropesPool[ropeInUseIndex].GetComponent<LineRenderer>();
        float elapsedTime = 0f;
        KeyValuePair<Vector3, Vector3> pointPair = priorityQueue.Dequeue();

        Vector3 startPos = pointPair.Key;
        Vector3 endPos = pointPair.Value;
        float duration = CalculateAnimationDuration(startPos, endPos);

        while (elapsedTime < duration)
        {
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, Vector3.Lerp(startPos, endPos, elapsedTime / duration));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isPreviousLineFinished = true;
        ropeInUseIndex++;
        if (isTheLastPoint && priorityQueue.Count <= 0)
        {
            yield return new WaitForSeconds(1);
            Actions.OnLevelFinish();

        }
        DrawFromQueue();
    }

    void DrawFromQueue()
    {
        if (isPreviousLineFinished && priorityQueue.Count > 0)
        {
            isPreviousLineFinished = false;
            StartCoroutine(MoveLineRenderer());
        }
    }

    void PoolRopes(LevelList levelList)
    {
        int count = Utils.FindMaximumNumberOfPoints(levelList);
        for (int i = 0; i < count ; i++)
        {
            GameObject rope = Instantiate(RopePrefab, poolPosition.position, Quaternion.identity, ropeParent);
            ropesPool.Add(rope);

        }
    }
    /// <summary>
    /// The last point in the sequence game has been reached. The last enqueued points pair is the last. Finish level after drawing that rope.
    /// </summary>
    public void SetTheLastPoint()
    {
        isTheLastPoint = true;
    }

    public void ResetRopes()
    {
        foreach (GameObject rope in ropesPool)
        {
            rope.transform.position = poolPosition.position;
            rope.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, 0));
            rope.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, 0));
            rope.SetActive(false);
        }
        isPreviousLineFinished = true;
        ropeInUseIndex = 0;
        isTheLastPoint = false;
        
    }
    /// <summary>
    /// Calculate aniamtion duration by given speed
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <returns></returns>
    float CalculateAnimationDuration(Vector3 startPos, Vector3 endPos)
    {
        float distance = Vector3.Distance(startPos, endPos);

        float animationDuration = distance / animationSpeed;

        return animationDuration;
    }


}
