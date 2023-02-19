using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LeaderboardTest
{
    [UnityTest]
    //Check that students are arranged by descending score
    public IEnumerator design_my_game_all_ui_components_check()
    {
        SceneManager.LoadScene("DesignMyGame-1");
        yield return new WaitForSeconds(3);
        // Check page one
        RowUi rowOne = GameObject.Find("Row1").GetComponent<RowUi>();
        RowUi rowTwo = GameObject.Find("Row2").GetComponent<RowUi>();
        RowUi rowThree = GameObject.Find("Row3").GetComponent<RowUi>();
        RowUi rowOne_preview_board = GameObject.Find("Row1-preview-board").GetComponent<RowUi>();
        RowUi rowTwo_preview_board = GameObject.Find("Row2-preview-board").GetComponent<RowUi>();
        RowUi rowThree_preview_board = GameObject.Find("Row3-preview-board").GetComponent<RowUi>();

        Button nextPageButton = GameObject.Find("qn-data-board-next-page-button").GetComponent<Button>();
        Button prevPageButton = GameObject.Find("qn-data-board-prev-page-button").GetComponent<Button>();
        Button nextPageButton_preview_board = GameObject.Find("preview-board-next-page-button").GetComponent<Button>();
        Button prevPageButton_preview_board = GameObject.Find("preview-board-prev-page-button").GetComponent<Button>();

        RowUi header1 = GameObject.Find("Header-id").GetComponent<RowUi>();
        RowUi header2 = GameObject.Find("header-content").GetComponent<RowUi>();
        RowUi header1_preview_board = GameObject.Find("Header-id-preview-board").GetComponent<RowUi>();
        RowUi header2_preview_board = GameObject.Find("header-content-preview-board").GetComponent<RowUi>();


        Dropdown selectWorld = GameObject.Find("select-world").GetComponent<DropDown>();
        Dropdown selectSection = GameObject.Find("select-section").GetComponent<DropDown>();
        Dropdown selectLevel = GameObject.Find("select-level").GetComponent<DropDown>();


        Assert.AreEqual(rowOne.GetType(), typeof(RowUi));
        Assert.AreEqual(rowTwo.GetType(), typeof(RowUi));
        Assert.AreEqual(rowThree.GetType(), typeof(RowUi));
        Assert.AreEqual(rowOne_preview_board.GetType(), typeof(RowUi));
        Assert.AreEqual(rowTwo_preview_board.GetType(), typeof(RowUi));
        Assert.AreEqual(rowThree_preview_board.GetType(), typeof(RowUi));

        Assert.AreEqual(nextPageButton.GetType(), typeof(Button));
        Assert.AreEqual(prevPageButton.GetType(), typeof(Button));
        Assert.AreEqual(nextPageButton_preview_board.GetType(), typeof(Button));
        Assert.AreEqual(prevPageButton_preview_board.GetType(), typeof(Button));


        Assert.AreEqual(header1.GetType(), typeof(RowUi));
        Assert.AreEqual(header2.GetType(), typeof(RowUi));
        Assert.AreEqual(header1_preview_board.GetType(), typeof(RowUi));
        Assert.AreEqual(header2_preview_board.GetType(), typeof(RowUi));

        Assert.AreEqual(selectWorld.GetType(), typeof(RowUi));
        Assert.AreEqual(selectSection.GetType(), typeof(RowUi));
        Assert.AreEqual(selectLevel.GetType(), typeof(RowUi));
    }
    public IEnumerator design_my_game_qn_data_view_rows_check()
    {
        SceneManager.LoadScene("DesignMyGame-1");
        yield return new WaitForSeconds(3);
        // Check page one
        RowUi rowOne = GameObject.Find("Row1").GetComponent<RowUi>();
        RowUi rowTwo = GameObject.Find("Row2").GetComponent<RowUi>();
        RowUi rowThree = GameObject.Find("Row3").GetComponent<RowUi>();

        int rowOneuId = int.Parse(rowOne.id.text);
        int rowTwoId = int.Parse(rowTwo.id.text);
        int rowThreeId = int.Parse(rowThree.id.text);
 
        Assert.IsTrue(rowOneId >= rowTwoId); 
        Assert.IsTrue(rowTwoId >= rowThreeId); 
        Assert.IsTrue(rowThreeId >= rowFourId);
    }

    public IEnumerator design_my_game_qn_data_view_headers_check()
    {
        SceneManager.LoadScene("DesignMyGame-1");
        yield return new WaitForSeconds(3);
        // Check page one
        RowUi textElement1 = GameObject.Find("Header-id").GetComponent<RowUi>();
        RowUi textElement2 = GameObject.Find("header-content").GetComponent<RowUi>();
        

        int textElement1_text = int.Parse(textElement1.text);
        int textElement2_text = int.Parse(textElement2.text);
 
        Assert.IsNotEmpty(); 
        Assert.IsNotEmpty(textElement1_text); 
    }

    

    public IEnumerator design_my_game_qn_data_view_next_page_button_check()
    {
        SceneManager.LoadScene("DesignMyGame-1");
        yield return new WaitForSeconds(3);

        Button nextPageButton = GameObject.Find("qn-data-board-next-page-button").GetComponent<Button>();
        nextPageButton.onClick.Invoke();

        yield return new WaitForSeconds(3);
        Assert.IsTrue(nextPageButton); 

        RowUi rowOne = GameObject.Find("Row1").GetComponent<RowUi>();
        RowUi rowTwo = GameObject.Find("Row2").GetComponent<RowUi>();
        RowUi rowThree = GameObject.Find("Row3").GetComponent<RowUi>();

        int rowOneuId = int.Parse(rowOne.id.text);
        int rowTwoId = int.Parse(rowTwo.id.text);
        int rowThreeId = int.Parse(rowThree.id.text);
 
        Assert.IsTrue(rowOneId >= rowTwoId); 
        Assert.IsTrue(rowTwoId >= rowThreeId); 
        Assert.IsTrue(rowThreeId >= rowFourId);

    }

    public IEnumerator design_my_game_qn_data_view_prev_page_button_check()
    {
        SceneManager.LoadScene("DesignMyGame-1");
        yield return new WaitForSeconds(3);

        Button nextPageButton = GameObject.Find("qn-data-board-next-page-button").GetComponent<Button>();
        nextPageButton.onClick.Invoke();

        yield return new WaitForSeconds(3);
        Assert.IsTrue(nextPageButton); 

        RowUi rowOne = GameObject.Find("Row1").GetComponent<RowUi>();
        RowUi rowTwo = GameObject.Find("Row2").GetComponent<RowUi>();
        RowUi rowThree = GameObject.Find("Row3").GetComponent<RowUi>();

        int rowOneuId = int.Parse(rowOne.id.text);
        int rowTwoId = int.Parse(rowTwo.id.text);
        int rowThreeId = int.Parse(rowThree.id.text);
 
        Assert.IsTrue(rowOneId >= rowTwoId); 
        Assert.IsTrue(rowTwoId >= rowThreeId); 
        Assert.IsTrue(rowThreeId >= rowFourId);


    }

    public IEnumerator design_my_game_qn_data_view_add_button_check()
    {
        SceneManager.LoadScene("DesignMyGame-1");
        yield return new WaitForSeconds(3);

        Button nextPageButton = GameObject.Find("qn-data-board-add-button-1").GetComponent<Button>();
        nextPageButton.onClick.Invoke();

        yield return new WaitForSeconds(3);
        Assert.IsTrue(nextPageButton); 

        RowUi rowOne = GameObject.Find("Row1-preview-baord").GetComponent<RowUi>();

        int rowOneuId = int.Parse(rowOne.id.text);
 
        Assert.IsNotEmpty(rowOneId); 
        //////
        yield return new WaitForSeconds(3);

        Button nextPageButton = GameObject.Find("qn-data-board-add-button-2").GetComponent<Button>();
        nextPageButton.onClick.Invoke();

        yield return new WaitForSeconds(3);
        Assert.IsTrue(nextPageButton); 

        RowUi rowTwo = GameObject.Find("Row2-preview-baord").GetComponent<RowUi>();

        int rowOneuId = int.Parse(rowTwo.id.text);
 
        Assert.IsNotEmpty(rowTwoId); 
        //////
        yield return new WaitForSeconds(3);

        Button nextPageButton = GameObject.Find("qn-data-board-add-button-3").GetComponent<Button>();
        nextPageButton.onClick.Invoke();

        yield return new WaitForSeconds(3);
        Assert.IsTrue(nextPageButton); 

        RowUi rowthree = GameObject.Find("Row3-preview-baord").GetComponent<RowUi>();

        int rowthreeuId = int.Parse(rowthree.id.text);
 
        Assert.IsNotEmpty(rowthreeId); 
    }
    
    
    public IEnumerator design_my_game_added_qn_view_delete_button_check()
    {
        SceneManager.LoadScene("DesignMyGame-1");
        yield return new WaitForSeconds(3);

        Button nextPageButton = GameObject.Find("prevew-board-remove-button-1").GetComponent<Button>();
        nextPageButton.onClick.Invoke();

        yield return new WaitForSeconds(3);
        Assert.IsTrue(nextPageButton); 

        RowUi rowOne = GameObject.Find("Row1-preview-baord").GetComponent<RowUi>();

        int rowOneuId = int.Parse(rowOne.id.text);
 
        Assert.IsEmpty(rowOneId); 

        //////
        yield return new WaitForSeconds(3);

        Button nextPageButton = GameObject.Find("prevew-board-remove-button-2").GetComponent<Button>();
        nextPageButton.onClick.Invoke();

        yield return new WaitForSeconds(3);
        Assert.IsTrue(nextPageButton); 

        RowUi rowTwo = GameObject.Find("Row2-preview-baord").GetComponent<RowUi>();

        int rowOneuId = int.Parse(rowTwo.id.text);
 
        Assert.IsEmpty(rowTwoId); 

        //////
        yield return new WaitForSeconds(3);

        Button nextPageButton = GameObject.Find("prevew-board-remove-button-3").GetComponent<Button>();
        nextPageButton.onClick.Invoke();

        yield return new WaitForSeconds(3);
        Assert.IsTrue(nextPageButton); 

        RowUi rowthree = GameObject.Find("Row3-preview-baord").GetComponent<RowUi>();

        int rowthreeuId = int.Parse(rowthree.id.text);
 
        Assert.IsEmpty(rowthreeId); 

    }

    public IEnumerator design_my_game_added_qn_view_prev_page_check()
    {
        SceneManager.LoadScene("DesignMyGame-1");
        yield return new WaitForSeconds(3);

        Button nextPageButton = GameObject.Find("preview-board-next-page-button").GetComponent<Button>();
        nextPageButton.onClick.Invoke();

        yield return new WaitForSeconds(3);
        Assert.IsTrue(nextPageButton); 

        RowUi rowOne = GameObject.Find("Row1-preview-board").GetComponent<RowUi>();
        RowUi rowTwo = GameObject.Find("Row2-preview-board").GetComponent<RowUi>();
        RowUi rowThree = GameObject.Find("Row3-preview-board").GetComponent<RowUi>();

        int rowOneuId = int.Parse(rowOne.id.text);
        int rowTwoId = int.Parse(rowTwo.id.text);
        int rowThreeId = int.Parse(rowThree.id.text);
 
        Assert.IsTrue(rowOneId >= rowTwoId); 
        Assert.IsTrue(rowTwoId >= rowThreeId); 
        Assert.IsTrue(rowThreeId >= rowFourId);

    }

    public IEnumerator design_my_game_added_qn_view_prev_page_check()
    {
        SceneManager.LoadScene("DesignMyGame-1");
        yield return new WaitForSeconds(3);

        Button nextPageButton = GameObject.Find("preview-board-prev-page-button").GetComponent<Button>();
        nextPageButton.onClick.Invoke();

        yield return new WaitForSeconds(3);
        Assert.IsTrue(nextPageButton); 

        RowUi rowOne = GameObject.Find("Row1-preview-board").GetComponent<RowUi>();
        RowUi rowTwo = GameObject.Find("Row2-preview-board").GetComponent<RowUi>();
        RowUi rowThree = GameObject.Find("Row3-preview-board").GetComponent<RowUi>();

        int rowOneuId = int.Parse(rowOne.id.text);
        int rowTwoId = int.Parse(rowTwo.id.text);
        int rowThreeId = int.Parse(rowThree.id.text);
 
        Assert.IsTrue(rowOneId >= rowTwoId); 
        Assert.IsTrue(rowTwoId >= rowThreeId); 
        Assert.IsTrue(rowThreeId >= rowFourId);

    }



          public IEnumerator design_my_game_input_description_text_check()
    {
        SceneManager.LoadScene("DesignMyGame-1");
        yield return new WaitForSeconds(3);
        
        Input = GameObject.Find("description_input").GetComponent<Input>();

        Assert.IsNotEmpty(Input);

    }

            public IEnumerator title_text_check()
    {
        SceneManager.LoadScene("DesignMyGame-1");
        yield return new WaitForSeconds(3);
        
        Input = GameObject.Find("title_input").GetComponent<Input>();

        Assert.IsNotEmpty(Input);
    }

    public IEnumerator design_my_game_added_qn_view_rows_check()
    {
        SceneManager.LoadScene("DesignMyGame-1");
        yield return new WaitForSeconds(3);
        // Check page one
        RowUi rowOne = GameObject.Find("Row1-preview-board").GetComponent<RowUi>();
        RowUi rowTwo = GameObject.Find("Row2-preview-board").GetComponent<RowUi>();
        RowUi rowThree = GameObject.Find("Row3-preview-board").GetComponent<RowUi>();

        int rowOneuId = int.Parse(rowOne.id.text);
        int rowTwoId = int.Parse(rowTwo.id.text);
        int rowThreeId = int.Parse(rowThree.id.text);
 
        Assert.IsTrue(rowOneId >= rowTwoId); 
        Assert.IsTrue(rowTwoId >= rowThreeId); 
        Assert.IsTrue(rowThreeId >= rowFourId);
    }






    

}
