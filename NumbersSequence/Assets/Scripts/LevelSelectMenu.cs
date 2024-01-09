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
        Actions.OnLevelFinish += ShowLevelMenu;

    }
    private void OnDisable()
    {
        Actions.OnLevelsLoaded -= CreateButtons;
        Actions.OnLevelFinish -= ShowLevelMenu;

    }

    public void CreateButtons(LevelList levelList)
    {
        
        for(int i=0; i< levelList.levels.Count; i++)
        {
            GameObject buttonObj = Instantiate(buttonPrefab, contentParent);
            Button button = buttonObj.GetComponent<Button>();
            int index = i;
            button.onClick.AddListener(() => StartLevel(index));
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
        LevelsScrollView.SetActive(false);
    }

    void ShowLevelMenu()
    {
        LevelsScrollView.gameObject.SetActive(true);
    }




}
