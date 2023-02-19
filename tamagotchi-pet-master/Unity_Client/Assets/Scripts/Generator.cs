
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

// For Question_Bank_Add Scene
public class Generator : MonoBehaviour
{
    public static int randomNumber;
    public static int i;

    public void generate()
    {
        i = Random.Range(0, 1000000000); // i think need check if the number exist already or not
    }
}
