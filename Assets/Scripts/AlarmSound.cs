using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AlarmSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _audioSourceForMaxVolume;

    private Coroutine _activeRoutine;

    private float _speed = 0.3f;
    private float _maxVolume = 1.0f;
    private float _minVolume = 0.0f;

    private void OnDisable()
    {
        TryStop();
    }

    public void PlayUp()
    {
        TryStop();
        _activeRoutine = StartCoroutine(DoUp());
    }

    public void PlayDown()
    {
        TryStop();
        _activeRoutine = StartCoroutine(DoDown());
    }

    private IEnumerator DoUp()
    {
        _audioSource.mute = false;

        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        yield return ChangeVolumeTo(_maxVolume, _speed, _audioSource);

        _audioSourceForMaxVolume.mute = false;
        _audioSourceForMaxVolume.Play();
    }

    private IEnumerator DoDown()
    {
        _audioSourceForMaxVolume.mute = true;
        _audioSourceForMaxVolume.Stop();

        yield return ChangeVolumeTo(_minVolume, _speed, _audioSource);

        _audioSource.mute = true;
        _audioSource.Stop();
    }

    private IEnumerator ChangeVolumeTo(float target, float speed, AudioSource source)
    {
        while (!Mathf.Approximately(source.volume, target))
        {
            source.volume = Mathf.MoveTowards(source.volume, target, speed * Time.deltaTime);

            yield return null;
        }
    }

    private void TryStop()
    {
        if (_activeRoutine == null)
            return;

        StopCoroutine(_activeRoutine);
        _activeRoutine = null;
    }
}
