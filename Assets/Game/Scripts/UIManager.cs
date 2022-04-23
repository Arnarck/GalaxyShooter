using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject titleScreen;
    public Image livesImageDisplay;
    public Text scoreDisplay;
    public Sprite[] lives;

    public int score = 0;

    //Update the player's lives on the interface after the player take damage
    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreDisplay.text = "Score: " + score.ToString();
    }

    public void ShowtitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        score = 0;
        scoreDisplay.text = "Score: 00";
    }
}
