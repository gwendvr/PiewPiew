using UnityEngine;

public class S_Weapon : MonoBehaviour
{
    public S_WeaponData data;

    [Header("Components")]
    [SerializeField]
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]
    private Rigidbody2D m_rb;


    [Header("Bullet")]
    private float m_timeSinceLastShot;
    [SerializeField]
    private Transform m_bulletSpawn;
    [SerializeField]
    private S_BulletParticle particle;
    private int bulletShot;
    private S_PoolingSystem m_poolingSystem;

    private void Start()
    {
        m_poolingSystem = S_PoolingSystem.instance;
    }

    private void FixedUpdate()
    {
        m_timeSinceLastShot += Time.fixedDeltaTime;
    }

    public void Shot(float _rotZ)
    {
        if (m_timeSinceLastShot < data.shotCouldown) return; // If couldown isn't finished, don't shot
        if (bulletShot >= data.bulletInMagazine) return; // no more ammo
        int i = 0;
        S_CameraBehaviour.instance.Shot();

        while (i < data.bulletPerShot) // Shot all bullets needed
        {
            if (data.bulletInMagazine > bulletShot)
            {
                S_Bullet _bullet = S_PoolingSystem.instance.GetBullet();
                _bullet.transform.position = m_bulletSpawn.position;
                _bullet.transform.rotation = m_bulletSpawn.rotation;

                // Get random angle
                float _minAngle = _rotZ - (data.coneAngle / 2);
                float _maxAngle = _rotZ + (data.coneAngle / 2);
                float _angle = Random.Range(_minAngle, _maxAngle);
                Quaternion _rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, _angle);

                _bullet.transform.rotation = _rotation;
                _bullet.StartTrail();
                _bullet.Shot(data);
                i++;
            }


            else
            {
                i = data.bulletPerShot;
            }
        }
        bulletShot++;

        if (data.bulletInMagazine > bulletShot)
        {
            S_BulletParticle _particle = S_ParticlePoolingSystem.instance.GetParticle();
            _particle.transform.position = transform.position;
            _particle.transform.rotation = transform.rotation;
        }
        m_timeSinceLastShot = 0;
    }

    public void Taken()
    {
        m_rb.bodyType = RigidbodyType2D.Kinematic;
        m_spriteRenderer.sprite = data.topSprite;
        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
    }

    public void Throw(Vector2 _throwDirection)
    {
        m_rb.bodyType = RigidbodyType2D.Dynamic;
        transform.parent = null;
        m_spriteRenderer.sprite = data.frontSprite;
        m_rb.AddForce(_throwDirection * data.throwStrenght, ForceMode2D.Impulse);
    }

    public Rigidbody2D GetRigidBody()
    { return m_rb; }
}
