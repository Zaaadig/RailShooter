using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text score;

    private void Update()
    {
        score.text = "Score: " + GameManager.m_score;
    }
}
