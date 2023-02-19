using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class Assignment : MonoBehaviour
{
    [JsonConstructor]
    public Assignment(string assignmentId, int totalMark, string assignmentName, string assignmentTopic, string assignmentDescriptions){ 
        this.assignmentId = assignmentId;
        this.totalMark = totalMark;
        this.assignmentName = assignmentName;
        this.assignmentTopic = assignmentTopic;
        this.assignmentDescriptions = assignmentDescriptions;
       // this.deuDate = new DateTime(Year, Month, Date, 23, 59, 00);
    }
    public string assignmentId { get; set; }
    public int totalMark { get; set; }
    public string assignmentName {get; set; }
    public string assignmentTopic { get; set; }
    public string assignmentDescriptions {get; set;}    
    
    public List<Question> questionList { get; set; } // get from backend
    
}
