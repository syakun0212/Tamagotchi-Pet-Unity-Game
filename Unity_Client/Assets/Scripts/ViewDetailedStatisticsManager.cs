using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class ViewDetailedStatisticsManager : MonoBehaviour
{
    public string username;

     private HttpManager http;

    private SceneLoaderManager scene;

    // private List<StudentsScoreList> studentList;

    private List<Question> questionsStat;

    private List<Stat> stats;

    private List<Question> questionList;

    private List<SinglePlayerRoundData> rounddata;

    private List<MultiPlayerRoundData> multiplayerRoundData;

    private DataManager dataController;

    private int currStatsPage = 0;

    private int currTopicsIndex = 0;

    private int currQuestionsIndex = 0;

    private int currModeIndex = 0;

    public StudentsRowUI studentrowUI;

    public QuestionsRowUi questionsrowUI;

    public TopicsRowUi topicsrowUI;

    float questionTiming = 0;

    int questionTotal = 0;

    float topicsTiming = 0;

    int topicsTotal = 0;

    int totalSingleScore;

    private List<string> topicList = new List<string>();

    int totalMultiScore;

    int MaxStats = 8; // Number of scores to be shown on one page

    // Start is called before the first frame update
    void Start()
    {
        GetQuestionsStatsData();
        // LoadQuestionsStatistics(0);
        GetTopicsStatsData();
        // LoadTopicsStatistics(0);
        GetModeStatsData();
        // LoadModeStatistics(0);
        LoadPage(currTopicsIndex, currQuestionsIndex, currModeIndex);
    }

    public void GetTopicsStatsData()
    {
        http = new HttpManager();
        var url = "http://172.21.148.165/get_SinglePlayerRoundData"; // change 
        var responseStr = http.Post(url, ""); 
        rounddata = JsonConvert.DeserializeObject<List<SinglePlayerRoundData>>(responseStr);
        Debug.Log(rounddata.Count);

        // dataController = FindObjectOfType<DataManager>();
        // username = dataController.username;
        // Debug.Log(username);
    }

    public void LoadTopicsStatistics(int currTopicsIndex)
    {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        topicList.Clear();
        topicsTiming = 0;
        if (currTopicsIndex == 0)
        {
            for (int i = 0; i < rounddata.Count; i++)
            {  
                if ((rounddata[i] != null))
                {
                    foreach (string item in topicList)
                    Debug.Log(rounddata[i].questionList[0].worldTopic);
                    {
                        if ((topicList.Contains(rounddata[i].questionList[0].worldTopic)))
                        {
                            continue;
                        }
                        else
                        {
                            topicList.Add(rounddata[i].questionList[0].worldTopic);
                            var row = Instantiate(topicsrowUI, transform).GetComponent<TopicsRowUi>();
                            row.gameObject.name = "Row" + (i+1).ToString();
                            row.questions.text = rounddata[i].questionList[0].worldTopic;
                            for(int j = 0; j < rounddata.Count; j++)
                            {
                                for (int k = 0; k < rounddata[j].statList.Count; k++)
                                {
                                    topicsTotal += 1;
                                    if (rounddata[j].statList[k].timing != 0)
                                    topicsTiming = topicsTiming + 1;
                                    // questionTiming = (questionTiming + 1)/questionTotal;
                                }
                            }
                            row.timing.text = topicsTiming.ToString();
                            topicsTiming = 0;
                        }
                    } 
                }
            }
        }
        else
        {
            if (topicList.Count > 8)
            {
                for (int i = 8; i < Mathf.Min(2*MaxStats, stats.Count); i++)
            {
                    if ((rounddata[i] != null ))
                    {
                        var row = Instantiate(topicsrowUI, transform).GetComponent<TopicsRowUi>();
                        row.gameObject.name = "Row" + (i+1).ToString();
                        row.questions.text = rounddata[i].questionList[1].worldTopic;
                        for (int k = 0; k < rounddata[i].statList.Count; k++)
                        {
                            if (rounddata[i].questionList[1].worldTopic == rounddata[i].questionList[k].worldTopic)
                                topicsTotal += 1;
                                if (rounddata [k].statList[k].timing != 0)
                                topicsTiming = topicsTiming + 1;
                                // questionTiming = (questionTiming + 1)/questionTotal;
                        }
                        // row.rank.text = (i + 1).ToString();
                        // row.questions.text = stats[i].questionId;
                        row.timing.text = topicsTiming.ToString(); 
                        topicsTiming = 0;
                    }
                }
            }
            else
            {
                for (int i = 1; i < rounddata.Count; i++)
                {  
                    if ((rounddata[i] != null))
                    {
                        foreach (string item in topicList)
                        // Debug.Log(rounddata[i].questionList[0].worldTopic);
                        {
                            if ((topicList.Contains(rounddata[i].questionList[0].worldTopic)))
                            {
                                continue;
                            }
                            else
                            {
                                topicList.Add(rounddata[i].questionList[0].worldTopic);
                                var row = Instantiate(topicsrowUI, transform).GetComponent<TopicsRowUi>();
                                row.gameObject.name = "Row" + (i+1).ToString();
                                row.questions.text = rounddata[i].questionList[0].worldTopic;
                                for(int j = 0; j < rounddata.Count; j++)
                                {
                                    for (int k = 0; k < rounddata[j].statList.Count; k++)
                                    {
                                        topicsTotal += 1;
                                        if (rounddata[j].statList[k].timing != 0)
                                        topicsTiming = topicsTiming + 1;
                                        // questionTiming = (questionTiming + 1)/questionTotal;
                                    }
                                }
                                row.timing.text = topicsTiming.ToString();
                                topicsTiming = 0;
                            }
                        } 
                    }
                }
            }
        }
    }

    public void GetQuestionsStatsData()
    {
        http = new HttpManager();
        var url = "http://172.21.148.165/get_stats"; // change 
        var responseStr = http.Post(url, ""); 
        // Debug.Log(responseStr);
        // currentRoundData = dataController.GetSinglePlayerInstance();
        // questionsStat = JsonConvert.DeserializeObject<List<QuestionsList>>(responseStr);
        stats = JsonConvert.DeserializeObject<List<Stat>>(responseStr);
        // questionsStat = questionsStat.ToList();
        // Debug.Log(questionsStat[1].statList);
        // Debug.Log(stats.Count);
        // questionsList = questionsList.OrderByDescending(o=>o.score).ToList();

        http = new HttpManager();
        var url_question = "http://172.21.148.165/get_question"; // change 
        var responseStrQuestion = http.Post(url_question, ""); 
        // Debug.Log(responseStr);
        questionList = JsonConvert.DeserializeObject<List<Question>>(responseStrQuestion);
    }

    public void LoadQuestionsStatistics(int currQuestionsIndex)
    {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        questionTiming = 0;
        if (currQuestionsIndex == 0)
        {
            for (int i = 0; i < Mathf.Min(MaxStats, questionList.Count); i++)
            {
                var row = Instantiate(questionsrowUI, transform).GetComponent<QuestionsRowUi>();
                row.gameObject.name = "Row" + (i+1).ToString();
                row.questions.text = questionList[i].questionText;
                for (int j = 1; j < stats.Count; j++)
                {
                    if (stats[j].questionId == questionList[i].questionId)
                    {
                        questionTotal += 1;
                        if (stats[j].timing != 0)
                        questionTiming = questionTiming + 1;
                        // questionTiming = (questionTiming + 1)/questionTotal;
                    }
                }
                row.timing.text = questionTiming.ToString();
                questionTiming = 0;
            }
        }
        else
        {
            for (int i = 8; i < Mathf.Min(2*MaxStats, questionList.Count); i++)
            {
                var row = Instantiate(questionsrowUI, transform).GetComponent<QuestionsRowUi>();
                row.gameObject.name = "Row" + (i+1).ToString();
                row.questions.text = questionList[i].questionText;
                for (int j = 1; j < stats.Count; j++)
                {
                    if (stats[j].questionId == questionList[i].questionId)
                    {
                        questionTotal += 1;
                        if (stats[j].timing != 0)
                        questionTiming = questionTiming + 1;
                        // questionTiming = (questionTiming + 1)/questionTotal;
                    }
                }
                row.timing.text = questionTiming.ToString();
                questionTiming = 0;
            }
        }
    }

    public void GetModeStatsData()
    {
        http = new HttpManager();
        var url = "http://172.21.148.165/get_SinglePlayerRoundData"; // change 
        var responseStr = http.Post(url, ""); 
        // Debug.Log(responseStr);
        rounddata = JsonConvert.DeserializeObject<List<SinglePlayerRoundData>>(responseStr);
        // Debug.Log(rounddata[1].questionList[1].worldTopic);

        http = new HttpManager();
        var url_question = "http://172.21.148.165/get_MultiPlayerRoundData"; // change 
        var responseStrMultiPlayer = http.Post(url_question, ""); 
        // Debug.Log(responseStrMultiPlayer);
        multiplayerRoundData = JsonConvert.DeserializeObject<List<MultiPlayerRoundData>>(responseStrMultiPlayer);
        // Debug.Log(multiplayerRoundData);
        
    }

    public void LoadModeStatistics(int currModeIndex)
    {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        totalMultiScore = 0;
        if (currModeIndex == 0)
        {
            var row = Instantiate(questionsrowUI, transform).GetComponent<QuestionsRowUi>();
            row.gameObject.name = "Row" + (1).ToString();
            row.questions.text = "Single Player Mode"; 
            for (int i = 0; i < rounddata.Count; i++)
            {
                totalSingleScore += rounddata[i].finalScore;
            }
            row.timing.text = totalSingleScore.ToString(); 
            totalSingleScore = 0;

            row = Instantiate(questionsrowUI, transform).GetComponent<QuestionsRowUi>();
            row.gameObject.name = "Row" + (2).ToString();
            row.questions.text = "MultiPlayer Player Mode"; 
            for (int i = 0; i < multiplayerRoundData.Count; i++)
            {
                totalMultiScore += multiplayerRoundData[i].finalScore;
            }
            row.timing.text = totalMultiScore.ToString();
            totalMultiScore = 0;
        }
        else
        {
            var row = Instantiate(questionsrowUI, transform).GetComponent<QuestionsRowUi>();
            row.gameObject.name = "Row" + (1).ToString();
            row.questions.text = "Single Player Mode"; 
            for (int i = 0; i < rounddata.Count; i++)
            {
                totalSingleScore += rounddata[i].finalScore;
            }
            row.timing.text = totalSingleScore.ToString();
            totalSingleScore = 0; 

            row = Instantiate(questionsrowUI, transform).GetComponent<QuestionsRowUi>();
            row.gameObject.name = "Row" + (2).ToString();
            row.questions.text = "MultiPlayer Player Mode"; 
            for (int i = 0; i < multiplayerRoundData.Count; i++)
            {
                totalMultiScore += multiplayerRoundData[i].finalScore;
            }
            row.timing.text = totalMultiScore.ToString();
            totalMultiScore = 0;
        }
    }

    public void LoadPage(int currTopicsIndex, int currQuestionsIndex, int currModeIndex)
    {
        if (currStatsPage == 0)
        {
            LoadTopicsStatistics(currTopicsIndex);
        }
        else if (currStatsPage == 1)
        {
            LoadQuestionsStatistics(currQuestionsIndex);
        }
        else
        {
            LoadModeStatistics(currModeIndex);
        }
    }
    public void NextPage()
    {
        if (currStatsPage == 0)
        {
            currTopicsIndex = 1;
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
            LoadPage(currTopicsIndex, currQuestionsIndex, currModeIndex);
        } 
        else if (currStatsPage == 1)
        {
            currQuestionsIndex = 1;
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
            LoadPage(currTopicsIndex, currQuestionsIndex, currModeIndex);
        }
        else
        {
            currModeIndex = 1;
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
            LoadPage(currTopicsIndex, currQuestionsIndex, currModeIndex);
        }
    }

    public void PrevPage()
    {
        if (currStatsPage == 0)
        {
            currTopicsIndex = 0;
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
            LoadPage(currTopicsIndex, currQuestionsIndex, currModeIndex);
        } 
        else if (currStatsPage == 1)
        {
            currQuestionsIndex = 0;
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
            LoadPage(currTopicsIndex, currQuestionsIndex, currModeIndex);
        }
        else
        {
            currModeIndex = 0;
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
            LoadPage(currTopicsIndex, currQuestionsIndex, currModeIndex);
        }
    }

    public void LoadTopicsButton()
    {
        currStatsPage = 0;
        LoadTopicsStatistics(0);
    }

    public void LoadQuestionsButton()
    {
        currStatsPage = 1;
        LoadQuestionsStatistics(0);
    }

    public void LoadModeButton()
    {
        currStatsPage = 2;
        LoadModeStatistics(0);
    }

    public void BackButtonClick() {
        SceneManager.LoadScene("GameHistoryUI");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
