using UnityEngine;
using System.Collections.Generic;


public class S_ParticlePoolingSystem : MonoBehaviour
{
    private static S_ParticlePoolingSystem Instance { get; set; }
    public static S_ParticlePoolingSystem instance => Instance;

    private List<S_BulletParticle> m_availableParticle = new List<S_BulletParticle>();
    [SerializeField]
    private S_BulletParticle m_particle;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public S_BulletParticle GetParticle()
    {
        if (m_availableParticle.Count > 0)
        {
            S_BulletParticle _particle = m_availableParticle[0];
            m_availableParticle.RemoveAt(0);
            _particle.gameObject.SetActive(true);
            return _particle;
        }
        else
        {
            var _particle = Instantiate(m_particle);
            return _particle;
        }
    }
    public void AddAvailableParticle(S_BulletParticle _particle)
    {
        m_availableParticle.Add(_particle);
        _particle.transform.parent = transform;
        _particle.gameObject.SetActive(false);
    }
}
