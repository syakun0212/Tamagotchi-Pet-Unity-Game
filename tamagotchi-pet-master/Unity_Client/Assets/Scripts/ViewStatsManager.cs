using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class ViewStatsManager : MonoBehaviour
{
    private HttpManager http;

    private SceneLoaderManager scene;

    private List<StudentsScoreList> studentList;

    private List<Question> questionsStat;

    private List<Stat> stats;

    private List<Question> questionList;

    private DataManager dataController;

    private int currStatsPage = 0;

    private int currStudentsIndex = 0;

    private int currQuestionsIndex = 0;

    public StudentsRowUI studentrowUI;

    public QuestionsRowUi questionsrowUI;

    int MaxStats = 10; // Number of scores to be shown on one page

    float questionTiming = 0;

    int questionTotal = 0;

    List<int> Questions;

    void Start() {
        //currLeaderboardIndex = 0;
        GetStudentsStatsData();
        GetQuestionsStatsData();
        LoadPage(currStudentsIndex, currQuestionsIndex);
        // LoadStudentsStatistics(0);
        // LoadQuestionsStatistics(0);
    }

    public class StudentsScoreList
    {
        public StudentsScoreList(string username, int score)
        {
            this.username = username;
            this.score = score;
        }

        public string username { get; set; }
        public int score { get; set; }

    }

    public void GetStudentsStatsData()
    {
        http = new HttpManager();
        var url = "http://172.21.148.165/get_Leaderboard"; // change 
        var responseStr = http.Post(url, ""); 
        Debug.Log(responseStr);
        studentList = JsonConvert.DeserializeObject<List<StudentsScoreList>>(responseStr);
        studentList = studentList.OrderByDescending(o=>o.score).ToList();
    }

    public void LoadStudentsStatistics(int currStudentsIndex)
    {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        if (currStudentsIndex == 0)
        {   
            for (int i = 0; i < Mathf.Min(MaxStats, studentList.Count); i++)
            {
                Debug.Log(studentList[i].username);
                Debug.Log(studentList[i].score);
                //if ((studentList[i] != null ) && (studentList[i].score != 0))
                if ((studentList[i] != null ))
                {
                    var row = Instantiate(studentrowUI, transform).GetComponent<StudentsRowUI>();
                    row.gameObject.name = "Row" + (i+1).ToString();
                    // row.rank.text = (i + 1).ToString();
                    row.name.text = studentList[i].username;
                    row.score.text = studentList[i].score.ToString(); 
                }
            }
         }
        else
        {
            for (int i = 10; i < Mathf.Min(2*MaxStats, studentList.Count); i++)
            {
                Debug.Log(studentList[i].username);
                Debug.Log(studentList[i].score);
                //if ((studentList[i] != null ) && (studentList[i].score != 0))
                if ((studentList[i] != null ))
                {
                    var row = Instantiate(studentrowUI, transform).GetComponent<StudentsRowUI>();
                    row.gameObject.name = "Row" + (i+1).ToString();
                    // row.rank.text = (i + 1).ToString();
                    row.name.text = studentList[i].username;
                    row.score.text = studentList[i].score.ToString(); 
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
        Debug.Log(responseStr);
        questionList = JsonConvert.DeserializeObject<List<Question>>(responseStrQuestion);
        // Debug.Log("questionList.Count");
        // Debug.Log(questionList.Count);
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
            for (int i = 10; i < Mathf.Min(2*MaxStats, questionList.Count); i++)
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

    public void LoadPage(int currStudentsIndex, int currQuestionsIndex)
    {
        if (currStatsPage == 0)
        {
            LoadStudentsStatistics(currStudentsIndex);
        }
        else
        {
            LoadQuestionsStatistics(currQuestionsIndex);
        }
    }
    public void NextPage()
    {
        if (currStatsPage == 0)
        {
            currStudentsIndex = 1;
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
            LoadPage(currStudentsIndex, currQuestionsIndex);
        } 
        else 
        {
            currQuestionsIndex = 1;
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
            LoadPage(currStudentsIndex, currQuestionsIndex);
        }
    }

    public void PrevPage()
    {
        if (currStatsPage == 0)
        {
            currStudentsIndex = 0;
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
            LoadPage(currStudentsIndex, currQuestionsIndex);
        } 
        else 
        {
            currQuestionsIndex = 0;
            foreach (Transform child in transform) {
                Destroy(child.gameObject);
            }
            LoadPage(currStudentsIndex, currQuestionsIndex);
        }
    }

    public void LoadStudentsButton()
    {
        currStatsPage = 0;
        LoadStudentsStatistics(0);
    }

    public void LoadQuestionsButton()
    {
        currStatsPage = 1;
        LoadQuestionsStatistics(0);
    }

    void Update()
    {
        
    }

    public void BackButtonClick() {
        SceneManager.LoadScene("TeacherWelcomeUI");
    }

    public void ReportButtonClick() {
        Application.OpenURL("http://172.21.148.165/get_Report");
    }

}