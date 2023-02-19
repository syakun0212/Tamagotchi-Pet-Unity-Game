using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class PetChangeSkinManager : MonoBehaviour
{
    public string username;
    private int displayPetsIndex;
    public Student student;
    public Image[] petSkinChoices;
    public Sprite[] petSkinSprites;
    public Text header;
    private DataManager dataController;
    private HttpManager http;

    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataManager>();
        username = dataController.username;
        displayPetsIndex = PlayerPrefs.GetInt("currentPetsIndex", 0);
        GetStudentData();
        UpdateChangeSkinDisplay();

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
        http = new HttpManager();
        var url = "http://172.21.148.165/update_userData?username=" + username; // add query parameter using username
        var responseStr = http.Post(url, student);
        Debug.Log(responseStr);
    }

    public void NextPet()
    {
        displayPetsIndex += 1;
        if (displayPetsIndex >= student.petsUnlocked.Count - 1)
        {
            displayPetsIndex = student.petsUnlocked.Count - 1;
        }
        PlayerPrefs.SetInt("currentPetsIndex", displayPetsIndex);
        UpdateChangeSkinDisplay();


    }

    public void PrevPet()
    {
        displayPetsIndex -= 1;
        if (displayPetsIndex <= 0)
        {
            displayPetsIndex = 0;
        }
        PlayerPrefs.SetInt("currentPetsIndex", displayPetsIndex);
        UpdateChangeSkinDisplay();
    }
    public void UpdateChangeSkinDisplay()
    {
        header.text = "change skin for: " + student.petsUnlocked[displayPetsIndex].petName;
        petSkinChoices[0].sprite = petSkinSprites[displayPetsIndex * 3 + 0];
        petSkinChoices[1].sprite = petSkinSprites[displayPetsIndex * 3 + 1];
        petSkinChoices[2].sprite = petSkinSprites[displayPetsIndex * 3 + 2];
    }

    public void SkinOneSelected()
    {
        student.petsUnlocked[displayPetsIndex].petSkinId = 0;
        UpdateStudentData();

    }
    public void SkinTwoSelected()
    {
        student.petsUnlocked[displayPetsIndex].petSkinId = 1;
        UpdateStudentData();
    }
    public void SkinThreeSelected()
    {
        student.petsUnlocked[displayPetsIndex].petSkinId = 2;
        UpdateStudentData();
    }


}
