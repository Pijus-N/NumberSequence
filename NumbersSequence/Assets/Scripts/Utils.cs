using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;
using static UnityEditor.PlayerSettings;

public class Utils 
{
    // Start is called before the first frame update
    public static KeyValuePair<float,float> ConvertAndNormalizePoints(string x_string, string y_string, float cameraWidth, float cameraHeight)
    {
        float x = float.Parse(x_string);
        float y = float.Parse(y_string);

        return NormalizePoints(x, y, cameraWidth, cameraHeight);

    }

    static KeyValuePair<float,float> NormalizePoints(float x_point, float y_point, float cameraWidth, float cameraHeight)
    {
        //Calculate the correct aspect ration for diffrent screen size
        float scale_factor = Math.Min(cameraWidth / 1000, cameraHeight / 1000);

        //Linear interpolation of points from one cooridation space to another
        float x = x_point * scale_factor;
        float y = y_point * scale_factor;

        //Convert point position to the the correct point on screen
        x = x - cameraWidth / 4;
        y = cameraHeight / 2 - y;

        return new KeyValuePair<float, float>(x, y);
    }



}
