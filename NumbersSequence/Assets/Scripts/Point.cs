using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Point : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI pointNumberText;
    [SerializeField] private Sprite clickedPointSprite;
    [SerializeField] private Sprite defaultPointSprite;
    [SerializeField] private Animator animator;
    private int pointNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(int number)
    {
        pointNumber = number;
        pointNumberText.text = number.ToString();
        GetComponent<SpriteRenderer>().sprite = defaultPointSprite;

    }

    public void ChangeStateToClicked()
    {
        GetComponent<SpriteRenderer>().sprite = clickedPointSprite;
        animator.SetTrigger("StartFadeOut");
    }

    public int GetNumber()
    {
        return pointNumber;
    }
    

}
