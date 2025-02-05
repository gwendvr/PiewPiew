using UnityEngine;
using System.Collections.Generic;


public class S_AudioManager : MonoBehaviour
{

    private static S_AudioManager Instance { get; set; }
    public static S_AudioManager instance => Instance;

    [Header("Audio")]
    [SerializeField]
    private List<S_Audio> m_audios = new();

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayAudio("MainThemeDimension1");
    }

    #region Play audio

    public void PlayAudio(string _name)
    {
        foreach (S_Audio _audio in m_audios)
        {
            if (_audio.audioName == _name)
            {
                _audio.audioSource.Play();
            }
        }
    }

    public void PlayAudio(string _name, float _volume)
    {
        foreach (S_Audio _audio in m_audios)
        {
            if (_audio.audioName == _name)
            {
                _audio.audioSource.volume = _volume;
                _audio.audioSource.Play();
            }
        }
    }

    public void PlayAudio(string _name, float _volume, float _pitch)
    {
        foreach (S_Audio _audio in m_audios)
        {
            if (_audio.audioName == _name)
            {
                _audio.audioSource.volume = _volume;
                _audio.audioSource.pitch = _pitch;
                _audio.audioSource.Play();
            }
        }
    }

    public void PlayAudioAtSecond(string _name, float _time)
    {
        foreach (S_Audio _audio in m_audios)
        {
            if (_audio.audioName == _name)
            {
                _audio.audioSource.time = _time;
                _audio.audioSource.Play();
            }
        }
    }
    #endregion

    public void StopAudio(string _name)
    {
        foreach (S_Audio _audio in m_audios)
        {
            if (_audio.audioName == _name)
            {
                _audio.audioSource.Stop();
            }
        }
    }

    public void SwitchAudioClip(string _name1, string _name2)
    {
        AudioSource _tempAudioSource = new(), _audioSource1 = null, _audioSource2 = null;
        foreach (S_Audio _audio in m_audios)
        {
            if (_audio.audioName == _name1)
            {
                _tempAudioSource = _audio.audioSource;
                _audioSource1 = _audio.audioSource;
            }
        }
        if (_audioSource1 == null)
        {
            Debug.Log("/!\\No Audio Source Found With The Name \"" + _name1 + "\"/!\\"); // '_name1' doesn't exist
            return;
        }
        foreach (S_Audio _audio in m_audios)
        {
            if (_audio.audioName == _name2)
            {
                _audioSource1.clip = _audio.audioSource.clip;
            }
        }

        _audioSource2.clip = _tempAudioSource.clip;
    }

    public float GetAudioTime(string _name)
    {
        foreach (S_Audio _audio in m_audios)
        {
            if (_audio.audioName == _name)
            {
                return _audio.audioSource.time;
            }
        }
        return 0;
    }

}
