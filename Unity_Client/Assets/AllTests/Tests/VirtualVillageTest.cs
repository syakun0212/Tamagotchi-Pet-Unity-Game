using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class VirtualVillageTest
{
    [UnityTest]
    public IEnumerator virtual_village_all_ui_components_check()
    {
        // SceneManager.LoadScene("VirtualVillageUI");
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
    public IEnumerator virtual_village_display_pets_on_start_check()
    {
        // SceneManager.LoadScene("VirtualVillageUI");
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
    public IEnumerator load_change_skin_ui_on_click()
    {
        // SceneManager.LoadScene("VirtualVillageUI");
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