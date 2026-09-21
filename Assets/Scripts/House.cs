using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private AlarmZone _alarmZone;
    [SerializeField] private AlarmSound _alarmSound;

    private void OnEnable()
    {
        _alarmZone.ZoneEntered += OnZoneEntered;
        _alarmZone.ZoneEmpty += OnZoneEmpty;
    }

    private void OnDisable()
    {
        _alarmZone.ZoneEntered -= OnZoneEntered;
        _alarmZone.ZoneEmpty -= OnZoneEmpty;
    }

    private void OnZoneEntered() =>
        _alarmSound.PlayUp();

    private void OnZoneEmpty() =>
        _alarmSound.PlayDown();
}
