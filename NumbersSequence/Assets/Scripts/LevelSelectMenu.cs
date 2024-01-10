using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class LevelSelectMenu : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject LevelsScrollView;
    [SerializeField] private Transform contentParent;

    private void OnEnable()
    {
        Actions.OnLevelsLoaded += CreateButtons;
    }
    private void OnDisable()
    {
        Actions.OnLevelsLoaded -= CreateButtons;
    }
    /// <summary>
    /// Create buttons dinamically according to the given amount of levels
    /// </summary>
    /// <param name="levelList">all levels list</param>
    public void CreateButtons(LevelList levelList)
    {
        
        for(int i=0; i< levelList.levels.Count; i++)
        {
            GameObject buttonObj = Instantiate(buttonPrefab, contentParent);
            Button button = buttonObj.GetComponent<Button>();
            int index = i;
            button.onClick.AddListener(() => StartLevel(index));//Add on click behaviour
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Level ");
            stringBuilder.Append((i + 1).ToString());
            button.GetComponentInChildren<TextMeshProUGUI>().text = stringBuilder.ToString();
            
        }

    }

    void StartLevel(int index)
    {
        gameManager.LoadLevel(index);
        HideLevelMenu();
    }

    void HideLevelMenu()
    {
        gameObject.SetActive(false);
    }







}
