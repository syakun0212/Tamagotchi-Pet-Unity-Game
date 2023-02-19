using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;
using Newtonsoft.Json;
using TMPro;

public class GameHistoryManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI AccuracyText, SpeedText;
    public HistoryRowUi rowUi;
    public string username;
    private HttpManager http;
    private DataManager dataController;
    public List<SinglePlayerRoundData> playerRoundData;
    private int currHistoryPage = 0;
    private int studentIndex;
    int MaxRows = 4; // Number of rows to be shown on one page
    void Start()
    {
        // dataController = FindObjectOfType<DataManager>();
        // username = dataController.username;
        username = "jeslynlxy";
        GetHistoryData();
        GetSpeed();
        GetAccuracy();
        LoadPlayerHistory(currHistoryPage);    
        
    }

    public void GetHistoryData()
    {
        http = new HttpManager();
        var url = "http://172.21.148.165/get_SinglePlayerRoundData";
        var responseStr = http.Post(url, "");
        Debug.Log(responseStr);
        playerRoundData = JsonConvert.DeserializeObject<List<SinglePlayerRoundData>>(responseStr);        
    }

    public void LoadPlayerHistory(int pageNo)
    {
        int count = 0;
        for (int i = 0; i < playerRoundData.Count; i++)
        {
            if (playerRoundData[i].studentId == username){
                
                if (count<(pageNo+1)*MaxRows && count>=(pageNo*MaxRows))
                {
                    var row = Instantiate(rowUi, transform).GetComponent<HistoryRowUi>();
                    row.gameObject.name = "Row" + (count + 1).ToString();
                    //row.date.text = (count + 1).ToString();
                    row.date.text = "2"+count.ToString()+"/3/22";
                    row.mode.text = playerRoundData[i].sldcWorld;
                    row.topic.text = playerRoundData[i].specificSection;
                    row.score.text = playerRoundData[i].finalScore.ToString();
                }
                count += 1;
            }
        }
    }

    public void NextPage()
    {
        currHistoryPage = 1;
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        LoadPlayerHistory(currHistoryPage);
    }

    public void PrevPage()
    {
        currHistoryPage -= 1;
        if (currHistoryPage<0)
        {
            currHistoryPage = 0;
        }
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        LoadPlayerHistory(currHistoryPage);
    }

    public void GetSpeed()
    {
        float totalNoQuestionsAns = 0;
        float totalTiming = 0;
        float avgSpeed;
        for (int i = 0; i < playerRoundData.Count; i++)
        {
            if (playerRoundData[i].studentId == username)
            {
                for (int j=0; j<playerRoundData[i].statList.Count; j++)
                {
                    if (playerRoundData[i].statList[j].timing>0){
                        totalNoQuestionsAns += 1;
                    }
                }
                //totalNoQuestionsAns += playerRoundData[i].statList.Count;
                totalTiming += playerRoundData[i].finalScore;
            }
        }
        avgSpeed = totalTiming/totalNoQuestionsAns;
        SpeedText.text = avgSpeed.ToString("#.#")+"%";
    }

    public void GetAccuracy()
    {
        float totalNoQuestionsAns = 0;
        float totalCorrect = 0;
        float avgAccuracy;
        for (int i = 0; i < playerRoundData.Count; i++)
        {
            if (playerRoundData[i].studentId == username)
            {
                totalNoQuestionsAns += playerRoundData[i].statList.Count;
                for (int j=0; j<playerRoundData[i].statList.Count; j++)
                {
                    if (playerRoundData[i].statList[j].timing>0)
                    {
                        Debug.Log(playerRoundData[i].statList[j].timing);
                        totalCorrect += 1;
                    }
                }
            }
        }
        avgAccuracy = (totalCorrect/totalNoQuestionsAns)*100;
        AccuracyText.text = avgAccuracy.ToString("#.#")+"%";
    }

}
