using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawner : MonoBehaviour
{
    public GameObject[] blueUniverseEnemies;
    public GameObject[] violetUniverseEnemies;
    public Transform[] spawnPoints;

    private List<GameObject> m_spawnedEnemiesBlue = new List<GameObject>();
    private List<GameObject> m_spawnedEnemiesViolet = new List<GameObject>();

    private void Start()
    {
        SpawnEnemies();
    }

    // Enemy spawner for all universe
    public void SpawnEnemies()
    {
        if (spawnPoints.Length == 0 || blueUniverseEnemies.Length == 0 || violetUniverseEnemies.Length == 0)
        {
            Debug.LogError("spawn point is not defined in " + gameObject.name);
            return;
        }

        foreach (var _spawnPoint in spawnPoints)
        {
            GameObject _randomEnemyPrefab = blueUniverseEnemies[Random.Range(0, blueUniverseEnemies.Length)];
            GameObject _spawnedEnemy = Instantiate(_randomEnemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
            _spawnedEnemy.SetActive(true);
            m_spawnedEnemiesBlue.Add(_spawnedEnemy);
        }

        foreach (var _spawnPoint in spawnPoints)
        {
            GameObject _randomEnemyPrefab = violetUniverseEnemies[Random.Range(0, violetUniverseEnemies.Length)];
            GameObject _spawnedEnemy = Instantiate(_randomEnemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
            _spawnedEnemy.SetActive(false);
            m_spawnedEnemiesViolet.Add(_spawnedEnemy);
        }

        S_EnemyManager.instance.RegisterEnemies("Blue", m_spawnedEnemiesBlue);
        S_EnemyManager.instance.RegisterEnemies("Violet", m_spawnedEnemiesViolet);
    }
}
