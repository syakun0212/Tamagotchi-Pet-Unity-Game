using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class MultiPlayerGameCompletionManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText, foodText, waterText, winText, victoryOrDefeatText;
    private DataManager dataController;
    private MultiPlayerRoundData currentRoundData;
    public string username;
    public Student student;
    private HttpManager http;
    private string shareMsg;
    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataManager>();
        currentRoundData = dataController.GetMultiPlayerInstance();
        username = dataController.username;
        
        int food = currentRoundData.rewardedFood;
        int water = currentRoundData.rewardedWater;
        int win = currentRoundData.winPoint;
        int playerScore = currentRoundData.finalScore;
        int opponentScore = currentRoundData.opponentStatList.Sum(item => item.timing);
        currentRoundData.opponentCharacterUsed = new Pet("Pet1", 0, "Add 5 Seconds", 5, 5);
        scoreText.text = playerScore.ToString();
        winText.text = win.ToString();
        foodText.text = food.ToString();
        waterText.text = water.ToString();

        if (playerScore >= opponentScore)
        {
            victoryOrDefeatText.text = "victory";
        }
        else
        {
            victoryOrDefeatText.text = "defeat";
        }
        // update user data in server
        GetStudentData();
        student.currentFood += food;
        student.currentWater += water;
        student.highestScore += playerScore;
        UpdateStudentData();
        AddMultiPlayerRoundData();
    }
    public void GetStudentData()
    {
        http = new HttpManager();
        var url = "http://172.21.148.165/get_userData?username=" + username; // add query parameter using username
        var responseStr = http.Post(url, "");
        student = JsonConvert.DeserializeObject<Student>(responseStr);
    }

    public void UpdateStudentData()
    {
        var url = "http://172.21.148.165/update_userData?username=" + username; // add query parameter using username
        var responseStr = http.Post(url, student);
        Debug.Log(responseStr);
    }
    public void AddMultiPlayerRoundData()
    {
        var url = "http://172.21.148.165/add_MultiPlayerRoundData";
        var responseStr = http.Post(url, currentRoundData);
        Debug.Log(responseStr);
    }

    public void BackToMenuButtonClick()
    {
        SceneManager.LoadScene("StudentWelcomeUI");
    }
    public void LeaderboardButtonClick()
    {
        SceneManager.LoadScene("LeaderboardUI");
    }

    public void TwitterButtonClick()
    {
        if (victoryOrDefeatText.text == "victory") {
            shareMsg = "I just obtained victory in Tamagotchi Pet Multiplayer, winners like me score " + currentRoundData.finalScore.ToString() + " come and try to beat me!";
        }
        else {
            shareMsg = "Unfortunately, i just got defeated in Tamagotchi Pet Multiplayer, i will work harder and score higher than " + currentRoundData.finalScore.ToString() + " next time.";
        }

        // links to google, as our app is not on the google play store for download
        string twittershare = "http://twitter.com/share?text=" + shareMsg + "&url=http://google.com";
        // + Uri.EscapeUriString(shareMsg);
        Application.OpenURL(twittershare);
        
        // for android
        // StartCoroutine(DataManager.TakeScreenshotAndShare(shareMsg));
    }

    public void FacebookButtonClick()
    {
        if (victoryOrDefeatText.text == "victory") {
            shareMsg = "I just obtained victory in Tamagotchi Pet Multiplayer, winners like me score " + currentRoundData.finalScore.ToString() + " come and try to beat me!";
        }
        else {
            shareMsg = "Unfortunately, i just got defeated in Tamagotchi Pet Multiplayer, i will work harder and score higher than " + currentRoundData.finalScore.ToString() + " next time.";
        }

        // links to google, as our app is not on the google play store for download
        string facebookShare = "https://www.facebook.com/sharer/sharer.php?u=google.com&quote=" + shareMsg;
        // + Uri.EscapeUriString(shareMsg);
        Application.OpenURL(facebookShare);
        
        // for android
        // StartCoroutine(DataManager.TakeScreenshotAndShare(shareMsg));
    }


}
