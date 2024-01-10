using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;
using static UnityEditor.PlayerSettings;
using System.Drawing;
using UnityEngine;
using System.Net;

public static class Utils 
{
    /// <summary>
    /// Convert given point to Unity's scene coordination space
    /// </summary>
    /// <param name="x_string">x position of a point</param>
    /// <param name="y_string">y position of a point</param>
    /// <param name="scale_factor">scale factor of points</param>
    /// <returns></returns>
    public static KeyValuePair<float, float> ConvertPointToUnityCoordSpace(string x_string, string y_string, float scale_factor, float dataCoordSpaceWidth, float dataCoordSpaceHeight)
    {
        float x = float.Parse(x_string);
        float y = float.Parse(y_string);
        //Convert point position to the the relative point on screen acording to image size and move points into the middle of the screen
        x = (x - dataCoordSpaceWidth/2) *scale_factor;
        y = (dataCoordSpaceHeight/2 - y)*scale_factor;

        return new KeyValuePair<float, float>(x, y);
    }
    /// <summary>
    /// Find a maximum number of points possible in a level list. Used to pool a maximum possible amount of points
    /// </summary>
    /// <param name="levelList">All levels list with points data</param>
    /// <returns></returns>
    public static int FindMaximumNumberOfPoints(LevelList levelList)
    {
        int max = 0;
        foreach(LevelData levelData in levelList.levels)
        {
            if (levelData.level_data.Count > max)
            {
                max = levelData.level_data.Count;
            }
        }
        return max;
    }
    /// <summary>
    /// Make a circle of positions around the point.
    /// </summary>
    /// <param name="centerPointX">x position of a point's center</param>
    /// <param name="centerPointY">y position of a point's center</param>
    /// <param name="radius">radius of a circle</param>
    /// <param name="numPoints">number of points in a circle</param>
    /// <returns></returns>
    public static List<KeyValuePair<float, float>> MakeACircle(float centerPointX, float centerPointY, float radius, float numPoints)
    {
        List<KeyValuePair<float, float>> circlePoints = new List<KeyValuePair<float, float>>();

        for (int i = 0; i < numPoints; i++)
        {
            float angle = (float)(2 * Math.PI * i / numPoints);
            float x = centerPointX + radius * (float)Math.Cos(angle);
            float y = centerPointY + radius * (float)Math.Sin(angle);

            circlePoints.Add(new KeyValuePair<float, float>(x, y));
        }

        return circlePoints;
    }







}
