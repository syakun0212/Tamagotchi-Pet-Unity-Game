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

public class PostAssignmentManager1 : MonoBehaviour
{
    private HttpManager http;
    public GameObject panelObject;
    private SceneLoaderManager scene;
    private static string worldSelected = null;
    private static string sectionSelected = null;
    private static string difficultySelected = null;
    public Button addButton1;
    public Button addButton2;
    public Button addButton3;
    public Button removeButton1;
    public Button removeButton2;
    public Button removeButton3;
    public Button qnPrevPageButton;
    public Button qnNextPageButton;
    public Button assimtPrevPageButton;
    public Button assimtNextPageButton;
    public Text QnContent1;
    public Text QnContent2;
    public Text QnContent3;
    public Text QnId1;
    public Text QnId2;
    public Text QnId3;
    public Text AddedQnId1;
    public Text AddedQnId2;
    public Text AddedQnId3;
    public Text AddedQnContent1;
    public Text AddedQnContent2;
    public Text AddedQnContent3;
    int QnRows = 3;
    private int AssignmtRows = 3;
    private static int currQuestionIndex = 0;
    private static int currAssimtQnIndex = 0;   
    private static List<Question> questionList;
    private List<Student> studentList = new List<Student>();
    public static List<Question> assignmentQnList = new List<Question>();
    public Image popUpBox;
    public Text popUpText;

    private Dropdown worldDropdown;
    private Dropdown sectionDropdown;
    private Dropdown levelDropdown;



    void Start()
    {
        assignmentQnList = new List<Question>();
        Debug.Log("starting session...");
        SetButtons();
        SetTextBoxObjects();
        SetDropdown();
        // Debug.Log("Fetching question data now....");
        GetQuestionData();  
        Debug.Log("Data fetched successfully!");
        DisplayQuestionData(currQuestionIndex);
        DisplayAddedQuestionData(currAssimtQnIndex, assignmentQnList);
    }

    public void GetQuestionData(){
        http = new HttpManager();
        var url = "http://172.21.148.165/get_question";
        var responseStr = http.Post(url, ""); 
        Debug.Log(responseStr);
        questionList = JsonConvert.DeserializeObject<List<Question>>(responseStr);
        questionList = questionList.ToList();
        Debug.Log(questionList);
    }

    public void GetQuestionDataFiltered(){
        http = new HttpManager();
        var url = "";
        if (worldSelected!=null && sectionSelected!=null&difficultySelected!=null){
            url = "http://172.21.148.165/get_question_filtered_optional?world="+worldSelected+"&section="+sectionSelected+"&difficultyStandard="+difficultySelected;
        }
        else if (worldSelected == null & sectionSelected == null & difficultySelected!=null){
            url = "http://172.21.148.165/get_question_filtered_optional?difficultyStandard=" + difficultySelected;
        }
        else if (worldSelected == null & sectionSelected != null & difficultySelected==null){
            url = "http://172.21.148.165/get_question_filtered_optional?section=" + sectionSelected;
        }
        else if (worldSelected != null && sectionSelected==null & difficultySelected==null){
            url = "http://172.21.148.165/get_question_filtered_optional?world="+worldSelected;
        }
        else if (worldSelected==null & sectionSelected!=null&difficultySelected!=null){
            url = "http://172.21.148.165/get_question_filtered_optional?section="+sectionSelected+"&difficultyStandard="+difficultySelected;
        }
        else if (worldSelected!=null && sectionSelected!=null&difficultySelected==null){
            url = "http://172.21.148.165/get_question_filtered_optional?world="+worldSelected+"&section="+sectionSelected;
        }
        else if (worldSelected!=null && sectionSelected==null&difficultySelected!=null){
            url = "http://172.21.148.165/get_question_filtered_optional?world="+worldSelected+"&difficultyStandard="+difficultySelected;
        }
        else{
            url = "http://172.21.148.165/get_question";
        }
        
        var responseStr = http.Post(url, ""); 
        Debug.Log(responseStr);
        questionList = JsonConvert.DeserializeObject<List<Question>>(responseStr);
        questionList = questionList.ToList();
        Debug.Log(questionList);
    }

