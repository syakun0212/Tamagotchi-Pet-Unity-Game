using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WorldAndStageSelectionManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI requirementButton, designButton, implementationButton;
    [SerializeField]
    private TextMeshProUGUI section1, section2, section3, section4, level1, level2, level3, level4;
    [SerializeField]
    private Button design, implement, sec2, sec3, sec4, lvl2, lvl3, lvl4;
    void Start () {
        design.interactable = false;
        implement.interactable = false;
        sec2.interactable = false;
        sec3.interactable = false;
        sec4.interactable = false;
        lvl2.interactable = false;
        lvl3.interactable = false;
        // lvl4.interactable = false;
        // checkAndUnlock();
    }
    void checkAndUnlock() {
        // get shit and unlock stages from user lmao
    }
    
    public void requirementButtonSelected()
    {
        PlayerPrefs.SetString("worldSelected", "requirement");
        requirementButton.color = Color.red;
        designButton.color = Color.black;
        implementationButton.color = Color.black;
    }
    public void designButtonSelected()
    {
        PlayerPrefs.SetString("worldSelected", "design");
        requirementButton.color = Color.black;
        designButton.color = Color.red;
        implementationButton.color = Color.black;
    }
    public void implementationButtonSelected()
    {
        PlayerPrefs.SetString("worldSelected", "implementation");
        requirementButton.color = Color.black;
        designButton.color = Color.black;
        implementationButton.color = Color.red;
    }
    public void section1Selected()
    {
        PlayerPrefs.SetString("sectionSelected", "1");
        section1.color = Color.red;
        section2.color = Color.black;
        section3.color = Color.black;
        section4.color = Color.black;
    }
        public void section2Selected()
    {
        PlayerPrefs.SetString("sectionSelected", "2");
        section1.color = Color.black;
        section2.color = Color.red;
        section3.color = Color.black;
        section4.color = Color.black;
    }
        public void section3Selected()
    {
        PlayerPrefs.SetString("sectionSelected", "3");
        section1.color = Color.black;
        section2.color = Color.black;
        section3.color = Color.red;
        section4.color = Color.black;
    }
        public void section4Selected()
    {
        PlayerPrefs.SetString("sectionSelected", "4");
        section1.color = Color.black;
        section2.color = Color.black;
        section3.color = Color.black;
        section4.color = Color.red;
    }
        public void level1Selected()
    {
        PlayerPrefs.SetString("levelSelected", "easy");
        level1.color = Color.red;
        level2.color = Color.black;
        level3.color = Color.black;
    }
        public void level2Selected()
    {
        PlayerPrefs.SetString("levelSelected", "medium");
        level1.color = Color.black;
        level2.color = Color.red;
        level3.color = Color.black;
    }
        public void level3Selected()
    {
        PlayerPrefs.SetString("levelSelected", "hard");
        level1.color = Color.black;
        level2.color = Color.black;
        level3.color = Color.red;
    }
    public void ComfirmButtonClicked() {
        // check both playerpref steady then continue
        if (PlayerPrefs.HasKey("worldSelected") && PlayerPrefs.HasKey("sectionSelected") && PlayerPrefs.HasKey("levelSelected")) {
            SceneManager.LoadScene("CharacterSelectionUI");
        }
    }

    public void BackSelected() {
        SceneManager.LoadScene("SingleMultiPlayerSelectionUI");
    }
}
