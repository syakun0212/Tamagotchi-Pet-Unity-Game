using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using TMPro;
using System;
using System.Threading;

public class DesignMyGameManager2 : MonoBehaviour
{
    private DataManager dataController;
    private static  string gameTitle;
    private static  string gameDescription;

   [SerializeField]
    public Text successMsg;
    private HttpManager http;
    public static CustomGame CustomGameInstance;
    public static string ID="00000";
    public static DesignMyGameManager1 DMGScript1;
  
    void Start()
    {   
        successMsg.gameObject.SetActive(false);
        dataController = FindObjectOfType<DataManager>();
        CustomGameInstance= dataController.GetCustomGame();
        CustomGameInstance.questionList = DesignMyGameManager1.gameQnList;
    }
   
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void PostFaceBook()
    {
        string shareMsg = "I just designed my own custom game, try it out: " + CustomGameInstance.customeGameId;
        
        // links to google, as our app is not on the google play store for download
        
        string facebookShare = "https://www.facebook.com/sharer/sharer.php?u=google.com&quote=" + CustomGameInstance.customeGameId;
        // + Uri.EscapeUriString(shareMsg);
        Application.OpenURL(facebookShare);
        
        // for android
        // StartCoroutine(DataManager.TakeScreenshotAndShare(shareMsg));
    }
    public void PostTelegram(){
        // Post through telegram 
        postGame();
        Debug.Log("Positng done");
        successMsg.gameObject.SetActive(true);
        successMsg.text = "Game Code is: " + CustomGameInstance.customeGameId;
        Application.OpenURL("https://t.me/share/url?url=google.com&text=" + successMsg.text);
        // links to google as we did not upload our app to the google play store
    }

    public void postGame(){
        //// Post the game data to the backend database 
        Debug.Log("Posting game to database now");
        http = new HttpManager();
        var url = "http://172.21.148.165/add_Assignment";
        // var response = http.Post(url, CustomGameInstance);
        Debug.Log("Assignment Posted Successfully");
    }
// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void readNameInput(string title){
        gameTitle = title;
        Debug.Log(title);
        CustomGameInstance.customeGameName = title;
    }

    public void readDescriptionInput(string description){
        gameDescription = description;
        Debug.Log(description);
        CustomGameInstance.customGameDescription = gameDescription;
    }

// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void backButton(){
        SceneManager.LoadScene("StudentWelcomeUI");
    }
}