    public void LoadQuestionData(){
        Debug.Log("Loading Question");
        Debug.Log(questionList);
        Debug.Log(questionList[0].questionText);
        Debug.Log(questionList[0].questionId);
    }

    public void DisplayQuestionData(int currQuestionIndex){
        int idx = Math.Abs(currQuestionIndex);
        // Debug.Log("Displaying Question Data now. current displayed index is " + idx);        

        if (questionList.Count>idx){
        addButton1.enabled = true;
        // Debug.Log("get text");
        QnContent1.text = questionList[idx].questionText;
        // Debug.Log("Get text successful");
        String idTemp = questionList[idx].questionId;
        // String idTemp = questionList[idx]?.questionId.ToString()??"w123";
        QnId1.text = idTemp;
        // Debug.Log("get id sucessfully");
        // Debug.Log("ID is " + idTemp);
        }
        else{
            Debug.Log("Row 1 is empty");
            QnContent1.text = "";
            QnId1.text = "";
            addButton1.enabled = false;
        }
        if (questionList.Count>idx+1){
        QnContent2.text = questionList[idx+1].questionText;
        QnId2.text = questionList[idx+1].questionId;
        }
        else{
            Debug.Log("Row 2 is empty");
            QnContent2.text = "";
            QnId2.text = "";
            // addButton2.enabled = false;
        }

        if (questionList.Count>idx+2){
        QnContent3.text = questionList[idx+2].questionText;
        QnId3.text = questionList[idx+2].questionId;
        }
        else{
            Debug.Log("Row 3 is empty");
            QnContent3.text = "";
            QnId3.text = "";
            // addButton3.enabled = false;
        }
    }

    public void DisplayAddedQuestionData(int currAssimtQnIndex, List<Question> assignmentQnList){
        int idx_assignmt = Math.Abs(currAssimtQnIndex);
        // Debug.Log("Refresing currently added questions. current Assignment Index displayed is "+idx_assignmt);
        if (assignmentQnList.Count>idx_assignmt){
            removeButton1.enabled = true;
            AddedQnContent1.text = assignmentQnList[idx_assignmt].questionText;
            AddedQnId1.text = (idx_assignmt+1).ToString();
        }
        else{
            AddedQnContent1.text = "";
            AddedQnId1.text = "";
            removeButton1.enabled = false;
        }
        if (assignmentQnList.Count>idx_assignmt+1){
            AddedQnContent2.text = assignmentQnList[idx_assignmt+1].questionText;
            AddedQnId2.text = (idx_assignmt+2).ToString();
        }
        else{
            AddedQnContent2.text = "";
            AddedQnId2.text = "";
            // addButton2.enabled = false;
        }
        if (assignmentQnList.Count>idx_assignmt+2){
            AddedQnContent3.text = assignmentQnList[idx_assignmt+2].questionText;
            AddedQnId3.text =(idx_assignmt+3).ToString();
        }
        else{
            AddedQnContent3.text = "";
            AddedQnId3.text = "";
            // addButton3.enabled = false;
        }

    }

    public List<Question> addQnListener(int buttonIndex, List<Question> assignmentQnList){
        // Debug.Log("Listener add question listener now....");
        int indexToAdd = Math.Abs(currQuestionIndex) + buttonIndex;
          if (questionList.Count>=indexToAdd){
              if (assignmentQnList.Contains(questionList[indexToAdd])){
                    Debug.Log("Question already exist, donot add.");
                    // PopUpBox("Question already added!");
              }
              else{
                assignmentQnList.Add(questionList[indexToAdd]);
                Debug.Log("Question in row 1 added successfully! ");
                Debug.Log("Current assignment question list: ");
                int pos=0;
                foreach( var x in assignmentQnList) {
                    Debug.Log("Question No. "+ (pos+1).ToString()+" "+x.questionId + " " +x.questionText);
                    pos = pos + 1;}
              }
            }
        else{
            Debug.Log("Index out of range");
        }
        return assignmentQnList;
    }

