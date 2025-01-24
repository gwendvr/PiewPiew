using UnityEngine;

public class S_Weapon : MonoBehaviour
{
    public S_WeaponData data;
    [SerializeField]
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]
    private Rigidbody2D m_rb;

    public void Shot()
    {

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
