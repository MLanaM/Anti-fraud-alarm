using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AlarmSound : MonoBehaviour
{
    [SerializeField] private float _volumeChangeSpeed = 1.0f;
    [SerializeField] private AudioSource _audioSource;

    private float _volumeTarget;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.mute = true;
        _audioSource.volume = 0.0f;
    }

    private void Update()
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _volumeTarget, _volumeChangeSpeed * Time.deltaTime);

        if (_volumeTarget > 0.1f)
            _audioSource.mute = false;
        else if (_audioSource.volume <= 0.1f)
            _audioSource.mute = true;
    }

    public void StartPlayAlarm()
    {
        _volumeTarget = 1.0f;

        if (_audioSource.isPlaying)
            return;

        _audioSource.Play();
    }

    public void StopPlayAlarm() =>
        _volumeTarget = 0.0f;
}
