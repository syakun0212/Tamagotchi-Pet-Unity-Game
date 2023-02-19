using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;

public class StudentWelcomeManager : MonoBehaviour
{
    public Text usernameLabel;
    public string username;
    public Student student;
    private HttpManager http;
    private int displayPetsIndex;
    public Image petImage;
    public Sprite[] petSprites;
    public Text petFoodAmt;
    public Text petWaterAmt;
    private DataManager dataController;


    void Start()
    {
        dataController = FindObjectOfType<DataManager>();
        username = dataController.username;
        UpdateStudentUsername(username);
        displayPetsIndex = 0;
        GetStudentData();
        UpdatePetsFoodAndWater();
        UpdatePetDisplay();
    }

    public void GetStudentData()
    {
        http = new HttpManager();
        var url = "http://172.21.148.165/get_userData?username=" + username; // add query parameter using username?
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

    public void UpdatePetsFoodAndWater()
    {
        var lastLoginDay = DateTime.Parse(student.lastLoginDay);
        int daysDiff = (int)Math.Round((DateTime.Now - lastLoginDay).TotalDays);
        Debug.Log(daysDiff);
        foreach (var pet in student.petsUnlocked)
        {
            pet.petCurrentFood -= daysDiff * 4;
            pet.petCurrentWater -= daysDiff * 4;
            if (pet.petCurrentFood < 0)
            {
                pet.petCurrentFood = 0;
            }
            if (pet.petCurrentWater < 0)
            {
                pet.petCurrentWater = 0;
            }
        }

        student.lastLoginDay = DateTime.Now.ToString();
        UpdateStudentData();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SingleMultiPlayerSelectionUI");
    }

    public void UpdateStudentUsername(string s)
    {
        var label = "hello, " + s + "!";
        usernameLabel.text = label;
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
        var petFood = student.petsUnlocked[displayPetsIndex].petCurrentFood * 10;
        var petFoodPercent = petFood.ToString();
        petFoodAmt.text = petFoodPercent + "%";
        var petWater = student.petsUnlocked[displayPetsIndex].petCurrentWater * 10;
        var petWaterPercent = petWater.ToString();
        petWaterAmt.text = petWaterPercent + "%";
    }

}
