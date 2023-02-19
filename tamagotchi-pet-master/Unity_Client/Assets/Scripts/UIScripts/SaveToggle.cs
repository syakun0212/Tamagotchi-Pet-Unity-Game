using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveToggle : MonoBehaviour
{
    [SerializeField]
    private Toggle toggle1, toggle2;
    private ToggleGroup allowSwitch;

    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetString("ToggleSelected") == "student")
        {
            toggle1.isOn = true;
            toggle2.isOn = false;
        }
        else if (PlayerPrefs.GetString("ToggleSelected") == "teacher")
        {
            toggle1.isOn = false;
            toggle2.isOn = true;
        }
    }

    public void Toggle1Selected()
    {
        PlayerPrefs.SetString("ToggleSelected", "student");
    }
    public void Toggle2Selected()
    {
        PlayerPrefs.SetString("ToggleSelected", "teacher");
    }
}
