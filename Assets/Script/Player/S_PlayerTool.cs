using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_PlayerTool : MonoBehaviour
{
    [SerializeField]
    private S_PlayerData m_playerData;

    [Header("Slider")]
    [SerializeField] private Slider m_health;
    [SerializeField] private Slider m_speed;
    [SerializeField] private Slider m_rotSpeed;
    [SerializeField] private Slider m_dashCD;
    [SerializeField] private Slider m_circulareCD;
    [SerializeField] private float m_maxHealth;
    [SerializeField] private float m_maxSpeed;
    [SerializeField] private float m_maxRotSpeed;
    [SerializeField] private float m_maxDashCD;
    [SerializeField] private float m_maxCirculareCD;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI m_healthTxt;
    [SerializeField] private TextMeshProUGUI m_speedTxt;
    [SerializeField] private TextMeshProUGUI m_rotSpeedTxt;
    [SerializeField] private TextMeshProUGUI m_dashCDTxt;
    [SerializeField] private TextMeshProUGUI m_circulareCDTxt;

    [Header("Animation")]
    [SerializeField] private Animator m_animator;
    private bool m_openTool;

    private void Start()
    {
        m_health.value = m_playerData.maxHealth/m_maxHealth;
        m_speed.value = m_playerData.speed/m_maxSpeed;
        m_rotSpeed.value = m_playerData.rotationSpeed/m_maxRotSpeed;
        m_dashCD.value = m_playerData.dashCouldown/m_maxDashCD;
        m_circulareCD.value = m_playerData.circulareCouldown/m_maxCirculareCD;

        m_healthTxt.text = Mathf.Round(m_playerData.maxHealth).ToString();
        int _speed = (int)(m_playerData.speed * 100);
        float _speedRounded = _speed;
        _speedRounded = _speedRounded / 100;
        m_speedTxt.text = (_speedRounded).ToString();
        m_rotSpeedTxt.text = Mathf.Round(m_playerData.rotationSpeed).ToString();
        m_dashCDTxt.text = Mathf.Round(m_playerData.dashCouldown).ToString();
        m_circulareCDTxt.text = Mathf.Round(m_playerData.circulareCouldown).ToString();
    }

    #region Open Close Tool
    public void ToggleTool()
    {
        m_openTool = !m_openTool;
        if (m_openTool) OpenTool();
        else CloseTool();
    }

    private void OpenTool()
    {
        m_animator.SetTrigger("Open");
    }

    private void CloseTool()
    {
        m_animator.SetTrigger("Close");
    }
    #endregion

    #region Setter
    public void SetHealth()
    {
        m_playerData.maxHealth = m_health.value * m_maxHealth;
        m_healthTxt.text = Mathf.Round(m_health.value * m_maxHealth).ToString();
    }

    public void SetSpeed()
    {
        m_playerData.speed = m_speed.value * m_maxSpeed;
        int _speed = (int)(m_playerData.speed * 100);
        float _speedRounded = _speed;
        _speedRounded = _speedRounded / 100;
        m_speedTxt.text = (_speedRounded).ToString();
    }

    public void SetRotationSpeed()
    {
        m_playerData.rotationSpeed = m_rotSpeed.value * m_maxRotSpeed;
        m_rotSpeedTxt.text = Mathf.Round(m_rotSpeed.value * m_maxRotSpeed).ToString();
    }

    public void SetDashCouldown()
    {
        m_playerData.dashCouldown = Mathf.Round(m_dashCD.value * m_maxDashCD);
        m_dashCDTxt.text = Mathf.Round(m_dashCD.value * m_maxDashCD).ToString();
    }

    public void SetCirculareCouldown()
    {
        m_playerData.circulareCouldown = Mathf.Round(m_circulareCD.value * m_maxCirculareCD);
        m_circulareCDTxt.text = Mathf.Round(m_circulareCD.value * m_maxCirculareCD).ToString();
    }

    #endregion
}
