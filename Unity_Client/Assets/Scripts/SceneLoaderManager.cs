using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoaderManager : MonoBehaviour
{
    public void LoadPrevScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadMainPageUI()
    {
        SceneManager.LoadScene("MainPageUI");
    }
    public void LoadPersistent()
    {
        SceneManager.LoadScene("Persistent");
    }

    public void LoadStudentLoginUI()
    {
        SceneManager.LoadScene("StudentLoginUI");
    }
    public void LoadTeacherLoginUI()
    {
        SceneManager.LoadScene("TeacherLoginUI");
    }

    public void LoadStudentWelcomeUI()
    {
        SceneManager.LoadScene("StudentWelcomeUI");
    }
    public void LoadTeacherWelcomeUI()
    {
        SceneManager.LoadScene("TeacherWelcomeUI");
    }
    public void LoadVirtualVillageUI()
    {
        SceneManager.LoadScene("VirtualVillageUI");
    }
    public void LoadChangeSkinUI()
    {
        SceneManager.LoadScene("ChangeSkinUI");
    }
    public void LoadGameHistoryUI()
    {
        SceneManager.LoadScene("GameHistoryUI");
    }
    public void LoadViewDetailedStatisticsUI()
    {
        SceneManager.LoadScene("ViewDetailedStatisticsUI");
    }
    public void LoadSingleMultiPlayerSelectionUI()
    {
        SceneManager.LoadScene("SingleMultiPlayerSelectionUI");
    }
    public void LoadWorldAndDifficultySelectionUI()
    {
        SceneManager.LoadScene("WorldAndDifficultySelectionUI");
    }
    public void LoadCharacterSelectionUI()
    {
        SceneManager.LoadScene("CharacterSelectionUI");
    }

    public void LoadViewStatsUI()
    {
        SceneManager.LoadScene("ViewStatsUI");
    }

    public void LoadQuestionBankAddScene()
    {
        SceneManager.LoadScene("Question_Bank_Add");
    }

    public void LoadQuestionBankWorldScene()
    {
        SceneManager.LoadScene("QuestionBankWorldUI");
    }


    public void LoadGameUI()
    {
        var pref = PlayerPrefs.GetString("gameTypeSelected");
        if (pref == "Single")
        {
            SceneManager.LoadScene("SinglePlayerGameUI");
        }
        else if (pref == "Multi")
        {
            SceneManager.LoadScene("MultiPlayerGameUI");
        }

    }

    public void LoadGameCompletionUI()
    {
        var pref = PlayerPrefs.GetString("gameTypeSelected");
        if (pref == "Single")
        {
            SceneManager.LoadScene("SinglePlayerGameCompletionUI");
        }
        else if (pref == "Multi")
        {
            SceneManager.LoadScene("MultiPlayerGameCompletionUI");
        }

    }
    public void LoadSinglePlayerGameCompletionUI()
    {
        SceneManager.LoadScene("SinglePlayerGameCompletionUI");
    }
    public void LoadLeaderboardUI()
    {
        SceneManager.LoadScene("LeaderboardUI");
    }
    public void LoadQuestionBankUI()
    {
        SceneManager.LoadScene("QuestionBank");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");

    }

    public void LoadPostAssignment(){
        SceneManager.LoadScene("PostAssignment-P1");
    }
    public void LoadCustomGame(){
        SceneManager.LoadScene("DesignMyGame-P1");
    }
}