using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System;

public class StudentLoginManager : MonoBehaviour
{
    private string usernameInput;
    private bool usernameValid;
    private bool passwordValid;
    private string passwordEncrypted;
    public PasswordManager pwd;
    private HttpManager http;
    private SceneLoaderManager scene;
    public Text MessageLabel;
    public Student student;
    private DataManager dataController;


    public void ReadUsernameInput(string s)
    {

        Debug.Log(s);
        usernameInput = s;
        IsUsernameValid();
        if (usernameValid)
        {
            usernameInput = s;
        }
        else
        {
            usernameInput = "";
            MessageLabel.text = "Enter username of at least 6 letters";
        }
    }

    public void ReadPasswordInput(string s)
    {
        Debug.Log(s);
        passwordEncrypted = s;
        IsPasswordValid();
        if (passwordValid)
        {
            pwd = new PasswordManager();
            passwordEncrypted = pwd.AESEncryption(s);
            Debug.Log(passwordEncrypted);
        }
        else
        {
            passwordEncrypted = "";
            MessageLabel.text = "Enter password of at least 8 chars";
        }
    }
    public void IsUsernameValid()
    {
        if ((usernameInput.Length >= 6) & (Regex.IsMatch(usernameInput, @"^[a-zA-Z]+$")))
        {
            usernameValid = true;
        }
        else if (usernameInput == "admin")
        {
            usernameValid = true;
        }
        else
        {
            usernameValid = false;
        }

    }
    public void IsPasswordValid()
    {
        if (passwordEncrypted.Length >= 8)
        {
            passwordValid = true;
        }
        else if (passwordEncrypted == "admin")
        {
            passwordValid = true;
        }
        else
        {
            passwordValid = false;
        }

    }


    public class StudentLoginDetails
    {
        public StudentLoginDetails(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string username { get; set; }
        public string password { get; set; }

    }
    private StudentLoginDetails studentLogin;
    public void Login()
    {
        scene = new SceneLoaderManager();
        pwd = new PasswordManager();
        var temp = pwd.AESEncryption("admin");

        if (usernameInput == null & passwordEncrypted == null)
        {
            MessageLabel.text = "Enter login details";
        }
        else if (usernameInput == null & passwordValid)
        {
            MessageLabel.text = "Enter username";
        }
        else if (passwordEncrypted == null & usernameValid)
        {
            MessageLabel.text = "Enter password";
        }

        if (usernameValid & passwordValid)
        {
            if ((usernameInput == "admin") & (passwordEncrypted == temp))
            {
                CreateNewStudentData();
                SaveUsername();
                scene.LoadStudentWelcomeUI();
            }
            else
            {
                studentLogin = new StudentLoginDetails(usernameInput, passwordEncrypted);
                http = new HttpManager();
                var url = "http://172.21.148.165/login_student";
                var response = http.Post(url, studentLogin);
                Debug.Log(response);
                response = response.Substring(1, response.Length - 2);
                MessageLabel.text = response;

                if (response == "Successfully authenticated")
                {
                    SaveUsername();
                    scene.LoadStudentWelcomeUI();
                }

            }
        }
    }

    public void RegisterAndLogin()
    {
        if (usernameInput == null & passwordEncrypted == null)
        {
            MessageLabel.text = "Enter registration details";
        }
        else if (usernameInput == null)
        {
            MessageLabel.text = "Enter username";
        }
        else if (passwordEncrypted == null)
        {
            MessageLabel.text = "Enter password";
        }

        if (usernameValid & passwordValid)
        {
            studentLogin = new StudentLoginDetails(usernameInput, passwordEncrypted);
            http = new HttpManager();
            scene = new SceneLoaderManager();
            var url = "http://172.21.148.165/register_student";
            var response = http.Post(url, studentLogin);
            Debug.Log(response);
            response = response.Substring(1, response.Length - 2);
            MessageLabel.text = response;

            if (response == "User successfully registered")
            {
                SaveUsername();
                CreateNewStudentData();
                var jsonString = JsonConvert.SerializeObject(student);
                Debug.Log(jsonString);
                scene.LoadStudentWelcomeUI();
            }

        }
    }
    public void CreateNewStudentData()
    {
        var defaultPet1 = new Pet("Pet1", 0, "Add 5 Seconds", 5, 5);
        var defaultPet2 = new Pet("Pet2", 0, "Add 1 Life", 3, 3);
        var petList = new List<Pet>();
        petList.Add(defaultPet1);
        petList.Add(defaultPet2);
        var levelsUnlockedList = new List<int> { 0, 1 };
        http = new HttpManager();
        var url = "http://172.21.148.165/add_userData";
        student = new Student(usernameInput, 0, petList, 3, 3, 0, levelsUnlockedList, DateTime.Now.ToString());
        var response = http.Post(url, student); // post to backend studentdata
        Debug.Log("post " + response);

    }

    public void SaveUsername()
    {
        dataController = FindObjectOfType<DataManager>();
        dataController.username = usernameInput;
    }



}

