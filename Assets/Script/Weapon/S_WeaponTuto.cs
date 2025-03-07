using UnityEngine;

public class S_WeaponTuto : S_Weapon
{
    public override void Taken()
    {
        m_rb.bodyType = RigidbodyType2D.Kinematic;
        m_spriteRenderer.sprite = data.topSprite;
        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
        S_Tuto.instance.spawnFirstWave();
        S_Tuto.instance.text1.SetActive(true);
    }

}
