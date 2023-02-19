using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SinglePlayerRoundData
{
    public SinglePlayerRoundData(string roundId, string studentId, string sldcWorld, string specificSection, 
    string difficultyLevel, Pet characterUsed, List<Question> questionList, List<Stat> statList, int finalScore,
    int rewardedFood, int rewardedWater)
    {
        this.roundId = roundId;
        this.studentId = studentId;
        this.sldcWorld = sldcWorld;
        this.specificSection = specificSection;
        this.difficultyLevel = difficultyLevel;
        this.characterUsed = characterUsed;
        this.questionList = questionList;
        this.statList = statList;
        this.finalScore = finalScore;
        this.rewardedFood = rewardedFood;
        this.rewardedWater = rewardedWater;
    }
    public string roundId { get; set; }
    public string studentId { get; set; }
    public string sldcWorld { get; set; } // different sldc stages
    public string specificSection { get; set; } // section 1, 2, 3, 4
    public string difficultyLevel { get; set; } // level easy, medium, hard
    public Pet characterUsed { get; set; }
    // programmer's end
    public List<Question> questionList { get; set; }
    public List<Stat> statList { get; set; }
    // student's world
    public int finalScore { get; set; } 
    public int rewardedFood { get; set; }
    public int rewardedWater { get; set; }
}


