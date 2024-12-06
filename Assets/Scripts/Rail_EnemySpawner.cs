using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Rail_EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rail_Enemy m_enemyToSpawn;
    [SerializeField] private SplineContainer m_splineContainer;

    [Header("Values")]
    [SerializeField] private int m_maxEnemyToSpawn = 10;
    [SerializeField] private float m_SpawnInterval = 0.5f;

    private List<Rail_Enemy> m_spawnedEnemies = new List<Rail_Enemy>();
    private Coroutine m_spawnCoroutine;

    private void OnEnable()
    {
        m_spawnCoroutine = StartCoroutine(C_Spawn());
    }

    private IEnumerator C_Spawn()
    {
        float timer = 0;
        float duration = m_SpawnInterval;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        SpawnEnemy();

        if (m_spawnedEnemies.Count < m_maxEnemyToSpawn) 
        {
            m_spawnCoroutine = StartCoroutine(C_Spawn());
        }
    }

    private void SpawnEnemy()
    {
        Rail_Enemy newEnemy = Instantiate(m_enemyToSpawn);
        newEnemy.transform.position = transform.position;
        m_spawnedEnemies.Add(newEnemy);
        newEnemy.Init(m_splineContainer);
    }
}
