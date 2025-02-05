using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class S_EnemyManager : MonoBehaviour
{
    public static S_EnemyManager instance { get; private set; }

    private Dictionary<string, List<GameObject>> m_ennemiesByUniverse = new Dictionary<string, List<GameObject>>();
    public string m_activeUniverse = "Blue";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        StartCoroutine(Wait());
    }

    // Register enemies by universe to differentiate them
    public void RegisterEnemies(string _universe, List<GameObject> _enemies)
    {
        if (!m_ennemiesByUniverse.ContainsKey(_universe))
        {
            m_ennemiesByUniverse[_universe] = new List<GameObject>();
        }

        m_ennemiesByUniverse[_universe].AddRange(_enemies);
    }

    // Change enemies when we change universe
    public void SetActiveUniverse(string _universe)
    {
        if (m_activeUniverse == _universe) return;

        if (m_ennemiesByUniverse.ContainsKey(m_activeUniverse))
        {

            foreach (var _enemy in m_ennemiesByUniverse[m_activeUniverse])
            {
                _enemy.SetActive(false);
            }
        }

        if (m_ennemiesByUniverse.ContainsKey(_universe))
        {
            foreach (var _enemy in m_ennemiesByUniverse[_universe])
            {
                _enemy.SetActive(true);
            }
        }

        m_activeUniverse = _universe;
    }

    // Get enemy count by universe
    public int GetEnemyCount(string _universe)
    {
        if (!m_ennemiesByUniverse.ContainsKey(_universe))
        {
            return 0;
        }
        return m_ennemiesByUniverse[_universe].Count;
    }

    public void RemoveEnemyFromUniverse(string _universe, GameObject _enemy)
    {
        if (m_ennemiesByUniverse.ContainsKey(_universe))
        {
            m_ennemiesByUniverse[_universe].Remove(_enemy);
            Debug.Log("Ennemi supprimé du dictionnaire : " + _enemy.name);
        }
    }

    // Test universe change
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(5.0f);
        SetActiveUniverse("Violet");
    }
}
