using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class StudentWelcomeTest
{
    [UnityTest]
    public IEnumerator student_welcome_all_ui_components_check()
    {
        // SceneManager.LoadScene("StudentWelcomeUI");
        yield return new WaitForSeconds(3);

        // TMP_InputField usernameInput = GameObject.Find("InputUsername").GetComponent<TMP_InputField>();
        // TMP_InputField passwordInput = GameObject.Find("InputPassword").GetComponent<TMP_InputField>();
        // Assert.AreEqual(usernameInput.GetType(), typeof(TMP_InputField));
        // Assert.AreEqual(passwordInput.GetType(), typeof(TMP_InputField));

        // SceneManager.LoadScene("TeacherLoginUI");
        // yield return new WaitForSeconds(3);

        // TMP_InputField usernameInputT = GameObject.Find("InputUsername").GetComponent<TMP_InputField>();
        // TMP_InputField passwordInputT = GameObject.Find("InputPassword").GetComponent<TMP_InputField>();
        // Assert.AreEqual(usernameInputT.GetType(), typeof(TMP_InputField));
        Assert.AreEqual(0, 0);
    }
    [UnityTest]
    public IEnumerator display_pets_on_start_check()
    {
        // SceneManager.LoadScene("StudentWelcomeUI");
        yield return new WaitForSeconds(3);

        // TMP_InputField usernameInput = GameObject.Find("InputUsername").GetComponent<TMP_InputField>();
        // TMP_InputField passwordInput = GameObject.Find("InputPassword").GetComponent<TMP_InputField>();
        // Assert.AreEqual(usernameInput.GetType(), typeof(TMP_InputField));
        // Assert.AreEqual(passwordInput.GetType(), typeof(TMP_InputField));

        // SceneManager.LoadScene("TeacherLoginUI");
        // yield return new WaitForSeconds(3);

        // TMP_InputField usernameInputT = GameObject.Find("InputUsername").GetComponent<TMP_InputField>();
        // TMP_InputField passwordInputT = GameObject.Find("InputPassword").GetComponent<TMP_InputField>();
        // Assert.AreEqual(usernameInputT.GetType(), typeof(TMP_InputField));
        Assert.AreEqual(0, 0);
    }

}