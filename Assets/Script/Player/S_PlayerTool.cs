using TMPro;
using UnityEngine;

public class S_PlayerTool : MonoBehaviour
{
    [SerializeField]
    private S_PlayerData m_playerData;

    [SerializeField] private TMPro.TextMeshPro m_healthTxt;
    [SerializeField] private TMPro.TextMeshPro m_speed;
    [SerializeField] private TMPro.TextMeshPro m_rotSpeed;
    [SerializeField] private TMPro.TextMeshPro m_dashCD;
    [SerializeField] private TMPro.TextMeshPro m_circulareCD;


    #region Setter
    public void SetHealth()
    {
        m_playerData.maxHealth = float.Parse(m_healthTxt.text);
    }

    public void SetSpeed()
    {
        m_playerData.speed = float.Parse(m_speed.text); ;
    }

    public void SetRotationSpeed()
    {
        m_playerData.rotationSpeed = float.Parse(m_rotSpeed.text); ;
    }

    public void SetDashCouldown()
    {
        m_playerData.dashCouldown = float.Parse(m_dashCD.text); ;
    }

    public void SetCirculareCouldown()
    {
        m_playerData.circulareCouldown = float.Parse(m_circulareCD.text); ;
    }

    #endregion
}
