using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Pet
{
    public Pet(string petName, int petSkinId, string petPowerup, int petCurrentFood, int petCurrentWater)
    {
        this.petName = petName;
        this.petSkinId = petSkinId;
        this.petPowerup = petPowerup;
        this.petCurrentFood = petCurrentFood;
        this.petCurrentWater = petCurrentWater;
    }
    public string petName { get; set; }
    public int petSkinId { get; set; }
    public string petPowerup { get; set; }
    // public bool isPetUnlocked { get; set; }
    public int petCurrentWater { get; set; }
    public int petCurrentFood { get; set; }
    // public List<int> petNumOfGamesToUnlock { get; set; }
}