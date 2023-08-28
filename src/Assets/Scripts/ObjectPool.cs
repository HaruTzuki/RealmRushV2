using System;
using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField, Range(0f, 50f), Tooltip("Max Enemy Number")] private int poolSize = 5;
    [SerializeField, Range(0.1f, 30f), Tooltip("In Seconds")] private float spawnPace = 1f;

    private GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }


    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (var i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(EnemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnPace);
        }
    }

    private void EnableObjectInPool()
    {
        for (var i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeSelf)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
