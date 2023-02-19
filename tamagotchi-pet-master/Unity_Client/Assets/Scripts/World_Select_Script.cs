using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;

public class World_Select_Script : MonoBehaviour
{

    public static string worldChoice;

    // Start is called before the first frame update

    public void onDesignSelect()
    {
        worldChoice = "DESIGN";
        SceneManager.LoadScene("QuestionBankSection");
    }

    public void onImplementationSelect()
    {
        worldChoice = "IMPLEMENTATION";
        SceneManager.LoadScene("QuestionBankSection");
    }

    public void onRequirementSelect()
    {
        worldChoice = "REQUIREMENT";
        SceneManager.LoadScene("QuestionBankSection");
    }




}
