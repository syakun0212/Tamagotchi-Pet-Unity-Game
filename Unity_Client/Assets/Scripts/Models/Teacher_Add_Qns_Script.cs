using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

// For Question_Bank_Add Scene
public class Teacher_Add_Qns_Script : MonoBehaviour
{
    // variables
    private Question current_question;

    // display
    public GameObject panelObject;
    public GameObject mainContentPanel;
    private Transform entryContainer;
    private GameObject popUp;
    private Dropdown dropdownAnswer;
    private Dropdown dropdownLevel;
    //private Dropdown dropdownStandard;
    private QuestionManager conn;
    private Question question;
    private Generator generator;
    public static int k = Generator.i;

    void Start()
    {
        // display   
        popUp = mainContentPanel.transform.Find("Panel_Messages").gameObject;
        entryContainer = panelObject.transform.Find("Panel_Question_Add_Teacher");
        dropdownAnswer = entryContainer.Find("Dropdown_Answer").GetComponent<Dropdown>();
        //dropdownLevel = entryContainer.Find("Dropdown_Standard").GetComponent<Dropdown>();
        dropdownLevel = entryContainer.Find("Dropdown_Level").GetComponent<Dropdown>();
        popUp.SetActive(false);
        conn = (QuestionManager)transform.GetComponent(typeof(QuestionManager));
        k = Generator.i;

        // button events
        panelObject.transform.Find("Button_Return").GetComponent<Button>().onClick.AddListener(ClickReturn);
        panelObject.transform.Find("Button_Save").GetComponent<Button>().onClick.AddListener(ClickSave);
        panelObject.transform.Find("Button_Clear").GetComponent<Button>().onClick.AddListener(ClickClear);
        popUp.transform.Find("Popup_Incomplete").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(popupQuestionIncompleteAcknowledge);
        popUp.transform.Find("Popup_Info").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(popupQuestionInfoAcknowledge);
        current_question = new Question(World_Select_Script.worldChoice, Section_Select_Script.sectionChoice);
    }
    public void ClickReturn()
    {
        SceneManager.LoadScene("QuestionBank");
    }
    public void ClickSave()
    {
       
        if (validateFields())
        {
            //string a = k.ToString();
            current_question.questionId = k.ToString();
            conn.addStoryQ(current_question);
            SceneManager.LoadScene("QuestionBank");
        }
        else
        {
              popupQuestionIncomplete();
        }
        
    }
    // Clear all fields
    public void ClickClear()
    {
        entryContainer.Find("InputField_Question").GetComponent<InputField>().text = "";
        entryContainer.Find("InputField_A").GetComponent<InputField>().text = "";
        entryContainer.Find("InputField_B").GetComponent<InputField>().text = "";
        entryContainer.Find("InputField_C").GetComponent<InputField>().text = "";
        entryContainer.Find("InputField_D").GetComponent<InputField>().text = "";
        dropdownAnswer.value = 0;
    }
    public void popupQuestionIncompleteAcknowledge()
    {
        popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(false);
        popUp.gameObject.SetActive(false);
    }
    public void popupQuestionInfoAcknowledge()
    {
        popUp.transform.Find("Popup_Info").gameObject.SetActive(false);
        popUp.gameObject.SetActive(false);
    }
    private void setPopupInfoMessage(string message)
    {   // helper function to set popup's message easily
        popUp.transform.Find("Popup_Incomplete").Find("Text").GetComponent<Text>().text = message;
    }
    // Validate if the data are valid
    private bool validateFields()
    {
        string ans;
        current_question.questionText = entryContainer.Find("InputField_Question").GetComponent<InputField>().text;

        current_question.answersText = new List<string>();
        ans = entryContainer.Find("InputField_A").GetComponent<InputField>().text;
        if (ans != "") current_question.answersText.Add(ans);
        ans = entryContainer.Find("InputField_B").GetComponent<InputField>().text;
        if (ans != "") current_question.answersText.Add(ans);
        ans = entryContainer.Find("InputField_C").GetComponent<InputField>().text;
        if (ans != "") current_question.answersText.Add(ans);
        ans = entryContainer.Find("InputField_D").GetComponent<InputField>().text;
        if (ans != "") current_question.answersText.Add(ans);

        // There must be a question string
        if (current_question.questionText == "")
        {
            setPopupInfoMessage("A question must be given");
            return false;
        }

        if (current_question.answersText.Count < 2)
        {
            setPopupInfoMessage("There must be at least two answers");
            return false;
        }
        try
        {
            for (int i = 0; i < current_question.answersText.Count - 1; i++)
            {
                for (int j = i + 1; j < current_question.answersText.Count; j++)
                {
                    if (current_question.answersText[i].Equals(current_question.answersText[j]))
                    {
                        setPopupInfoMessage("All answers must be unique");
                        return false;
                    }
                }
            }
        }
        catch (ArgumentOutOfRangeException) { }

        if (dropdownAnswer.value == 0)
        {
            setPopupInfoMessage("Please choose an answer");
            return false;
        }
        current_question.answerIndex = dropdownAnswer.value - 1;
        // current_question.questionStandard = dropdownLevel.options[dropdownLevel.value].text;
        current_question.difficultyStandard = dropdownLevel.options[dropdownLevel.value].text;
        populateFields();
        switch (current_question.answerIndex)
        {
            case 2:
                {
                    if (current_question.answersText.Count <= 2)
                    {
                        setPopupInfoMessage("With two answers, C and D cannot be the correct answer");
                        return false;
                    }
                    break;
                }
            case 3:
                {
                    if (current_question.answersText.Count == 2)
                    {
                        setPopupInfoMessage("With two answers, C and D cannot be the correct answer");
                        return false;
                    }
                    else if (current_question.answersText.Count == 3)
                    {
                        setPopupInfoMessage("With three answers, D cannot be the correct answer");
                        return false;
                    }
                    break;
                }
        }
        return true;
    }

    // UI instructions to populate the input fields and dropdowns
    private void populateFields()
    {
        ClickClear();
        entryContainer.Find("InputField_Question").GetComponent<InputField>().text = current_question.questionText;
        try
        {
            entryContainer.Find("InputField_A").GetComponent<InputField>().text = current_question.answersText[0];
            entryContainer.Find("InputField_B").GetComponent<InputField>().text = current_question.answersText[1];
            entryContainer.Find("InputField_C").GetComponent<InputField>().text = current_question.answersText[2];
            entryContainer.Find("InputField_D").GetComponent<InputField>().text = current_question.answersText[3];
        }
        catch (ArgumentOutOfRangeException) { }
        dropdownAnswer.value = current_question.answerIndex + 1;
    }
    private void popupQuestionIncomplete()
    {
        popUp.SetActive(true);
        popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(true);
    }
}
