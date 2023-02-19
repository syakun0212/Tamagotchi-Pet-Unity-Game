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
    public IEnumerator leaderboard_check_rows_arranged_in_descending_score()
    {
        SceneManager.LoadScene("LeaderboardUI");
        yield return new WaitForSeconds(3);
        // Check page one
        RowUi rankOne = GameObject.Find("Row1").GetComponent<RowUi>();
        RowUi rankTwo = GameObject.Find("Row2").GetComponent<RowUi>();
        RowUi rankThree = GameObject.Find("Row3").GetComponent<RowUi>();
        RowUi rankFour = GameObject.Find("Row4").GetComponent<RowUi>();
        RowUi rankFive = GameObject.Find("Row5").GetComponent<RowUi>();

        int rankOneScore = int.Parse(rankOne.score.text);
        int rankTwoScore = int.Parse(rankTwo.score.text);
        int rankThreeScore = int.Parse(rankThree.score.text);
        int rankFourScore = int.Parse(rankFour.score.text);
        int rankFiveScore = int.Parse(rankFive.score.text);

        Assert.IsTrue(rankOneScore >= rankTwoScore); 
        Assert.IsTrue(rankTwoScore >= rankThreeScore); 
        Assert.IsTrue(rankThreeScore >= rankFourScore);
        Assert.IsTrue(rankFourScore >= rankFiveScore); 
        // Check page two
        Button nextPageButton = GameObject.Find("next-page-button").GetComponent<Button>();
        nextPageButton.onClick.Invoke();
        yield return new WaitForSeconds(3);

        RowUi rankSix = GameObject.Find("Row6").GetComponent<RowUi>();
        RowUi rankSeven = GameObject.Find("Row7").GetComponent<RowUi>();
        RowUi rankEight = GameObject.Find("Row8").GetComponent<RowUi>();
        RowUi rankNine = GameObject.Find("Row9").GetComponent<RowUi>();
        RowUi rankTen = GameObject.Find("Row10").GetComponent<RowUi>();

        int rankSixScore = int.Parse(rankSix.score.text);
        int rankSevenScore = int.Parse(rankSeven.score.text);
        int rankEightScore = int.Parse(rankEight.score.text);
        int rankNineScore = int.Parse(rankNine.score.text);
        int rankTenScore = int.Parse(rankTen.score.text);

        Assert.IsTrue(rankSixScore >= rankSevenScore); 
        Assert.IsTrue(rankSevenScore >= rankEightScore); 
        Assert.IsTrue(rankEightScore >= rankNineScore);
        Assert.IsTrue(rankNineScore >= rankTenScore); 

    }

    [UnityTest]
    // Check all rows on leaderboard have a name
    public IEnumerator leaderboard_check_all_rows_have_name()
    {
        SceneManager.LoadScene("LeaderboardUI");
        yield return new WaitForSeconds(3);

        Text rankOneName = GameObject.Find("Row1").GetComponent<RowUi>().name;
        Text rankTwoName = GameObject.Find("Row2").GetComponent<RowUi>().name;
        Text rankThreeName = GameObject.Find("Row3").GetComponent<RowUi>().name;
        Text rankFourName = GameObject.Find("Row4").GetComponent<RowUi>().name;
        Text rankFiveName = GameObject.Find("Row5").GetComponent<RowUi>().name;

        Assert.IsTrue(rankOneName);
        Assert.IsTrue(rankTwoName);
        Assert.IsTrue(rankThreeName);
        Assert.IsTrue(rankFourName);
        Assert.IsTrue(rankFiveName);

        // Check page two
        Button nextPageButton = GameObject.Find("next-page-button").GetComponent<Button>();
        nextPageButton.onClick.Invoke();
        yield return new WaitForSeconds(3);

        Text rankSixName = GameObject.Find("Row6").GetComponent<RowUi>().name;
        Text rankSevenName = GameObject.Find("Row7").GetComponent<RowUi>().name;
        Text rankEightName = GameObject.Find("Row8").GetComponent<RowUi>().name;
        Text rankNineName = GameObject.Find("Row9").GetComponent<RowUi>().name;
        Text rankTenName = GameObject.Find("Row10").GetComponent<RowUi>().name;

        Assert.IsTrue(rankSixName);
        Assert.IsTrue(rankSevenName);
        Assert.IsTrue(rankEightName);
        Assert.IsTrue(rankNineName);
        Assert.IsTrue(rankTenName);
    }

    [UnityTest]
    // Check all rows on leaderboard have a rank
    public IEnumerator leaderboard_check_all_rows_have_rank()
    {
        SceneManager.LoadScene("LeaderboardUI");
        yield return new WaitForSeconds(3);

        Text rankOneRank = GameObject.Find("Row1").GetComponent<RowUi>().rank;
        Text rankTwoRank = GameObject.Find("Row2").GetComponent<RowUi>().rank;
        Text rankThreeRank = GameObject.Find("Row3").GetComponent<RowUi>().rank;
        Text rankFourRank = GameObject.Find("Row4").GetComponent<RowUi>().rank;
        Text rankFiveRank = GameObject.Find("Row5").GetComponent<RowUi>().rank;

        Assert.IsTrue(rankOneRank);
        Assert.IsTrue(rankTwoRank);
        Assert.IsTrue(rankThreeRank);
        Assert.IsTrue(rankFourRank);
        Assert.IsTrue(rankFiveRank);

        // Check page two
        Button nextPageButton = GameObject.Find("next-page-button").GetComponent<Button>();
        nextPageButton.onClick.Invoke();
        yield return new WaitForSeconds(3);

        Text rankSixRank = GameObject.Find("Row6").GetComponent<RowUi>().rank;
        Text rankSevenRank = GameObject.Find("Row7").GetComponent<RowUi>().rank;
        Text rankEightRank = GameObject.Find("Row8").GetComponent<RowUi>().rank;
        Text rankNineRank = GameObject.Find("Row9").GetComponent<RowUi>().rank;
        Text rankTenRank = GameObject.Find("Row10").GetComponent<RowUi>().rank;

        Assert.IsTrue(rankSixRank);
        Assert.IsTrue(rankSevenRank);
        Assert.IsTrue(rankEightRank);
        Assert.IsTrue(rankNineRank);
        Assert.IsTrue(rankTenRank);
    }

    [UnityTest]
    // Check all rows on leaderboard have a score
    public IEnumerator leaderboard_check_all_rows_have_score()
    {
        SceneManager.LoadScene("LeaderboardUI");
        yield return new WaitForSeconds(3);

        Text rankOneScore = GameObject.Find("Row1").GetComponent<RowUi>().score;
        Text rankTwoScore = GameObject.Find("Row2").GetComponent<RowUi>().score;
        Text rankThreeScore = GameObject.Find("Row3").GetComponent<RowUi>().score;
        Text rankFourScore = GameObject.Find("Row4").GetComponent<RowUi>().score;
        Text rankFiveScore = GameObject.Find("Row5").GetComponent<RowUi>().score;

        Assert.IsTrue(rankOneScore);
        Assert.IsTrue(rankTwoScore);
        Assert.IsTrue(rankThreeScore);
        Assert.IsTrue(rankFourScore);
        Assert.IsTrue(rankFiveScore);

        // Check page two
        Button nextPageButton = GameObject.Find("next-page-button").GetComponent<Button>();
        nextPageButton.onClick.Invoke();
        yield return new WaitForSeconds(3);

        Text rankSixScore = GameObject.Find("Row6").GetComponent<RowUi>().score;
        Text rankSevenScore = GameObject.Find("Row7").GetComponent<RowUi>().score;
        Text rankEightScore = GameObject.Find("Row8").GetComponent<RowUi>().score;
        Text rankNineScore = GameObject.Find("Row9").GetComponent<RowUi>().score;
        Text rankTenScore = GameObject.Find("Row10").GetComponent<RowUi>().score;

        Assert.IsTrue(rankSixScore);
        Assert.IsTrue(rankSevenScore);
        Assert.IsTrue(rankEightScore);
        Assert.IsTrue(rankNineScore);
        Assert.IsTrue(rankTenScore);
    }

    [UnityTest]
    // Check that each page on the leaderboard has 5 rows
    public IEnumerator leaderboard_check_each_page_5_rows()
    {
        SceneManager.LoadScene("LeaderboardUI");
        yield return new WaitForSeconds(3);

        // Check for first page
        Text rankOneRank = GameObject.Find("Row1").GetComponent<RowUi>().rank;
        Text rankTwoRank = GameObject.Find("Row2").GetComponent<RowUi>().rank;
        Text rankThreeRank = GameObject.Find("Row3").GetComponent<RowUi>().rank;
        Text rankFourRank = GameObject.Find("Row4").GetComponent<RowUi>().rank;
        Text rankFiveRank = GameObject.Find("Row5").GetComponent<RowUi>().rank;

        Assert.IsTrue(rankOneRank);
        Assert.IsTrue(rankTwoRank);
        Assert.IsTrue(rankThreeRank);
        Assert.IsTrue(rankFourRank);
        Assert.IsTrue(rankFiveRank);

        Button nextPageButton = GameObject.Find("next-page-button").GetComponent<Button>();
        nextPageButton.onClick.Invoke();
        yield return new WaitForSeconds(3);

        // Check for second page
        Text rankSixRank = GameObject.Find("Row6").GetComponent<RowUi>().rank;
        Text rankSevenRank = GameObject.Find("Row7").GetComponent<RowUi>().rank;
        Text rankEightRank = GameObject.Find("Row8").GetComponent<RowUi>().rank;
        Text rankNineRank = GameObject.Find("Row9").GetComponent<RowUi>().rank;
        Text rankTenRank = GameObject.Find("Row10").GetComponent<RowUi>().rank;

        Assert.IsTrue(rankSixRank);
        Assert.IsTrue(rankSevenRank);
        Assert.IsTrue(rankEightRank);
        Assert.IsTrue(rankNineRank);
        Assert.IsTrue(rankTenRank);

    }


    [UnityTest]
    // Check that next page button on leaderboard can be clicked successfully
    public IEnumerator leaderboard_check_next_page_button()
    {
        SceneManager.LoadScene("LeaderboardUI");
        yield return new WaitForSeconds(3);

        Button nextPageButton = GameObject.Find("next-page-button").GetComponent<Button>();
        nextPageButton.onClick.Invoke();

        yield return new WaitForSeconds(3);
        Assert.IsTrue(nextPageButton); 

        // Check for second page
        Text rankSixRank = GameObject.Find("Row6").GetComponent<RowUi>().rank;
        Text rankSevenRank = GameObject.Find("Row7").GetComponent<RowUi>().rank;
        Text rankEightRank = GameObject.Find("Row8").GetComponent<RowUi>().rank;
        Text rankNineRank = GameObject.Find("Row9").GetComponent<RowUi>().rank;
        Text rankTenRank = GameObject.Find("Row10").GetComponent<RowUi>().rank;

        Assert.IsTrue(rankSixRank);
        Assert.IsTrue(rankSevenRank);
        Assert.IsTrue(rankEightRank);
        Assert.IsTrue(rankNineRank);
        Assert.IsTrue(rankTenRank);
    }

    [UnityTest]
    // Check that prev page button on leaderboard can be clicked successfully
    public IEnumerator leaderboard_check_prev_page_button()
    {
        SceneManager.LoadScene("LeaderboardUI");
        yield return new WaitForSeconds(3);

        Button nextPageButton = GameObject.Find("next-page-button").GetComponent<Button>();
        nextPageButton.onClick.Invoke();
        yield return new WaitForSeconds(3);
        Button prevPageButton = GameObject.Find("prev-page-button").GetComponent<Button>();
        prevPageButton.onClick.Invoke();

        yield return new WaitForSeconds(3);
        Assert.IsTrue(prevPageButton); 

        // Check for first page
        Text rankOneRank = GameObject.Find("Row1").GetComponent<RowUi>().rank;
        Text rankTwoRank = GameObject.Find("Row2").GetComponent<RowUi>().rank;
        Text rankThreeRank = GameObject.Find("Row3").GetComponent<RowUi>().rank;
        Text rankFourRank = GameObject.Find("Row4").GetComponent<RowUi>().rank;
        Text rankFiveRank = GameObject.Find("Row5").GetComponent<RowUi>().rank;

        Assert.IsTrue(rankOneRank);
        Assert.IsTrue(rankTwoRank);
        Assert.IsTrue(rankThreeRank);
        Assert.IsTrue(rankFourRank);
        Assert.IsTrue(rankFiveRank);
    }




    

}
