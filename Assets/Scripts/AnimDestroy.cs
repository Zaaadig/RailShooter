using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDestroy : MonoBehaviour
{
    [SerializeField] private Animator m_self;
    [SerializeField] private GameObject[] enemyList;
    //[SerializeField] private GameObject m_enemy1;
    //[SerializeField] private GameObject m_enemy2;
    [SerializeField] private Rail_Enemy m_railEnemy;
    [SerializeField] private string m_name;

    private void Update()
    {
        //if(m_enemy1.GetComponent<Rail_Enemy>().IsDead ==  true && m_enemy2.GetComponent<Rail_Enemy>().IsDead == true)
        //{
        //    m_self.Play(m_name);
        //}

        bool allEnemiesDead = true;
        foreach (GameObject obj in enemyList)
        {
            if (!obj.GetComponent<Rail_Enemy>().IsDead)
            {
                allEnemiesDead = false;
                break;
            }
        }

        // Si tous les ennemis sont morts, jouez l'animation
        if (allEnemiesDead)
        {
            m_self.Play(m_name);
        }
    }
}
