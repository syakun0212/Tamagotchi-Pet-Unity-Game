using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Stat
{
    public Stat(string statId, string roundId, string questionId, string studentUsername, int timing, bool isCorrect, int currentHealth, bool isSkillLeft)
    {
        this.statId = statId;
        this.roundId = roundId;
        this.questionId = questionId;
        this.studentUsername = studentUsername;
        this.timing = timing;
        this.isCorrect = isCorrect;
        this.currentHealth = currentHealth;
        this.isSkillLeft = isSkillLeft;
    }
    public string statId { get; set; }
    public string roundId { get; set; }
    public string questionId { get; set; }
    public string studentUsername { get; set; }
    public int timing { get; set; }
    public bool isCorrect { get; set; }
    public int currentHealth { get; set; }
    public bool isSkillLeft { get; set; }
}