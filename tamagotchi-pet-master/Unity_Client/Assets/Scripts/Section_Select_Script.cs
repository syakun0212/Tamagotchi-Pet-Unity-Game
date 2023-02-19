using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System.Linq;

public class Section_Select_Script : MonoBehaviour
{

    public static string sectionChoice;

    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();

        List<string> items = new List<string>();

        if (World_Select_Script.worldChoice == "REQUIREMENT")
        {
            items.Add("1");
            items.Add("2");
            items.Add("3");
            items.Add("4");
        }
        if (World_Select_Script.worldChoice == "DESIGN")
        {
            items.Add("1");
            items.Add("2");
            items.Add("3");
            items.Add("4");
        }
        if (World_Select_Script.worldChoice == "IMPLEMENTATION")
        {
            items.Add("1");
            items.Add("2");
            items.Add("3");
            items.Add("4");
        }

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });

    }

    public void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        try
        {
            sectionChoice = dropdown.options[index].text;
        }
        catch (ArgumentOutOfRangeException)
        {
            sectionChoice = "";
        }
    }

    // Start is called before the first frame update
    public void onSectionSelect()
    {
        SceneManager.LoadScene("QuestionBank");
    }
}
