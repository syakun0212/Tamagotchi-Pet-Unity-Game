using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class SinglePlayerGameManager : MonoBehaviour
{
    private DataManager dataController;
    private SinglePlayerRoundData singlePlayerInstance;
    private List<Question> questionPool;
    private Question currentQuestion;

    private bool skillLeft;
    private string skillType, studentId, roundId; // "add 5 seconds", "recover 1 life"
    private int questionIndex, playerScore, playerLife, totalNumberOfQuestions, totalCorrect;
    float currentTime = 0f;
    float startingTime = 30f;
    private int timeBetweenQuestion = 2000;
    [SerializeField]
    private Text quesText, ansOneText, ansTwoText, ansThreeText, ansFourText, scoreText, lifeText, skillLeftText, skillExplainText;
    [SerializeField]
    public Text countdownText;
    public Button ansOne, ansTwo, ansThree, ansFour, useSkill;
    public int difficulty;

    private HttpManager http;
    public string username;
    public Student student;
    public int petSelectedIndex;
    public Pet petSelected;
    public Image petImage;
    public Sprite[] petSprites;
    public bool gameMadeHarder = false;

    public Image backgroundImg;

    void Start()
    {

        dataController = FindObjectOfType<DataManager>();
        username = dataController.username;
        petSelectedIndex = dataController.selectedPetIndex;
        singlePlayerInstance = dataController.GetSinglePlayerInstance();
        singlePlayerInstance.statList = new List<Stat>();

        GetPetChosen();
        UpdatePetDisplay();

        if (singlePlayerInstance.difficultyLevel == "easy")
        {
            difficulty = 1;
        }
        else if (singlePlayerInstance.difficultyLevel == "medium")
        {
            difficulty = 2;
        }
        else if (singlePlayerInstance.difficultyLevel == "hard")
        {
            difficulty = 3;
        }

        questionPool = singlePlayerInstance.questionList;
        playerScore = 0;
        playerLife = 3;

        // singlePlayerInstance.characterUsed = somepet; // get from server/object


        roundId = dataController.generateUID();
        singlePlayerInstance.roundId = roundId;
        singlePlayerInstance.studentId = username;

        skillLeft = true;
        skillLeftText.text = "1";
        skillExplainText.text = skillType;
        questionIndex = 0;
        totalNumberOfQuestions = 0;
        totalCorrect = 0;
        useSkill.interactable = true;
        SetCurrentQuestion();
    }
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");
        if (currentTime < 10f)
        {
            countdownText.color = Color.red;
        }
        if (currentTime <= 0)
        {
            currentTime = 0;
            loseLife();
            ManageNext();
        }
    }
    public static List<t> GetRandomElements<t>(IEnumerable<t> list, int elementsCount)
    {
        return list.OrderBy(x => Guid.NewGuid()).Take(elementsCount).ToList();
    }
    public void GetPetChosen()
    {
        http = new HttpManager();
        var url = "http://172.21.148.165/get_userData?username=" + username; // add query parameter using username?
        var responseStr = http.Post(url, "");
        student = JsonConvert.DeserializeObject<Student>(responseStr);
        petSelected = student.petsUnlocked[petSelectedIndex];
        singlePlayerInstance.characterUsed = petSelected;
    }

    public void UpdatePetDisplay()
    {
        var petSkinId = petSelected.petSkinId;
        petImage.sprite = petSprites[petSelectedIndex * 3 + petSkinId];
        skillType = petSelected.petPowerup;
        skillExplainText.text = skillType;
    }

    public void whenSkillButtonPressed()
    {
        if (skillLeft == true)
        {
            skillLeft = false;
            skillLeftText.text = "0";
            if (skillType == "Add 5 Seconds")
            {
                currentTime = currentTime + 5f;
            }
            else if (skillType == "Add 1 Life")
            {
                playerLife = playerLife + 1;
                lifeText.text = playerLife.ToString();
            }
            useSkill.interactable = false;
        }
    }
    // food = accuracy * level chosen
    // water = speed * level chosen
    void determineFood()
    {
        float percentageAccuracy = totalCorrect / totalNumberOfQuestions;
        if (percentageAccuracy >= 0.9f)
        {
            singlePlayerInstance.rewardedFood = 10 * difficulty;
        }
        else if (percentageAccuracy < 0.9f && percentageAccuracy >= 0.8f)
        {
            singlePlayerInstance.rewardedFood = 9 * difficulty;
        }
        else if (percentageAccuracy < 0.8f && percentageAccuracy >= 0.7f)
        {
            singlePlayerInstance.rewardedFood = 8 * difficulty;
        }
        else if (percentageAccuracy < 0.7f && percentageAccuracy >= 0.6f)
        {
            singlePlayerInstance.rewardedFood = 7 * difficulty;
        }
        else if (percentageAccuracy < 0.6f && percentageAccuracy >= 0.5f)
        {
            singlePlayerInstance.rewardedFood = 6 * difficulty;
        }
        else if (percentageAccuracy < 0.5f && percentageAccuracy >= 0.4f)
        {
            singlePlayerInstance.rewardedFood = 5 * difficulty;
        }
        else if (percentageAccuracy < 0.4f && percentageAccuracy >= 0.3f)
        {
            singlePlayerInstance.rewardedFood = 4 * difficulty;
        }
        else if (percentageAccuracy < 0.3f && percentageAccuracy >= 0.2f)
        {
            singlePlayerInstance.rewardedFood = 3 * difficulty;
        }
        else if (percentageAccuracy < 0.2f && percentageAccuracy >= 0.1f)
        {
            singlePlayerInstance.rewardedFood = 2 * difficulty;
        }
        else if (percentageAccuracy < 0.1f && percentageAccuracy >= 0f)
        {
            singlePlayerInstance.rewardedFood = 1 * difficulty;
        }
        else
        {
            singlePlayerInstance.rewardedFood = 0;
        }
    }
    void determineWater()
    {
        float baseLine = totalNumberOfQuestions * startingTime;
        float percentageSpeed = playerScore / baseLine;
        if (percentageSpeed >= 0.9f)
        {
            singlePlayerInstance.rewardedWater = 10 * difficulty;
        }
        else if (percentageSpeed < 0.9f && percentageSpeed >= 0.8f)
        {
            singlePlayerInstance.rewardedWater = 9 * difficulty;
        }
        else if (percentageSpeed < 0.8f && percentageSpeed >= 0.7f)
        {
            singlePlayerInstance.rewardedWater = 8 * difficulty;
        }
        else if (percentageSpeed < 0.7f && percentageSpeed >= 0.6f)
        {
            singlePlayerInstance.rewardedWater = 7 * difficulty;
        }
        else if (percentageSpeed < 0.6f && percentageSpeed >= 0.5f)
        {
            singlePlayerInstance.rewardedWater = 6 * difficulty;
        }
        else if (percentageSpeed < 0.5f && percentageSpeed >= 0.4f)
        {
            singlePlayerInstance.rewardedWater = 5 * difficulty;
        }
        else if (percentageSpeed < 0.4f && percentageSpeed >= 0.3f)
        {
            singlePlayerInstance.rewardedWater = 4 * difficulty;
        }
        else if (percentageSpeed < 0.3f && percentageSpeed >= 0.2f)
        {
            singlePlayerInstance.rewardedWater = 3 * difficulty;
        }
        else if (percentageSpeed < 0.2f && percentageSpeed >= 0.1f)
        {
            singlePlayerInstance.rewardedWater = 2 * difficulty;
        }
        else if (percentageSpeed < 0.1f && percentageSpeed >= 0f)
        {
            singlePlayerInstance.rewardedWater = 1 * difficulty;
        }
        else
        {
            singlePlayerInstance.rewardedWater = 0;
        }
    }
    void SetCurrentQuestion()
    {
        totalNumberOfQuestions = totalNumberOfQuestions + 1;
        currentTime = startingTime;
        countdownText.color = Color.black;
        scoreText.text = playerScore.ToString();
        lifeText.text = playerLife.ToString();
        currentQuestion = questionPool[questionIndex];

        ansOneText.color = Color.black;
        ansTwoText.color = Color.black;
        ansThreeText.color = Color.black;
        ansFourText.color = Color.black;

        quesText.text = currentQuestion.questionText;
        ansOneText.text = currentQuestion.answersText[0];
        ansTwoText.text = currentQuestion.answersText[1];
        ansThreeText.text = currentQuestion.answersText[2];
        ansFourText.text = currentQuestion.answersText[3];
        Debug.Log(totalNumberOfQuestions);
    }

    void makeQuestionsHarderInRealTime()
    {
        if (!gameMadeHarder)
        {
            var level = PlayerPrefs.GetString("levelSelected", "Nil");
            if (level == "easy" || level == "medium")
            {
                int numberOfQuestionsToQuery = 5;
                var url = "temp"; // temp
                if (level == "easy")
                {
                    url = "http://172.21.148.165/get_question_filtered_optional?world=REQUIREMENT&section=1&difficultyStandard=MEDIUM";
                }
                else if (level == "medium")
                {
                    url = "http://172.21.148.165/get_question_filtered_optional?world=REQUIREMENT&section=1&difficultyStandard=HARD";
                }
                var questionList = GetRandomElements(http.Get<List<Question>>(url), numberOfQuestionsToQuery);
                for (int f = 0; f < 5; f++) 
                {
                    questionPool.Add(questionList[f]);
                }
                Debug.Log("Hard questions spawned!");
                gameMadeHarder = true;
                backgroundImg.color = new Color32(255,0,0,100);
                petImage.color = new Color32(255,0,0,100);
            }
        }
    }
    // gen own stat id
    public async void addScore()
    {
        playerScore = playerScore + (int)System.Math.Round(currentTime);
        scoreText.text = playerScore.ToString();
        totalCorrect = totalCorrect + 1;
        var newStat = new Stat(dataController.generateUID(), roundId, currentQuestion.questionId, username, (int)System.Math.Round(currentTime), true, playerLife, skillLeft);
        singlePlayerInstance.statList.Add(newStat);
        if (playerScore >= 15*4)
        {
            makeQuestionsHarderInRealTime();
        }
    }
    public void loseLife()
    {
        playerLife = playerLife - 1;
        lifeText.text = playerLife.ToString();
        var newStat = new Stat(dataController.generateUID(), roundId, currentQuestion.questionId, username, 0, false, playerLife, skillLeft);
        singlePlayerInstance.statList.Add(newStat);
        if (playerLife <= 0)
        {
            lifeText.color = Color.red;
            EndRound();
        }
    }
    public async void ManageNext()
    {
        allDisable();
        await Task.Delay(timeBetweenQuestion);
        if (questionPool.Count > questionIndex + 1)
        {
            questionIndex++;
            SetCurrentQuestion();
        }
        else
        {
            EndRound();
        }
        allEnable();
    }
    public async void EndRound()
    {
        singlePlayerInstance.finalScore = playerScore;
        determineFood();
        determineWater();
        SceneManager.LoadScene("SinglePlayerGameCompletionUI");
    }
    public void UserSelectOne()
    {
        if (currentQuestion.answerIndex == 0)
        {
            ansOneText.color = Color.green;
            addScore();
        }
        else
        {
            ansOneText.color = Color.red;
            loseLife();
        }
        ManageNext();
    }
    public void UserSelectTwo()
    {
        if (currentQuestion.answerIndex == 1)
        {
            ansTwoText.color = Color.green;
            addScore();
        }
        else
        {
            ansTwoText.color = Color.red;
            loseLife();
        }
        ManageNext();
    }
    public void UserSelectThree()
    {
        if (currentQuestion.answerIndex == 2)
        {
            ansThreeText.color = Color.green;
            addScore();
        }
        else
        {
            ansThreeText.color = Color.red;
            loseLife();
        }
        ManageNext();
    }
    public void UserSelectFour()
    {
        if (currentQuestion.answerIndex == 3)
        {
            ansFourText.color = Color.green;
            addScore();
        }
        else
        {
            ansFourText.color = Color.red;
            loseLife();
        }
        ManageNext();
    }
    public void allDisable()
    {
        ansOne.interactable = false;
        ansTwo.interactable = false;
        ansThree.interactable = false;
        ansFour.interactable = false;
    }
    public void allEnable()
    {
        ansOne.interactable = true;
        ansTwo.interactable = true;
        ansThree.interactable = true;
        ansFour.interactable = true;
    }
}