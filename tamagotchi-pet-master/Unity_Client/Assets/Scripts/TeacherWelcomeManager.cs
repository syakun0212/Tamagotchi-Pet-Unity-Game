using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeacherWelcomeManager : MonoBehaviour
{
    public Text usernameLabel;
    void Start()
    {
        UpdateTeacherUsername();
    }
    public void UpdateTeacherUsername()
    {
        var username = PlayerPrefs.GetString("teacherUsername");
        var label = "hello, " + username + "!";
        usernameLabel.text = label;
    }

    public void PostAssignment(){
        SceneManager.LoadScene("PostAssignment-P1");
    }
}
