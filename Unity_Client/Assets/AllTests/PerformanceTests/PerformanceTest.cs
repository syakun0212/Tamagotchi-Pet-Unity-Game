// using System.Collections;
// using NUnit.Framework;
// using Unity.PerformanceTesting;
// using UnityEngine;
// using UnityEngine.TestTools;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;
// using UnityEngine.Profiling;
// using System;
// using TMPro;

// public class PerformanceTest
// {
//     // Test for Login
//     [UnityTest, Performance]
//     public IEnumerator Login()
//     {
//         using (Measure.Scope(new SampleGroup("Setup.LoadScene")))
//         {
//             SceneManager.LoadScene("StudentLoginUI");
//         }
//         yield return null;

//         yield return new WaitForSeconds(.001f);

//         TMP_InputField usernameInput = GameObject.Find("InputUsername").GetComponent<TMP_InputField>();
//         usernameInput.text = "studentH";
//         usernameInput.textComponent.SetText("studentH");


//         TMP_InputField passwordInput = GameObject.Find("InputPassword").GetComponent<TMP_InputField>();
//         passwordInput.text = "studenth123";
//         passwordInput.textComponent.SetText("studenth123");

//         Button LoginButton = GameObject.Find("LoginButton").GetComponent<Button>();
//         LoginButton.onClick.Invoke();

//         yield return new WaitForSeconds(.001f);

//         yield return Measure.Frames().Run();
//     }

//     // Loading Scene Measurement
//     [UnityTest, Performance]
//     public IEnumerator Loading_Scene_Measurement()
//     {
//         using (Measure.Scope(new SampleGroup("Setup.LoadScene")))
//         {
//             SceneManager.LoadScene("LeaderboardUI");
//         }
//         yield return null;

//         yield return Measure.Frames().Run();
//     }

//     // Measure the total allocated memory and total reserved memory
//     [Test, Performance, Version("1")]
//     public void Measure_Custom()
//     {
//         var allocated = new SampleGroup("TotalAllocatedMemory", SampleUnit.Megabyte);
//         var reserved = new SampleGroup("TotalReservedMemory", SampleUnit.Megabyte);
//         Measure.Custom(allocated, Profiler.GetTotalAllocatedMemoryLong() / 1048576f);
//         Measure.Custom(reserved, Profiler.GetTotalReservedMemoryLong() / 1048576f);
//     }
    

//     // Measure transition time from main menu to start single player game
//     [UnityTest, Performance]
//     public IEnumerator StudentMainMenu_to_SinglePlayerMode()
//     {
//         using (Measure.Scope(new SampleGroup("Setup.LoadScene")))
//         {
//             SceneManager.LoadScene("StudentWelcomeUI");
//         }
//         yield return new WaitForSeconds(.001f);

//         Button StartGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();
//         StartGameButton.onClick.Invoke();

//         yield return new WaitForSeconds(.001f);

//         Button SingleButton = GameObject.Find("SingleButton").GetComponent<Button>();
//         SingleButton.onClick.Invoke();

//         yield return null;
//         yield return Measure.Frames().Run();
//     }

//     // Measure transition time from main menu to start multiplayer game
//     [UnityTest, Performance]
//     public IEnumerator StudentMainMenu_to_MultiPlayerMode()
//     {
//         using (Measure.Scope(new SampleGroup("Setup.LoadScene")))
//         {
//             SceneManager.LoadScene("StudentWelcomeUI");
//         }
//         yield return new WaitForSeconds(.001f);

//         Button StartGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();
//         StartGameButton.onClick.Invoke();

//         yield return new WaitForSeconds(.001f);

//         Button MultiButton = GameObject.Find("MultiButton").GetComponent<Button>();
//         MultiButton.onClick.Invoke();

//         yield return null;
//         yield return Measure.Frames().Run();
//     }

//     // Loading Next Page Button
//     [UnityTest, Performance]
//     public IEnumerator Next_Page_Button()
//     {
//         using (Measure.Scope(new SampleGroup("Setup.LoadScene")))
//         {
//             SceneManager.LoadScene("LeaderboardUI");
//         }
//         yield return new WaitForSeconds(.001f);

//         Button NextPageButton = GameObject.Find("next-page-button").GetComponent<Button>();
//         NextPageButton.onClick.Invoke();

//         yield return null;

//         yield return Measure.Frames().Run();
//     }

// }