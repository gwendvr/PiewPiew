using UnityEngine;

public class S_BulletParticle : MonoBehaviour
{
    private float m_timeAlive;
    // Update is called once per frame
    void Update()
    {
        m_timeAlive += Time.deltaTime;
        if(m_timeAlive >= 60)
        {
            S_ParticlePoolingSystem.instance.AddAvailableParticle(this);
        }
    }
}
