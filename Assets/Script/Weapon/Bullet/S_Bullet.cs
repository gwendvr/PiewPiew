using UnityEngine;

public class S_Bullet : MonoBehaviour
{
    private S_WeaponData m_data;
    [SerializeField]
    private Rigidbody2D m_rb;
    private float m_timeAlive;
    [SerializeField]
    private TrailRenderer m_trail;

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
        S_PoolingSystem.instance.AddAvailableBullet(this);
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
