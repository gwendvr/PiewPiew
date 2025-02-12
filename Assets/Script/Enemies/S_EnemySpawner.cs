using System.Collections.Generic;
using UnityEngine;

public class S_EnemySpawner : MonoBehaviour
{
    public GameObject[] blueUniverseEnemies;
    public GameObject[] violetUniverseEnemies;

    private GameObject[] spawnPoints;
    private List<GameObject> m_spawnedEnemiesBlue = new List<GameObject>();
    private List<GameObject> m_spawnedEnemiesViolet = new List<GameObject>();

    #region Unity Methods
    private void Start()
    {
        S_EnemyManager _enemyManager = S_EnemyManager.instance;
        spawnPoints = _enemyManager.spawnPoints;
        SpawnEnemies();
        _enemyManager.SetActiveUniverse("Blue");
    }
    #endregion

    #region Enemy Spawning
    public void SpawnEnemies()
    {
        if (spawnPoints.Length == 0 || blueUniverseEnemies.Length == 0 || violetUniverseEnemies.Length == 0)
        {
            Debug.LogError("spawn point is not defined in " + gameObject.name);
            return;
        }

        SpawnUniverseEnemies(blueUniverseEnemies, m_spawnedEnemiesBlue, true);
        SpawnUniverseEnemies(violetUniverseEnemies, m_spawnedEnemiesViolet, true);

        S_EnemyManager.instance.RegisterEnemies("Blue", m_spawnedEnemiesBlue);
        S_EnemyManager.instance.RegisterEnemies("Violet", m_spawnedEnemiesViolet);
    }

    private void SpawnUniverseEnemies(GameObject[] universeEnemies, List<GameObject> spawnedEnemies, bool isActive)
    {
        foreach (var _spawnPoint in spawnPoints)
        {
            var _pos = _spawnPoint.GetComponent<S_Path>();
            GameObject _randomEnemyPrefab = universeEnemies[Random.Range(0, universeEnemies.Length)];
            GameObject _spawnedEnemy = Instantiate(_randomEnemyPrefab, _pos.paths[0].transform.position, _pos.paths[0].transform.rotation);
            var enemy = _spawnedEnemy.GetComponent<S_Enemy>();
            enemy.pathLinked = _spawnPoint.name;
            spawnedEnemies.Add(_spawnedEnemy);
        }
    }
    #endregion
}
