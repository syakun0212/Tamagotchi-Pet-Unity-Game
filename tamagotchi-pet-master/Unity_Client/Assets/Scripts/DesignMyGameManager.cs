// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using System.Linq;
// using System.Text.RegularExpressions;
// using Newtonsoft.Json;

// using TMPro;



// public class DesignMyGame : MonoBehaviour
// {
//     private HttpManager http;
//     private SceneLoaderManager scene;
//     private List<Question> questionList;

//     void Start()
//     {
//         var selectWorldDropdown = transform.GetComponent<Dropdown>();

//         dropdown.options.Clear();

//         List<string> worldList = new List<string>();
//         worldList.Add("software design");
//         worldList.Add("design principles");

//         foreach(var world in worldList){
//             dropdown.options.Add(new Dropdown.OptionData(){text=world});
//         }

//         DropdownItemSelected(dropdown);
//         dropdown.onValueChanged.AddListener(delegate{DropdownItemSelected(dropdown);});

//         void DropdownItemSelected(Dropdown dropdown){
//             int index = dropdown.value;
//             TextBox.text = dropdown.options[index].text;

//         }
//     }

//     // public selectWorldDropdown(){

//     // }



    

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
