using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class CustomGame : MonoBehaviour
{
  
   public CustomGame(){

   }
    [JsonConstructor]
   public CustomGame(string customeGameId, string customeGameName, string customGameDescription){
      this.customeGameId = customeGameId;
      this.customeGameName = customeGameName;
      this.customGameDescription = customGameDescription;
      // this.shareStudentList = shareStudentList;
      // this.questionList = questionList;
   }
   public string customeGameId { get; set; }
   public string customeGameName { get; set; }
   public string customGameDescription { get; set; }

   // public List<Student> shareStudentList { get; set; }
   public List<Question> questionList { get; set; }
}
