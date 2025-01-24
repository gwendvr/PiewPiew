using UnityEngine;

public class S_Weapon : MonoBehaviour
{
    public S_WeaponData data;
    [SerializeField]
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]
    private Rigidbody2D m_rb;
    private float m_timeSinceLastShot;
    [SerializeField]
    private Transform m_bulletSpawn;

    private void FixedUpdate()
    {
        m_timeSinceLastShot += Time.fixedDeltaTime;
    }

    public void Shot(float _rotZ)
    {
        if (m_timeSinceLastShot < data.shotCouldown) return; // If couldown isn't finished, don't shot
        int i = 0;

        while (i < data.nbBullet) // Shot all bullets needed
        {
            S_Bullet _bullet = S_PoolingSystem.instance.GetBullet();
            _bullet.transform.position = m_bulletSpawn.position;
            _bullet.transform.rotation = m_bulletSpawn.rotation;

            // Get random angle
            float _minAngle = _rotZ - (data.coneAngle / 2);
            float _maxAngle = _rotZ + (data.coneAngle / 2);
            float _angle = Random.Range(_minAngle, _maxAngle);
            print("rotation : " + _rotZ + " | min : " + _minAngle + " | max : " + _maxAngle + " | angle : " + _angle);
            Quaternion _rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, _angle);

            _bullet.transform.rotation = _rotation;
            _bullet.Shot(data);
            i++;
        }
    }

    public void Taken()
    {
        Debug.Log("Take");
        m_rb.bodyType = RigidbodyType2D.Kinematic;
        m_spriteRenderer.sprite = data.topSprite;
        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
    }

    public void Throw(Vector2 _throwDirection)
    {
        Debug.Log("Throw");
        m_rb.bodyType = RigidbodyType2D.Dynamic;
        transform.parent = null;
        m_spriteRenderer.sprite = data.frontSprite;
        m_rb.AddForce(_throwDirection * data.throwStrenght, ForceMode2D.Impulse);
    }

    public Rigidbody2D GetRigidBody()
    { return m_rb; }
}
