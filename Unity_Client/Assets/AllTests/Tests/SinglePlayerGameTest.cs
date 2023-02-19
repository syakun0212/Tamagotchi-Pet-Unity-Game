// login then play game

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class SinglePlayerGameTest
{
    [UnityTest]
    public IEnumerator student_login_all_correct()
    {
        SceneManager.LoadScene("Persistent");
        yield return new WaitForSeconds(1);

        Button studentButton = GameObject.Find("StudentLoginButton").GetComponent<Button>();
        studentButton.onClick.Invoke();
        yield return new WaitForSeconds(1);

        TMP_InputField usernameInput = GameObject.Find("InputUsername").GetComponent<TMP_InputField>();
        TMP_InputField passwordInput = GameObject.Find("InputPassword").GetComponent<TMP_InputField>();
        usernameInput.text = "aaaaaaaa"; // jes test account
        passwordInput.text = "aaaaaaaa"; // jes test account
        Assert.AreEqual(usernameInput.text, "aaaaaaaa");
        Assert.AreEqual(passwordInput.text, "aaaaaaaa");
        Button loginButton = GameObject.Find("LoginButton").GetComponent<Button>();
        loginButton.onClick.Invoke();
        Text warningMessage = GameObject.Find("MessageLabel").GetComponent<Text>();
        Assert.AreEqual(warningMessage.text, "Successfully authenticated");
        yield return new WaitForSeconds(1);

        // hello, student_name!
        Text helloWorld = GameObject.Find("HelloHeader").GetComponent<Text>();
        Assert.AreEqual(helloWorld.text, "hello, aaaaaaaa!");

        Button startGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();
        startGameButton.onClick.Invoke();
        yield return new WaitForSeconds(1);

        // check new screen

        Button singlePlayerButton = GameObject.Find("SingleButton").GetComponent<Button>();
        singlePlayerButton.onClick.Invoke();
        yield return new WaitForSeconds(1);

        // check new screen

        Button worldButton = GameObject.Find("World1Button").GetComponent<Button>();
        worldButton.onClick.Invoke();
        Button sectionButton = GameObject.Find("Section1Button").GetComponent<Button>();
        sectionButton.onClick.Invoke();
        Button levelButton = GameObject.Find("Level1Button").GetComponent<Button>();
        levelButton.onClick.Invoke();
        Button nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        nextButton.onClick.Invoke();
        yield return new WaitForSeconds(1);

        // check new screen

        Button enterGameButton = GameObject.Find("EnterGameButton").GetComponent<Button>();
        enterGameButton.onClick.Invoke();
        yield return new WaitForSeconds(1);


    }
}
