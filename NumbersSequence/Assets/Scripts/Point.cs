using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class Point : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointNumberText;
    [SerializeField] private CanvasOverlapDetector canvasOverlapDetector;
    [SerializeField] private Sprite clickedPointSprite;
    [SerializeField] private Sprite defaultPointSprite;
    [SerializeField] private Animator animator;
    [SerializeField] private Canvas numberCanvas;
    private Vector3 canvasInitialPosition;
    private int pointNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        canvasInitialPosition = numberCanvas.GetComponent<RectTransform>().localPosition;
    }
    /// <summary>
    /// Set info for a specific point and reset it for a new game
    /// </summary>
    /// <param name="number">point's number in a sequence</param>
    public void SetInfo(int number)
    {
        pointNumber = number;
        pointNumberText.text = number.ToString();
        GetComponent<SpriteRenderer>().sprite = defaultPointSprite;
        numberCanvas.GetComponent<RectTransform>().localPosition = canvasInitialPosition;
    }
    public void ResetAnimation()
    {
        animator.SetTrigger("Restart");
    }
    /// <summary>
    /// Changes the button state to clicked
    /// </summary>
    public void ChangeStateToClicked()
    {
        GetComponent<SpriteRenderer>().sprite = clickedPointSprite;
        animator.SetTrigger("StartFadeOut");//Start fade out animation
    }

    public int GetNumber()
    {
        return pointNumber;
    }
    /// <summary>
    /// Check if number canvas is overlapping with any other points
    /// </summary>
    public void CheckIfCanvasIsOverlaping()
    {
        if (canvasOverlapDetector.CanvasOverlaps2DObjects())
        {
            PositionNumberCanvasWithNoOverlaps();
        }
    }
    /// <summary>
    /// If number canvas is overlapping with some other points rotate it until it is clear of other points
    /// </summary>
    private void PositionNumberCanvasWithNoOverlaps()
    {
        RectTransform canvasRect = numberCanvas.GetComponent<RectTransform>();
        float width = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;//radius of a circle is roughly a size of the Point sprite
        List<KeyValuePair<float, float>> circleOfPoints = Utils.MakeACircle(0f, 0f, width , 20);

        foreach (KeyValuePair<float, float> point in circleOfPoints)
        {
            if (!canvasOverlapDetector.CanvasOverlaps2DObjects())
            {
                return;
            }
            numberCanvas.transform.localPosition = new Vector3(point.Key, point.Value, 0);
        }

    }



}
