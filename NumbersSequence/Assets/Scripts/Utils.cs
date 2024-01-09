using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;
using static UnityEditor.PlayerSettings;

public class Utils 
{
    /// <summary>
    /// Convert points from string to float type and call normalization function
    /// </summary>
    /// <param name="x_string">x point in type of string</param>
    /// <param name="y_string">y point in type of string</param>
    /// <param name="cameraWidth">cameraWidth value used for normalization</param>
    /// <param name="cameraHeight">cameraHeight value used for normalization</param>
    /// <returns>Keyvaluepair where key is x position, value is y position</returns>
    public static KeyValuePair<float,float> ConvertAndNormalizePoints(string x_string, string y_string, float cameraWidth, float cameraHeight)
    {
        float x = float.Parse(x_string);
        float y = float.Parse(y_string);

        return NormalizePoints(x, y, cameraWidth, cameraHeight);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x_point">x point in type of float</param>
    /// <param name="y_point">y point in type of float</param>
    /// <param name="cameraWidth">cameraWidth value used for normalization</param>
    /// <param name="cameraHeight">cameraHeight value used for normalization</param>
    /// <returns>Keyvaluepair where key is x position, value is y position</returns>
    static KeyValuePair<float,float> NormalizePoints(float x_point, float y_point, float cameraWidth, float cameraHeight)
    {
        //Calculate the correct aspect ration for diffrent screen size
        float scale_factor = Math.Min(cameraWidth / 1000, cameraHeight / 1000);

        //Linear interpolation of points from one cooridation space to another while preserving the aspect ratio
        float x = x_point * scale_factor;
        float y = y_point * scale_factor;

        //Convert point position to the the relative point on screen acording to screen size
        x = x - cameraWidth / 4;
        y = cameraHeight / 2 - y;

        return new KeyValuePair<float, float>(x, y);
    }

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



}
