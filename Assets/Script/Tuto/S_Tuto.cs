using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class S_Tuto : MonoBehaviour
{
    public GameObject m_EnemieWave1;
    public GameObject[] m_EnemiesWave2;
    public GameObject[] m_EnemiesWave3;
    public int ennemiesAlive = 0;
    public int nbwave = 0;

    public S_TutoDoor door1;
    public S_TutoDoor door2;
    public S_TutoDoor door3;

    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;

    private S_EnemyManager _enemyManager;

    #region Singleton
    public static S_Tuto instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    void Start()
    {
        _enemyManager = S_EnemyManager.instance; 
        _enemyManager.SetActiveUniverse("Blue");
        _enemyManager.RegisterEnemies("Blue", new List<GameObject>());
        _enemyManager.RegisterEnemies("Violet", new List<GameObject>());
    }

    public void spawnFirstWave()
    {
        GameObject _spawnedEnemy = Instantiate(m_EnemieWave1, _enemyManager.spawnPoints[0].transform.position, Quaternion.identity);
        _spawnedEnemy.GetComponent<S_Enemy>().pathLinked = "None";
        _enemyManager.AddEnnemyToUniverse(_spawnedEnemy, "Blue");
        ennemiesAlive += 1;
        nbwave = 1;
        door1.openDoor();
    }

    public void spawnSecondWave()
    {
        GameObject _spawnedEnemy1 = Instantiate(m_EnemiesWave2[0], _enemyManager.spawnPoints[1].transform.position, Quaternion.identity);
        GameObject _spawnedEnemy2 = Instantiate(m_EnemiesWave2[1], _enemyManager.spawnPoints[2].transform.position, Quaternion.identity);
        GameObject _spawnedEnemy3 = Instantiate(m_EnemiesWave2[2], _enemyManager.spawnPoints[3].transform.position, Quaternion.identity);
        GameObject _spawnedEnemy4 = Instantiate(m_EnemiesWave2[3], _enemyManager.spawnPoints[4].transform.position, Quaternion.identity);
        GameObject _spawnedEnemy5 = Instantiate(m_EnemiesWave2[4], _enemyManager.spawnPoints[5].transform.position, Quaternion.identity);
        _spawnedEnemy1.GetComponent<S_Enemy>().pathLinked = "None";
        _spawnedEnemy2.GetComponent<S_Enemy>().pathLinked = "None";
        _spawnedEnemy3.GetComponent<S_Enemy>().pathLinked = "None";
        _spawnedEnemy4.GetComponent<S_Enemy>().pathLinked = "None";
        _spawnedEnemy5.GetComponent<S_Enemy>().pathLinked = "None";
        _enemyManager.AddEnnemyToUniverse(_spawnedEnemy1, "Blue");
        _enemyManager.AddEnnemyToUniverse(_spawnedEnemy2, "Blue");
        _enemyManager.AddEnnemyToUniverse(_spawnedEnemy3, "Blue");
        _enemyManager.AddEnnemyToUniverse(_spawnedEnemy4, "Blue");
        _enemyManager.AddEnnemyToUniverse(_spawnedEnemy5, "Blue");
        ennemiesAlive += 5;
        nbwave = 2;
        text2.SetActive(true);
    }

    public void spawnThirdWave()
    {
        GameObject _spawnedEnemy1 = Instantiate(m_EnemiesWave3[0], _enemyManager.spawnPoints[6].transform.position, Quaternion.identity);
        GameObject _spawnedEnemy2 = Instantiate(m_EnemiesWave3[1], _enemyManager.spawnPoints[7].transform.position, Quaternion.identity);
        GameObject _spawnedEnemy3 = Instantiate(m_EnemiesWave3[2], _enemyManager.spawnPoints[8].transform.position, Quaternion.identity);
        _spawnedEnemy1.GetComponent<S_Enemy>().pathLinked = "None";
        _spawnedEnemy2.GetComponent<S_Enemy>().pathLinked = "None";
        _spawnedEnemy3.GetComponent<S_Enemy>().pathLinked = "None";
        _enemyManager.AddEnnemyToUniverse(_spawnedEnemy1, "Violet");
        _enemyManager.AddEnnemyToUniverse(_spawnedEnemy2, "Blue");
        _enemyManager.AddEnnemyToUniverse(_spawnedEnemy3, "Violet");
        ennemiesAlive += 3;
        nbwave = 3;
        text3.SetActive(true);
    }

    public void EnnemyDead()
    {
        ennemiesAlive -= 1;
        if (ennemiesAlive <= 0)
        {
            if (nbwave == 1)
            {
                spawnSecondWave();
                door2.openDoor();
            }
            else if (nbwave == 3)
            {
                door2.openDoor();
                text4.SetActive(true);
            }
        }
    }

    public void EndTuto()
    {
        Debug.Log("End tuto");
        //SceneManager.LoadScene("");
    }
}
