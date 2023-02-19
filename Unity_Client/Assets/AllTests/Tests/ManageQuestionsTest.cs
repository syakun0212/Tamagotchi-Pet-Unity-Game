using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class ManageQuestionsTest
{
    [UnityTest]
    public IEnumerator world_select_check_all_ui_components_truth()
    {
        SceneManager.LoadScene("QuestionBankWorldUI");
        yield return new WaitForSeconds(3);

        Button PlanningButton = GameObject.Find("Button_Planning").GetComponent<Button>();
        Button DesignButton = GameObject.Find("Button_Design").GetComponent<Button>();
        Button ImpleButton = GameObject.Find("Button_Implementation").GetComponent<Button>();
        Button TestingButton = GameObject.Find("Button_Testing").GetComponent<Button>();
        Button MaintainButton = GameObject.Find("Button_Maintenance").GetComponent<Button>();

        Assert.AreEqual(PlanningButton.GetType(), typeof(Button));
        Assert.AreEqual(DesignButton.GetType(), typeof(Button));
        Assert.AreEqual(ImpleButton.GetType(), typeof(Button));
        Assert.AreEqual(TestingButton.GetType(), typeof(Button));
        Assert.AreEqual(MaintainButton.GetType(), typeof(Button));

    }

    [UnityTest]
    public IEnumerator section_select_check_all_ui_components_truth()
    {
        SceneManager.LoadScene("QuestionBankSection");
        yield return new WaitForSeconds(3);

        Button enterButton = GameObject.Find("Button_enter").GetComponent<Button>();
        Dropdown sectionDropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        Button backButton = GameObject.Find("Button").GetComponent<Button>();

        Assert.AreEqual(enterButton.GetType(), typeof(Button));
        Assert.AreEqual(backButton.GetType(), typeof(Button));
        Assert.AreEqual(sectionDropdown.GetType(), typeof(Dropdown));
    }

}