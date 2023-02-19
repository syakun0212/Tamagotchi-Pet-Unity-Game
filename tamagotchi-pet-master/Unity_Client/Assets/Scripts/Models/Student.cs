using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static User;


[System.Serializable]
public class Student : User
{
    public Student(string username, int highestScore, List<Pet> petsUnlocked, int currentFood, int currentWater, int numOfGamesCompleted, List<int> levelsUnlocked, string lastLoginDay)
    {
        this.username = username;
        this.highestScore = highestScore;
        this.petsUnlocked = petsUnlocked;
        this.currentFood = currentFood;
        this.currentWater = currentWater;
        this.numOfGamesCompleted = numOfGamesCompleted;
        this.levelsUnlocked = levelsUnlocked;
        this.lastLoginDay = lastLoginDay;
    }
    public int highestScore { get; set; } // for leaderboard ?
    public List<Pet> petsUnlocked { get; set; }
    public int currentFood { get; set; }
    public int currentWater { get; set; }
    public int numOfGamesCompleted { get; set; }
    public List<int> levelsUnlocked { get; set; }
    public string lastLoginDay;
}