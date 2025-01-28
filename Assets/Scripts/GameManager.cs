using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static int m_score = 0;

    public static void AddScore(int points)
    {
        m_score += points;
        Debug.Log("Score : " + m_score);
    }

    public static void ResetScore()
    {
        m_score = 0;
    }
}
