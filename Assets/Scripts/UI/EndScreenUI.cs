using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreenUI : MonoBehaviour
{
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI bodyText;

    public void SetEndScreen(bool didWin, int roundsSurvive)
    {
        headerText.text = didWin ? "You Win!" : "Game Over!";
        headerText.color = didWin ? Color.green : Color.red;
        bodyText.text = $"You survived {roundsSurvive} rounds";

    }

    public void OnPlayAgainButton ()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnMenuGame()
    {
        SceneManager.LoadScene("Menu");
    }

}
