using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawner : MonoBehaviour
{
    public GameObject[] blueUniverseEnemies;
    public GameObject[] violetUniverseEnemies;

    private GameObject[] spawnPoints;
    private List<GameObject> m_spawnedEnemiesBlue = new List<GameObject>();
    private List<GameObject> m_spawnedEnemiesViolet = new List<GameObject>();

    private void Start()
    {
        spawnPoints = S_EnemyManager.instance.spawnPoints;
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
            var _pos = _spawnPoint.GetComponent<S_Path>();
            GameObject _randomEnemyPrefab = blueUniverseEnemies[Random.Range(0, blueUniverseEnemies.Length)];
            GameObject _spawnedEnemy = Instantiate(_randomEnemyPrefab, _pos.paths[0].transform.position, _pos.paths[0].transform.rotation);
            _spawnedEnemy.SetActive(true);
            var enemy = _spawnedEnemy.GetComponent<S_Enemy>();
            enemy.pathLinked = _spawnPoint.name;
            m_spawnedEnemiesBlue.Add(_spawnedEnemy);
        }

        foreach (var _spawnPoint in spawnPoints)
        {
            var _pos = _spawnPoint.GetComponent<S_Path>();
            GameObject _randomEnemyPrefab = violetUniverseEnemies[Random.Range(0, violetUniverseEnemies.Length)];
            GameObject _spawnedEnemy = Instantiate(_randomEnemyPrefab, _pos.paths[0].transform.position, _pos.paths[0].transform.rotation);
            _spawnedEnemy.SetActive(false);
            var enemy = _spawnedEnemy.GetComponent<S_Enemy>();
            enemy.pathLinked = _spawnPoint.name;
            m_spawnedEnemiesViolet.Add(_spawnedEnemy);
        }

        S_EnemyManager.instance.RegisterEnemies("Blue", m_spawnedEnemiesBlue);
        S_EnemyManager.instance.RegisterEnemies("Violet", m_spawnedEnemiesViolet);
    }
}
