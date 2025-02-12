using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/PlayerData", order = 1)]
public class S_PlayerData : ScriptableObject
{
    public float maxHealth;
    public float speed;
    public float rotationSpeed;
    public float dashCouldown;
    public float circulareCouldown;
}
