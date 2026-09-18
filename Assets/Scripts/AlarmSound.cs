using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AlarmSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _audioSourceForMaxVolume;

    private float _volumeTarget;
    private float _volumeChangeSpeed = 0.3f;

    private float _maxVolume = 1.0f;
    private float _minVolume = 0.0f;

    private bool _canPlayAudioSourceForMaxVolume;
    private Coroutine _volumeChangeCoroutine;

    private void Awake()
    {
        ResetAll();
    }

    private void OnDisable()
    {
        if (_volumeChangeCoroutine == null)
            return;

        StopCoroutine(_volumeChangeCoroutine);
        _volumeChangeCoroutine = null;

        ResetAll();
    }

    private IEnumerator PlayAlarmSound()
    {
        while (enabled)
        {
            if (_audioSource.volume == _minVolume && _volumeTarget == _minVolume)
            {
                ResetAll();
                _volumeChangeCoroutine = null;

                yield break;
            }

            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _volumeTarget, _volumeChangeSpeed * Time.deltaTime);

            if (_audioSource.volume >= _maxVolume && _canPlayAudioSourceForMaxVolume)
            {
                _audioSourceForMaxVolume.mute = false;
                _audioSourceForMaxVolume.Play();

                _canPlayAudioSourceForMaxVolume = false;
            }

            yield return null;
        }
    }

    public void StartPlayAlarm()
    {
        if (_volumeChangeCoroutine != null)
        {
            StopCoroutine(_volumeChangeCoroutine);
            _volumeChangeCoroutine = null;
        }

        ResetAll();

        _audioSource.mute = false;
        _audioSource.Play();

        _volumeChangeCoroutine = StartCoroutine(PlayAlarmSound());
    }

    public void StopPlayAlarm() =>
        _volumeTarget = _minVolume;

    private void ResetAll()
    {
        _audioSource.mute = true;
        _audioSourceForMaxVolume.mute = true;

        _audioSource.Stop();
        _audioSourceForMaxVolume.Stop();

        _audioSource.volume = _minVolume;
        _volumeTarget = _maxVolume;
        _canPlayAudioSourceForMaxVolume = true;
    }
}
