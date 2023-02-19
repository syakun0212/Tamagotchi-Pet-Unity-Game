using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using TMPro;
using System;
using System.Threading;

public class PostAssignmentManager2 : MonoBehaviour
{
    private DataManager dataController;
    private static  string assignmentTitle;
    private static  string assignmentDescription;
    private static int assignmentMark;
    private static string assignmentTopic;
    private static  int dueYear;
    private static  int dueMonth;
    private static  int dueDay;
    private static  int dueHour;
   [SerializeField]
    public Text successMsg;
    private HttpManager http;
    private string shareMsg;

    public static Assignment AssignmentInstance;
    public static string ID="00000";
    public static PostAssignmentManager1 assignmentScript1;
    public static List<Question> assignmentQnListToAdd;
    
    void Start()
    {   
        successMsg.gameObject.SetActive(false);
        dataController = FindObjectOfType<DataManager>();
        AssignmentInstance = dataController.GetAssignment();
        AssignmentInstance.questionList = PostAssignmentManager1.assignmentQnList;
    }
    public void postAssignment(){
        //post the assignmetn details to the backend database
        Debug.Log("======checking assignmetn List ======");
        Debug.Log("Posting assignment to database now");
        http = new HttpManager();
        var url = "http://172.21.148.165/add_Assignment";
        var response = http.Post(url, AssignmentInstance);
        Debug.Log("Assignment Posted Successfully");
    }

    public void postButton(){
        postAssignment();
        Debug.Log("Posting done");
        successMsg.gameObject.SetActive(true);
        successMsg.text = "Assignment Code is: " + AssignmentInstance.assignmentId;
        Application.OpenURL("https://t.me/share/url?url=google.com&text=" + successMsg.text);
        // links to google as we did not upload our app to the google play store
    }

    
    public void FacebookButtonClick()
    {
        shareMsg = "New Assignment Posted: " + AssignmentInstance.assignmentId;
        // links to google, as our app is not on the google play store for download
        
        string facebookShare = "https://www.facebook.com/sharer/sharer.php?u=google.com&quote=" + shareMsg;
        // + Uri.EscapeUriString(shareMsg);
        Application.OpenURL(facebookShare);
        
        // for android
        // StartCoroutine(DataManager.TakeScreenshotAndShare(shareMsg));
    }
// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void readNameInput(string title){
        assignmentTitle = title;
        Debug.Log(title);
        AssignmentInstance.assignmentName=assignmentTitle;
    }

    public void readDescriptionInput(string description){
        assignmentDescription = description;
        Debug.Log(description);
        AssignmentInstance.assignmentDescriptions=assignmentDescription;
    }

    public void readTotalMarkInput(string totalMark){
        assignmentMark = Int32.Parse(totalMark);
        Debug.Log(totalMark);
        AssignmentInstance.totalMark=assignmentMark;
    }
// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void handleTopicDropdown(int val){

        if (val == 0){
            assignmentTopic = "";
        }
        if (val==1){
            assignmentTopic = "REQUIREMENT";
        }
        if (val==3){
            assignmentTopic = "DESIGN";
            Debug.Log("World 3: " + assignmentTopic);
        }
        if (val==3){
            assignmentTopic = "IMPLEMENTATION";
        }
        Debug.Log("topic " + assignmentTopic + " is selected.");
        AssignmentInstance.assignmentTopic = assignmentTopic;
    }
    public void handleDueYearDropDown(int val){
        if (val == 0){
            dueYear = 2021;
        }
        if (val==1){
            dueYear = 2021;
        }
        if (val==3){
            dueYear = 2022;
        }
        if (val==3){
            dueYear = 2023;
        }
        Debug.Log(dueYear);
    }

    

    public void handleDueMonthDropdown(int val){
        dueMonth = val+1;
    }
    public void handleDueDayDropDown(int val){
        dueDay = val+1;
    }
    public void handleDueTimeDropdown(int val){
        dueHour = val;
    }
    public void backButton(){
        SceneManager.LoadScene("PostAssignment-P1");
    }
    }
