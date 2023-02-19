using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using TMPro;

public class TeeacherWelcomeManager1 : MonoBehaviour
{
    private string assignmentTitle;
    private string assignmentDescription;

    private string assignmentMark;
    private string assignmentTopic;
    private int dueYear;
    private int dueMonth;
    private int dueDay;
    private int dueHour;

    public Text successMsg;
    void Start()
    {
        
    }

    
    public void PostAssignment(){
        SceneManager.LoadScene("PostAssignment-P1");
    }
    
    

}
