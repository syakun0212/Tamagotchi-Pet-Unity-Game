using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;
using static Question;
using static SinglePlayerRoundData;
using System.Linq;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public class CharacterSelectAndLoadingManager : MonoBehaviour
{
    public Student student;
    private HttpManager http;
    private int displayPetsIndex;
    public string username;
    public Image petImage;
    public Sprite[] petSprites;
    public Text petSkill;
    private DataManager dataController;

    private List<Question> questionListData;

    private SinglePlayerRoundData SinglePlayerInstance;
    private MultiPlayerRoundData MultiPlayerInstance;



    void Start()
    {
        dataController = FindObjectOfType<DataManager>();
        username = dataController.username;
        displayPetsIndex = 0;
        GetStudentData();
        UpdatePetDisplay();

        SinglePlayerInstance = dataController.GetSinglePlayerInstance();
        MultiPlayerInstance = dataController.GetMultiPlayerInstance();
    }

    public void GetStudentData()
    {
        http = new HttpManager();
        var url = "http://172.21.148.165/get_userData?username=" + username; // add query parameter using username?
        var responseStr = http.Post(url, "");
        student = JsonConvert.DeserializeObject<Student>(responseStr);
    }

    public void NextPet()
    {
        displayPetsIndex += 1;
        if (displayPetsIndex >= student.petsUnlocked.Count - 1)
        {
            displayPetsIndex = student.petsUnlocked.Count - 1;
        }
        UpdatePetDisplay();
    }

    public void PrevPet()
    {
        displayPetsIndex -= 1;
        if (displayPetsIndex <= 0)
        {
            displayPetsIndex = 0;
        }
        UpdatePetDisplay();
    }

    public void UpdatePetDisplay()
    {
        var petSkinId = student.petsUnlocked[displayPetsIndex].petSkinId;
        petImage.sprite = petSprites[displayPetsIndex * 3 + petSkinId];
        petSkill.text = student.petsUnlocked[displayPetsIndex].petPowerup;
        dataController.selectedPetIndex = displayPetsIndex;
    }

    public void SetSinglePlayerQuestions(string world, string section, string level)
    {
        var url = "temp"; // temp
        if (level == "easy")
        {
            url = "http://172.21.148.165/get_question_filtered_optional?world=REQUIREMENT&section=1&difficultyStandard=EASY";
        }
        else if (level == "medium")
        {
            url = "http://172.21.148.165/get_question_filtered_optional?world=REQUIREMENT&section=1&difficultyStandard=MEDIUM";
        }
        else
        {
            url = "http://172.21.148.165/get_question_filtered_optional?world=REQUIREMENT&section=1&difficultyStandard=HARD";
        }
        var questionList = http.Get<List<Question>>(url);
        SinglePlayerInstance.questionList = GetRandomElements(questionList, 5);
    }
    public static List<t> GetRandomElements<t>(IEnumerable<t> list, int elementsCount)
        {
            return list.OrderBy(x => Guid.NewGuid()).Take(elementsCount).ToList();
        }
    public async void SetMultiPlayerQuestions(string world, string section, string level)
    {
        // considering the world section level do the item below
        // need to double check the values in server, maybe need wipe out everything
        // done and checked, works well
        var url = "http://172.21.148.165/get_single_round_data_by?world="+world+"&section="+section+"&difficultyStandard="+level;
        var all_data_list = http.Get<List<SinglePlayerRoundData>>(url);
        var chosen_game = GetRandomElements(all_data_list, 1);

        // very anon
        MultiPlayerInstance.questionList = chosen_game[0].questionList;
        MultiPlayerInstance.opponentStatList = chosen_game[0].statList;

        // sample only, need to replace with backend stuffs above
        // var answersText = new List<string>();
        // answersText.Add("4");
        // answersText.Add("3");
        // answersText.Add("2");
        // answersText.Add("1");
        // var question1 = new Question("0", "2+2", 0, answersText, "requirements", "1", "easy");
        // var question2 = new Question("1", "3x1", 1, answersText, "requirements", "1", "easy");
        // var questionList = new List<Question>();
        // questionList.Add(question1);
        // questionList.Add(question2);
        // MultiPlayerInstance.questionList = questionList;
        // var stat1 = new Stat(dataController.generateUID(), dataController.generateUID(), "0", "meowmeow", 15, true, 2, true);
        // var stat2 = new Stat(dataController.generateUID(), dataController.generateUID(), "1", "meowmeow", 20, true, 2, true);
        // var opponentStatList = new List<Stat>();
        // opponentStatList.Add(stat1);
        // opponentStatList.Add(stat2);
        // MultiPlayerInstance.opponentStatList = opponentStatList;
    }



    public void StartGameButtonClicked()
    {
        // maybe can show loading animation
        string gameType = PlayerPrefs.GetString("gameTypeSelected", "Nil");
        string world = PlayerPrefs.GetString("worldSelected", "Nil");
        string section = PlayerPrefs.GetString("sectionSelected", "Nil");
        string level = PlayerPrefs.GetString("levelSelected", "Nil");

        if (gameType == "Single")
        {
            SinglePlayerInstance.sldcWorld = world;
            SinglePlayerInstance.specificSection = section;
            SinglePlayerInstance.difficultyLevel = level;

            SetSinglePlayerQuestions(world, section, level);
            SceneManager.LoadScene("SinglePlayerGameUI");
        }

        else if (gameType == "Multi")
        {
            MultiPlayerInstance.sldcWorld = world;
            MultiPlayerInstance.specificSection = section;
            MultiPlayerInstance.difficultyLevel = level;

            SetMultiPlayerQuestions(world, section, level);
            SceneManager.LoadScene("MultiPlayerGameUI");
        }

        else
        {
            Debug.Log("Error!");
        }
    }

    public void BackSelected() {
        SceneManager.LoadScene("SingleMultiPlayerSelectionUI");
    }

}
