using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static User;

[System.Serializable]
public class Teacher : User
{
    public void viewStatistics()
    {
        Debug.Log("View stats");
    }

    public void createAssignment()
    {
        Debug.Log("Create assignment");
    }




}