    public List<Question> removeQnListener(int buttonIndex,List<Question> assignmentQnList){
        // Debug.Log("Loading remove qn listener now....");
        // Debug.Log("total number of question in the assignment is " + assignmentQnList.Count);
        // Debug.Log("Printing assignment question list...");
        // int pos=0;
        // foreach( var x in assignmentQnList) {
        //         Debug.Log("Question No. "+ (pos+1).ToString()+" "+x.questionId + " " +x.questionText);
        //     pos = pos + 1;
        // }
        if (assignmentQnList.Count>(0+buttonIndex)){
            assignmentQnList.RemoveAt(buttonIndex);
            // DisplayQuestionData(currQuestionIndex);
            Debug.Log("Current assignment question list: ");
            }
        else{
            Debug.Log("Index out of range");     
            // PopUpBox("Index out of range");   
        }
        return assignmentQnList;
    }

    public void Add1(){
        Debug.Log("=============Add1============");
        assignmentQnList = addQnListener(0,assignmentQnList);
        DisplayAddedQuestionData(currAssimtQnIndex, assignmentQnList);
    }

    public void Add2(){
        Debug.Log("=============Add2============");
        assignmentQnList = addQnListener(1,assignmentQnList);
        DisplayAddedQuestionData(currAssimtQnIndex, assignmentQnList);
    }

    public void Add3(){
        Debug.Log("=============Add3============");
        assignmentQnList = addQnListener(2,assignmentQnList);
        DisplayAddedQuestionData(currAssimtQnIndex, assignmentQnList);
    }


    public void Remove1(){
        Debug.Log("=============Remove1============");
        assignmentQnList = removeQnListener(0,assignmentQnList);
        DisplayAddedQuestionData(currAssimtQnIndex, assignmentQnList);
    }

    public void Remove2(){
        Debug.Log("=============Remove2============");
        assignmentQnList = removeQnListener(1,assignmentQnList);
        DisplayAddedQuestionData(currAssimtQnIndex, assignmentQnList);
    }

    public void Remove3(){
        Debug.Log("=============Remove3============");
        assignmentQnList = removeQnListener(2,assignmentQnList);
        DisplayAddedQuestionData(currAssimtQnIndex, assignmentQnList);
    }

