using UnityEngine;
using UnityEngine.UI;

public class S_DimensionManager : MonoBehaviour
{
    private static S_DimensionManager Instance { get; set; }
    public static S_DimensionManager instance => Instance;
    [SerializeField]
    private GameObject m_dimensionFilter;
    [SerializeField]
    private GameObject m_dashIcon;
    [SerializeField]
    private GameObject m_circularAttackIcon;
    public bool isDimension1 = true;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void ChangeDimension()
    {
        isDimension1 = !isDimension1;
        S_AudioManager _audioManager = S_AudioManager.instance;

        if (isDimension1)
        {
            m_dashIcon.SetActive(true);
            m_circularAttackIcon.SetActive(false);
            m_dimensionFilter.SetActive(false);
            _audioManager.PlayAudioAtSecond("MainThemeDimension1", _audioManager.GetAudioTime("MainThemeDimension2"));
            _audioManager.StopAudio("MainThemeDimension2");
            m_dimensionFilter.SetActive(false);
        }
        else
        {
            m_dashIcon.SetActive(false);
            m_circularAttackIcon.SetActive(true);
            m_dimensionFilter.SetActive(true);
            _audioManager.PlayAudioAtSecond("MainThemeDimension2", _audioManager.GetAudioTime("MainThemeDimension1"));
            _audioManager.StopAudio("MainThemeDimension1");
            m_dimensionFilter.SetActive(true);
        }
    }
}
