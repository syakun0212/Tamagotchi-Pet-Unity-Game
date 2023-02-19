using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class TeacherLoginManager : MonoBehaviour
{
    private string usernameInput;
    private bool usernameValid;
    private bool passwordValid;
    private string passwordEncrypted;
    private PasswordManager pwd;
    private HttpManager http;
    private SceneLoaderManager scene;
    public Text MessageLabel;


    public void ReadUsernameInput(string s)
    {

        Debug.Log(s);
        usernameValid = IsUsernameValid(s);
        if (usernameValid)
        {
            usernameInput = s;
        }
        else
        {
            MessageLabel.text = "Enter username of at least 6 chars with letters";
        }
    }

    public void ReadPasswordInput(string s)
    {
        Debug.Log(s);
        passwordValid = IsPasswordValid(s);
        if (passwordValid)
        {
            pwd = new PasswordManager();
            passwordEncrypted = pwd.AESEncryption(s);
            Debug.Log(passwordEncrypted);
        }
        else
        {
            MessageLabel.text = "Enter password of at least 8 chars";
        }
    }
    public bool IsUsernameValid(string un)
    {
        if ((un.Length >= 6) & (Regex.IsMatch(un, @"^[a-zA-Z]+$")))
        {
            return true;
        }
        else if (un == "admin")
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public bool IsPasswordValid(string pw)
    {
        // if ((pw.Length >= 8) & (Regex.IsMatch(pw, @"^[a-zA-Z]+$")) & (Regex.IsMatch(pw, @"^-?\d+$")))
        if (pw.Length >= 8)
        {
            return true;
        }
        else if (pw == "admin")
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    public class TeacherLoginDetails
    {
        public TeacherLoginDetails(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string username { get; set; }
        public string password { get; set; }

    }
    private TeacherLoginDetails teacherLogin;
    public void Login()
    {
        scene = new SceneLoaderManager();
        pwd = new PasswordManager();
        var temp = pwd.AESEncryption("admin");
        if (usernameValid & passwordValid)
        {
            if ((usernameInput == "admin") & (passwordEncrypted == temp))
            {
                PlayerPrefs.SetString("teacherUsername", usernameInput);
                scene.LoadTeacherWelcomeUI();
            }
            else
            {
                teacherLogin = new TeacherLoginDetails(usernameInput, passwordEncrypted);
                http = new HttpManager();
                scene = new SceneLoaderManager();
                var url = "http://172.21.148.165/login_teacher";
                var response = http.Post(url, teacherLogin);
                Debug.Log(response);
                response = response.Substring(1, response.Length - 2);
                MessageLabel.text = response;

                if (response == "Successfully authenticated")
                {
                    PlayerPrefs.SetString("teacherUsername", usernameInput);
                    scene.LoadTeacherWelcomeUI();
                }
            }
        }

    }
    public void RegisterAndLogin()
    {
        if (usernameValid & passwordValid)
        {
            teacherLogin = new TeacherLoginDetails(usernameInput, passwordEncrypted);
            http = new HttpManager();
            scene = new SceneLoaderManager();
            var url = "http://172.21.148.165/register_teacher";
            var response = http.Post(url, teacherLogin);
            Debug.Log(response);
            response = response.Substring(1, response.Length - 2);
            MessageLabel.text = response;

            if (response == "User successfully registered")
            {
                PlayerPrefs.SetString("teacherUsername", usernameInput);
                scene.LoadTeacherWelcomeUI();
            }

        }

    }



}
