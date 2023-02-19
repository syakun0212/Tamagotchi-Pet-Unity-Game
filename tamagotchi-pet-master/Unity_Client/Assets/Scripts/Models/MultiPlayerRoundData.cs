using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiPlayerRoundData
{
    public string multiRoundId { get; set; }

    public string playerStudentId { get; set; }
    
    public string opponentRoundId { get; set; }

    public string sldcWorld { get; set; } // different sldc stages
    public string specificSection { get; set; } // section 1, 2, 3, 4
    public string difficultyLevel { get; set; } // level easy, medium, hard
    public Pet playerCharacterUsed { get; set; }
    public Pet opponentCharacterUsed { get; set; }
    // programmer's end
    public List<Question> questionList { get; set; }
    public List<Stat> opponentStatList { get; set; } 
    public List<Stat> playerStatList { get; set; }
    // student's world
    public int winPoint { get; set; }
    public int finalScore { get; set; } 
    public int rewardedFood { get; set; }
    public int rewardedWater { get; set; }
}