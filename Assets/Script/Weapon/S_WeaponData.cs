using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/WeaponData", order = 0)]
public class S_WeaponData : ScriptableObject
{
    public Sprite frontSprite;
    public Sprite topSprite;
    public float damage;
    public int nbBullet;
    public float angleMin;
    public float angleMax;
    public float range;
    public float bulletSpeed;
    public float throwStrenght;
}
