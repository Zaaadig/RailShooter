using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelManager : MonoBehaviour
{
    public Text finalScoreText;
    public Text oldScore;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayFinalScore();
        }
    }

    public void DisplayFinalScore()
    {
        finalScoreText.text = "Your Score : " + GameManager.m_score;
        oldScore.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(true);
    }
}
