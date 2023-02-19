using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SinglePlayerGameCompletionManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI victoryOrDefeat, scoreText, winText, foodText, waterText;
    private DataManager dataController;
    private SinglePlayerRoundData currentRoundData;
    private SceneLoaderManager scene;
    public string username;
    public Student student;
    private HttpManager http;
    private string shareMsg;

    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataManager>();
        username = dataController.username;
        currentRoundData = dataController.GetSinglePlayerInstance();
        int score = currentRoundData.finalScore;
        int food = currentRoundData.rewardedFood;
        int water = currentRoundData.rewardedWater;
        scoreText.text = score.ToString();
        foodText.text = food.ToString();
        waterText.text = water.ToString();
        GetStudentData();
        student.currentFood += food;
        student.currentWater += water;
        student.highestScore += score;
        UpdateStudentData();
        AddSinglePlayerRoundData();
        AddStatsList();
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

    public void AddSinglePlayerRoundData()
    {
        var url = "http://172.21.148.165/add_SinglePlayerRoundData";
        var responseStr = http.Post(url, currentRoundData);
        Debug.Log(responseStr);
    }
    public void AddStatsList()
    {
        var url = "http://172.21.148.165/add_stats_array";
        var responseStr = http.Post(url, currentRoundData.statList);
        Debug.Log(responseStr);
    }
    public void BackToMenuButtonClick()
    {
        SceneManager.LoadScene("StudentWelcomeUI");
    }
    public void LeaderboardButtonClick()
    {
        SceneManager.LoadScene("LeaderBoardUI");
    }

    public void TwitterButtonClick()
    {
        shareMsg = "I just played a game on Tamagotchi Pet and scored " + currentRoundData.finalScore.ToString() + " come and learn through this fabulous game!";
        
        // links to google, as our app is not on the google play store for download
        string twittershare = "http://twitter.com/share?text=" + shareMsg + "&url=http://google.com";
        // + Uri.EscapeUriString(shareMsg);
        Application.OpenURL(twittershare);
        
        // for android
        // StartCoroutine(DataManager.TakeScreenshotAndShare(shareMsg));
    }

    public void FacebookButtonClick()
    {
        shareMsg = "I just played a game on Tamagotchi Pet and scored " + currentRoundData.finalScore.ToString() + " come and learn through this fabulous game!";
        
        // links to google, as our app is not on the google play store for download
        
        string facebookShare = "https://www.facebook.com/sharer/sharer.php?u=google.com&quote=" + shareMsg;
        // + Uri.EscapeUriString(shareMsg);
        Application.OpenURL(facebookShare);
        
        // for android
        // StartCoroutine(DataManager.TakeScreenshotAndShare(shareMsg));
    }

}
