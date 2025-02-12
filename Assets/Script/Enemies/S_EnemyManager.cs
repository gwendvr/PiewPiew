using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class S_EnemyManager : MonoBehaviour
{
    public string activeUniverse = "Blue";
    public GameObject player;
    public GameObject[] spawnPoints;

    private Dictionary<string, List<GameObject>> m_ennemiesByUniverse = new Dictionary<string, List<GameObject>>();

    #region Singleton
    public static S_EnemyManager instance { get; private set; }

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

    // Register enemies by universe to differentiate them
    #region Enemy Management
    public void RegisterEnemies(string _universe, List<GameObject> _enemies)
    {
        if (!m_ennemiesByUniverse.ContainsKey(_universe))
        {
            m_ennemiesByUniverse[_universe] = new List<GameObject>();
        }

        m_ennemiesByUniverse[_universe].AddRange(_enemies);
    }

    // Remove enemies detsroyed on dictionary
    public void RemoveEnemyFromUniverse(string _universe, GameObject _enemy)
    {
        if (m_ennemiesByUniverse.ContainsKey(_universe))
        {
            m_ennemiesByUniverse[_universe].Remove(_enemy);
            Debug.Log("Ennemi supprimé du dictionnaire : " + _enemy.name);
        }
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
    #endregion

    // Change enemies when we change universe
    #region Universe Switching
    public void SetActiveUniverse(string _universe)
    {
        if (activeUniverse == _universe) return;

        if (m_ennemiesByUniverse.ContainsKey(activeUniverse))
        {
            foreach (var _enemy in m_ennemiesByUniverse[activeUniverse])
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

        activeUniverse = _universe;
    }
    #endregion
}
