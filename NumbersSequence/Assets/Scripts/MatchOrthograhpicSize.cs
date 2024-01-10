using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchOrthograhpicSize : MonoBehaviour
{

    [SerializeField] private float padding;

    /// <summary>
    /// Sets cameras orthographic view according to the given image size
    /// </summary>
    /// <param name="imageSize">the width and height of the game image size</param>
    public void SetOrthographicSize(float imageSizeWidth, float imageSizeHeight)
    {
        imageSizeWidth += padding; //added padding so image is not too close to the edges of the screen
        imageSizeHeight += padding;
        float screenRatio = Screen.width / Screen.height;
        float targetRatio = imageSizeWidth/ imageSizeHeight;

        if(screenRatio> targetRatio)
        {
            Camera.main.orthographicSize = imageSizeHeight / 2;
        }
        else
        {
            float diffrenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = imageSizeHeight / 2 * diffrenceInSize;
        }

    }

}