    public void HandleWorldDropdown(Dropdown worldDropdown){
        int val = worldDropdown.value;
        if (val == 0){
            Debug.Log("val=0");
            worldSelected = null;
            GetQuestionDataFiltered();
              DisplayQuestionData(currQuestionIndex);
            Debug.Log(worldSelected);
            Debug.Log("-----------RefreshedFinihsed------------");

        }
        if (val==1){
            Debug.Log("val=1");

            worldSelected = "REQUIREMENT";
            GetQuestionDataFiltered();
            DisplayQuestionData(currQuestionIndex);
            Debug.Log(worldSelected);

            Debug.Log("-----------RefreshedFinihsed------------");
        }
        if (val==2){
            Debug.Log("val=2");
            worldSelected = "DESIGN";
            Debug.Log("World 3: " + worldSelected);
            GetQuestionDataFiltered();
            DisplayQuestionData(currQuestionIndex);
            Debug.Log(worldSelected);

            Debug.Log("-----------RefreshedFinihsed------------");
        }
        if (val==3){
            worldSelected = "IMPLEMENTATION";
            GetQuestionDataFiltered();
            DisplayQuestionData(currQuestionIndex);
            Debug.Log(worldSelected);

            Debug.Log("-----------RefreshedFinihsed------------");
        }
        Debug.Log("World " + worldSelected + " is selected.");
    }
    public void HandleSectionDropdown(Dropdown sectionDropdown){
        int val = sectionDropdown.value;
        if (val == 0){
            Debug.Log("val-0");
            sectionSelected = null;
        }
        if (val == 1){
            Debug.Log("val-1");
            sectionSelected = "1";
            GetQuestionDataFiltered();
            DisplayQuestionData(currQuestionIndex);
            Debug.Log("-----------RefreshedFinihsed------------");
        }
        if (val == 2){
            Debug.Log("val-2");
            sectionSelected = "3";
            GetQuestionDataFiltered();
            DisplayQuestionData(currQuestionIndex);
            Debug.Log("-----------RefreshedFinihsed------------");
        }
        if (val == 3){
            sectionSelected = "3";
            GetQuestionDataFiltered();
            DisplayQuestionData(currQuestionIndex);
            Debug.Log("-----------RefreshedFinihsed------------");
        }
        if (val == 4){
            sectionSelected = "4";
            GetQuestionDataFiltered();
            DisplayQuestionData(currQuestionIndex);
            Debug.Log("-----------RefreshedFinihsed------------");
        }
        Debug.Log("Section " + sectionSelected + " is selected.");
    }
    public void HandleDiffcultyDropdown(Dropdown levelDropdown){
        int val = levelDropdown.value;
        if (val == 0){
            difficultySelected = null;
            DisplayQuestionData(currQuestionIndex);
            Debug.Log("-----------RefreshedFinihsed------------");
        }
        if (val == 1){
            difficultySelected = "EASY";
            GetQuestionDataFiltered();
            DisplayQuestionData(currQuestionIndex);
            Debug.Log("-----------RefreshedFinihsed------------");
        }
        if (val == 2){
            difficultySelected = "MEDIUM";
            GetQuestionDataFiltered();
            DisplayQuestionData(currQuestionIndex);
            Debug.Log("-----------RefreshedFinihsed------------");
        }
        if (val == 3){
            difficultySelected = "HARD";
            GetQuestionDataFiltered();
             DisplayQuestionData(currQuestionIndex);
            Debug.Log("-----------RefreshedFinihsed------------");           
        }
        Debug.Log("Difficulty Level " + difficultySelected + " is selected " );
    }

    public void QnNextPage(){
        if (currQuestionIndex + QnRows<questionList.Count){
        // qnNextPageButton.enabled = true;
        currQuestionIndex += QnRows;
        Debug.Log("============Show next page (Page " +(currQuestionIndex/3+1) +") of question data.============");
        DisplayQuestionData(currQuestionIndex); }
        else{
            // qnNextPageButton.enabled = false;
            // PopUpBox("This is the Last page already!");
        }
    }

    public void QnPrevPage(){
 
        if(Math.Abs(currQuestionIndex)!=0){
        // Debug.Log("Current Question Index (absolute value) to be decreased later: " + Math.Abs(currQuestionIndex));
        // GetComponent<Button>().interactable = true;
        // qnPrevPageButton.enabled = true;
        currQuestionIndex = Math.Abs(currQuestionIndex) +(-3);
        // currQuestionIndex = -currQuestionIndex;
        Debug.Log("============Show prev page (Page " + (currQuestionIndex/3+1) +") of question data============");
        DisplayQuestionData(currQuestionIndex);}
        else{
            // qnNextPageButton.enabled = false;
            // PopUpBox("This is the first page already!");
        }
    }

    public void AssignmentNextPage(){

        currAssimtQnIndex += AssignmtRows;
        Debug.Log("============Show next page (Page " +(currAssimtQnIndex/3+1 )+") of assignment data============");

        DisplayAddedQuestionData(currAssimtQnIndex, assignmentQnList);
    }

