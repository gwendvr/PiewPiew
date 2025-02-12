using UnityEngine;

public class S_Bullet : MonoBehaviour
{
    private S_WeaponData m_data;
    [SerializeField]
    private Rigidbody2D m_rb;
    private float m_timeAlive;
    [SerializeField]
    private TrailRenderer m_trail;
    [SerializeField]
    private bool m_isEnemyBullet;
    private void FixedUpdate()
    {
        m_timeAlive += Time.fixedDeltaTime;
        if(m_timeAlive >= m_data.range)
        {
            S_PoolingSystem.instance.AddAvailableBullet(this);
        }
    }

    public void Shot(S_WeaponData _data)
    {
        m_data = _data;
        m_rb.AddForce(transform.up * m_data.bulletSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_isEnemyBullet && collision.collider.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<S_PlayerController>().RemoveHealth(m_data.damage);
            S_PoolingSystem.instance.AddAvailableEnemyBullet(this);
        }
        else if(collision.collider.CompareTag("Ennemy"))
        {
            collision.collider.gameObject.GetComponent<S_Enemy>().health -= m_data.damage;
            S_PoolingSystem.instance.AddAvailableBullet(this);
        }
        else if (m_isEnemyBullet)
        {
            S_PoolingSystem.instance.AddAvailableEnemyBullet(this);
        }
        else
        {
            S_PoolingSystem.instance.AddAvailableBullet(this);
        }
    }

    public void ResetBullet()
    {
        m_data = null;
        m_timeAlive = 0f;
        transform.parent = null;
    }

    #region Trail

    public void StopTrail()
    {
        m_trail.emitting = false;
    }

    public void StartTrail()
    {
        m_trail.emitting = true;
    }
    #endregion
}
