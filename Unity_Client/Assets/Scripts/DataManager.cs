using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement; // can load scene
public class DataManager : MonoBehaviour
{
    public SinglePlayerRoundData SinglePlayerInstance;
    public MultiPlayerRoundData MultiPlayerInstance;
    public Assignment AssignmentInstance;
    public CustomGame CustomGameInstance;
    // public Student student { get; set; }
    public string username { get; set; }
    public int selectedPetIndex { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("MainPageUI");
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public SinglePlayerRoundData GetSinglePlayerInstance()
    {
        return SinglePlayerInstance;
    }

    public MultiPlayerRoundData GetMultiPlayerInstance()
    {
        return MultiPlayerInstance;
    }
    public Assignment GetAssignment()
    {
        AssignmentInstance = new Assignment(
            "01",
            100,
            "AssignmentName",
            "AssignmentTopid",
            "AssignmentDescription"
        );
        return AssignmentInstance;
    }

    public CustomGame GetCustomGame()
    {
        CustomGameInstance = new CustomGame(
            "01",
            "GameName",
            "GameDescription"
        );
        return CustomGameInstance;
    }
    public string generateUID()
    {
        var ticks = DateTime.Now.Ticks;
        string guid = Guid.NewGuid().ToString();
        string uniqueSessionId = ticks.ToString() + '-' + guid; //guid created by combining ticks 
        return uniqueSessionId;
    }
    private static System.Random random = new System.Random();
    public static string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static IEnumerator TakeScreenshotAndShare(String shareMsg)
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
        ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
        ss.Apply();

        string filePath = Path.Combine( Application.temporaryCachePath, "shared img.png" );
        File.WriteAllBytes( filePath, ss.EncodeToPNG() );

        // To avoid memory leaks
        Destroy( ss );

        // new NativeShare().AddFile( filePath )
        //     .SetSubject( "Tamagotchi Pet" ).SetText(shareMsg)
        //     .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
        //     .Share();

    }

}