    public void AssignmentPrevPage(){

        // Debug.Log("Current Assignment Question Index (absolute value) to be decreased later: " + Math.Abs(currAssimtQnIndex));
        currAssimtQnIndex = Math.Abs(currAssimtQnIndex)+(-3);
        Debug.Log("============Show prev page (Page " +(currAssimtQnIndex/3+1)+") of assignment data============");
        DisplayAddedQuestionData(currAssimtQnIndex,assignmentQnList);
    }



    public void SetButtons(){
        addButton1 = GameObject.Find("Add-button-1").GetComponent<Button>();
        addButton2 = GameObject.Find("Add-button-2").GetComponent<Button>();
        addButton3 = GameObject.Find("Add-button-3").GetComponent<Button>();

        removeButton1 = GameObject.Find("remove-button-1").GetComponent<Button>();
        removeButton2 = GameObject.Find("remove-button-2").GetComponent<Button>();
        removeButton3 = GameObject.Find("remove-button-3").GetComponent<Button>();

        addButton1.onClick.AddListener(()=>addQnListener(0,assignmentQnList));
        addButton2.onClick.AddListener(()=>addQnListener(1,assignmentQnList));
        addButton3.onClick.AddListener(()=>addQnListener(2,assignmentQnList));

        removeButton1.onClick.AddListener(()=>removeQnListener(0,assignmentQnList));
        removeButton2.onClick.AddListener(()=>removeQnListener(1,assignmentQnList));
        removeButton3.onClick.AddListener(()=>removeQnListener(2,assignmentQnList));
    }
    public void SetTextBoxObjects(){
        QnContent1=GameObject.Find("QnContent1").GetComponent<Text>(); 
        QnId1=GameObject.Find("QnId1").GetComponent<Text>();
        QnContent2=GameObject.Find("QnContent2").GetComponent<Text>(); 
        QnId2=GameObject.Find("QnId2").GetComponent<Text>();
        QnContent3=GameObject.Find("QnContent3").GetComponent<Text>(); 
        QnId3=GameObject.Find("QnId3").GetComponent<Text>();

        AddedQnContent1 = GameObject.Find("addedQnContent1").GetComponent<Text>();
        AddedQnId1 = GameObject.Find("addedQnId1").GetComponent<Text>();
        AddedQnContent2 = GameObject.Find("addedQnContent2").GetComponent<Text>();
        AddedQnId2 = GameObject.Find("addedQnId2").GetComponent<Text>();
        AddedQnContent3 = GameObject.Find("addedQnContent3").GetComponent<Text>();
        AddedQnId3 = GameObject.Find("addedQnId3").GetComponent<Text>();
    }

    public void SetDropdown(){
        worldDropdown = GameObject.Find("Dropdown - Select-world").GetComponent<Dropdown>();
        worldDropdown.onValueChanged.AddListener(delegate { HandleWorldDropdown(worldDropdown);});

        sectionDropdown = GameObject.Find("Dropdown - Select-section").GetComponent<Dropdown>();
        sectionDropdown.onValueChanged.AddListener(delegate { HandleSectionDropdown(sectionDropdown);});

        levelDropdown = GameObject.Find("Dropdown - Select-level").GetComponent<Dropdown>();
        levelDropdown.onValueChanged.AddListener(delegate { HandleDiffcultyDropdown(levelDropdown);});
    }
    public void nextPageButton(){
        SceneManager.LoadScene("PostAssignment-P2");    
    }

     public void backToMainButton(){
        SceneManager.LoadScene("TeacherWelcomeUI");    
    }

    public void PAInputDetailsPageBackButton(){
        SceneManager.LoadScene("PostAssignment-P2");
    }
    public void SelectTargetStudentButton(){
        SceneManager.LoadScene("PostAssignment-P3");
    }

    public void TargetStudentPageCancelButton(){
        SceneManager.LoadScene("PostAssignment-P2");
    }

    public void TargetStudentPageConfirmButton(){
        SceneManager.LoadScene("PostAssignment-P2");
    }

    public void goToPostAssignment(){
        SceneManager.LoadScene("PostAssignment-P1");
    }

}
