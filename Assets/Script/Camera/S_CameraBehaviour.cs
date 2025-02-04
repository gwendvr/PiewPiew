using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class S_CameraBehaviour : MonoBehaviour
{
    private static S_CameraBehaviour Instance { get; set; }
    public static S_CameraBehaviour instance => Instance;

    [SerializeField]
    private Volume m_volume;
    [SerializeField]
    private float m_effectDuration;
    private bool isShooting;
    [Header("Chromatic Aberation")]
    private ChromaticAberration m_chromaticAberation;
    [Range(1, 1)]
    [SerializeField]
    private float m_AdditionnalChromaticAberation;
    [SerializeField]
    private float m_resetChromaticAberationSpeed;
    [SerializeField]
    private float m_chromaticAberationSpeed;

    [Header("Lens Distortion")]
    private LensDistortion m_lensDistortion;
    [Range(-1, 1)]
    [SerializeField]
    private float m_AdditionnalLensDistortion;
    [SerializeField]
    private float m_resetLensDistortionSpeed;
    [SerializeField]
    private float m_lensDistortionSpeed;

    private Coroutine m_coroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        LensDistortion _lens;
        ChromaticAberration _chroma;

        if (m_volume.profile.TryGet<LensDistortion>(out _lens))
        {
            m_lensDistortion = _lens;
        }
        if (m_volume.profile.TryGet<ChromaticAberration>(out _chroma))
        {
            m_chromaticAberation = _chroma;
        }
    }

    private void Update()
    {
        if (!isShooting && m_chromaticAberation != null && m_lensDistortion != null)
        {
            m_chromaticAberation.intensity.value = Mathf.Lerp(m_chromaticAberation.intensity.value, 0, m_resetChromaticAberationSpeed);
            m_lensDistortion.intensity.value = Mathf.Lerp(m_lensDistortion.intensity.value, 0, m_resetLensDistortionSpeed);
        }

        else if (isShooting && m_chromaticAberation != null && m_lensDistortion != null)
        {
            m_chromaticAberation.intensity.value = Mathf.Lerp(m_chromaticAberation.intensity.value, m_AdditionnalChromaticAberation, m_chromaticAberationSpeed);
            m_lensDistortion.intensity.value = Mathf.Lerp(m_lensDistortion.intensity.value, m_AdditionnalLensDistortion, m_lensDistortionSpeed);
        }
    }

    public void Shot()
    {
        isShooting = true;
        if(m_coroutine != null) StopCoroutine(m_coroutine);
        m_coroutine = StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(m_effectDuration);
        isShooting = false;
    }
}
