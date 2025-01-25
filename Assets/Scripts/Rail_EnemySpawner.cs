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
    [SerializeField] private float m_delay;

    private List<Rail_Enemy> m_spawnedEnemies = new List<Rail_Enemy>();
    private Coroutine m_spawnCoroutine;
    private bool m_ready = false;

    private void Start()
    {
        StartCoroutine(C_Start());
    }

    private IEnumerator C_Start()
    {
        yield return new WaitForSeconds(m_delay);
        m_ready = true;

    }

    private void Update()
    {   
        if (m_ready == true)
            m_spawnCoroutine = StartCoroutine(C_Spawn());
    }

    private IEnumerator C_Spawn()
    {
        m_ready = false;
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
