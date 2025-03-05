using UnityEngine;
using System.Collections.Generic;


public class S_Tuto : MonoBehaviour
{
    #region Singleton
    public static S_Tuto instance { get; private set; }

    public GameObject m_EnemieWave1 ;
    public List<GameObject> m_EnemiesWave2 = new List<GameObject>();
    public List<GameObject> m_EnemiesWave3 = new List<GameObject>();



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
        
    }

    void Update()
    {
        
    }
}
