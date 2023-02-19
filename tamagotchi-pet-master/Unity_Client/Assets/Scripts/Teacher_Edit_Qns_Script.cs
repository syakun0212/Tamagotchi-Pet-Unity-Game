using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Teacher_Edit_Qns_Script : MonoBehaviour
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
    private Dropdown dropdownStandard;
    private QuestionManager conn;

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
        panelObject.gameObject.SetActive(false);

        // button events
        panelObject.transform.Find("Button_Return").GetComponent<Button>().onClick.AddListener(ClickReturn);
        panelObject.transform.Find("Button_Save").GetComponent<Button>().onClick.AddListener(ClickSave);
        panelObject.transform.Find("Button_Clear").GetComponent<Button>().onClick.AddListener(ClickClear);
        panelObject.transform.Find("Button_Delete").GetComponent<Button>().onClick.AddListener(ClickDelete);
        popUp.transform.Find("Popup_Incomplete").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(popupQuestionIncompleteAcknowledge);
        popUp.transform.Find("Popup_Info").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(popupQuestionInfoAcknowledge);
        popUp.transform.Find("Popup_Delete").Find("Button_Cancel").GetComponent<Button>().onClick.AddListener(exitDelete);
        popUp.transform.Find("Popup_Delete").Find("Button_Confirm").GetComponent<Button>().onClick.AddListener(confirmDelete);

        fetchQuestions();
    }
    void Update()
    {
        if (QuestionManager.getStoryQDone)
        {
            QuestionManager.getStoryQDone = false;
            current_question = new Question(QuestionManager.jsonNodeStoryQ);
            populateFields();
            panelObject.gameObject.SetActive(true);
        }
    }
    // Fetches the question from the backend
    private void fetchQuestions()
    {
        QuestionManager.getStoryQDone = false;
        StartCoroutine(conn.getStoryQ(Question_List_Script.editQ.questionId));
    }
    public void ClickReturn()
    {
        SceneManager.LoadScene("QuestionBank");
    }
    public void ClickSave()
    {
        if (validateFields())
        {
            conn.updateStoryQ(current_question);
        }
        else
        {
            popupQuestionIncomplete();
        }
    }
    public void ClickClear()
    {
        entryContainer.Find("InputField_Question").GetComponent<InputField>().text = "";
        entryContainer.Find("InputField_A").GetComponent<InputField>().text = "";
        entryContainer.Find("InputField_B").GetComponent<InputField>().text = "";
        entryContainer.Find("InputField_C").GetComponent<InputField>().text = "";
        entryContainer.Find("InputField_D").GetComponent<InputField>().text = "";
        dropdownAnswer.value = 0;
    }
    public void ClickDelete()
    {
        popUp.SetActive(true);
        popUp.transform.Find("Popup_Delete").gameObject.SetActive(true);
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
    public void confirmDelete()
    {
        conn.deleteStoryQ(current_question);
        SceneManager.LoadScene("QuestionBank");
    }
    public void exitDelete()
    {
        popUp.transform.Find("Popup_Delete").gameObject.SetActive(false);
        popUp.gameObject.SetActive(false);
    }
    // Other functions can set up pop up messages to be displayed. This allows customisation of error messages
    private void setPopupInfoMessage(string message)
    {   // helper function to set popup's message easily
        popUp.transform.Find("Popup_Incomplete").Find("Text").GetComponent<Text>().text = message;
    }
    // Validate if data are valid
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
        current_question.difficultyStandard = dropdownLevel.options[dropdownLevel.value].text;
        //current_question.difficultyStandard = dropdownStandard.options[dropdownStandard.value].text;
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
    // Populate fields on the screen
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
        catch (ArgumentOutOfRangeException)
        {

        }
        dropdownAnswer.value = current_question.answerIndex + 1;
        switch (current_question.difficultyStandard)
        {
            case "Easy":
                dropdownLevel.value = 0;
                break;
            case "Medium":
                dropdownLevel.value = 1;
                break;
            case "Hard":
                dropdownLevel.value = 2;
                break;
        }
        // switch (current_question.questionStandard)
        // {
        //     case "simple":
        //         dropdownLevel.value = 0;
        //         break;
        //     case "complex":
        //         dropdownLevel.value = 1;
        //         break;
        // }
    }
    private void popupQuestionIncomplete()
    {
        popUp.SetActive(true);
        popUp.transform.Find("Popup_Incomplete").gameObject.SetActive(true);
    }
}
