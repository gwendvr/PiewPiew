using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class S_PoolingSystem : MonoBehaviour
{
    private static S_PoolingSystem Instance { get; set; }
    public static S_PoolingSystem instance => Instance;

    private List<S_Bullet> m_availableBullet = new List<S_Bullet>();
    [SerializeField]
    private S_Bullet m_bullet;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public S_Bullet GetBullet()
    {
        if (m_availableBullet.Count > 0)
        {
            S_Bullet _bullet = m_availableBullet[0];
            m_availableBullet.RemoveAt(0);
            _bullet.gameObject.SetActive(true);
            _bullet.ResetBullet();
            return _bullet;
        }
        else
        {
            var _bullet = Instantiate(m_bullet);
            return _bullet;
        }
    }

    public void AddAvailableBullet(S_Bullet _bullet)
    {
        m_availableBullet.Add(_bullet);
        _bullet.StopTrail();
        _bullet.transform.parent = transform;
        _bullet.gameObject.SetActive(false);
    }
}
