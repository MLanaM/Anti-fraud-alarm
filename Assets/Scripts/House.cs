using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private AlarmZone _alarmZone;
    [SerializeField] private AlarmSound _alarmSound;

    private void OnEnable()
    {
        _alarmZone.zoneEntered += OnZoneEntered;
        _alarmZone.zoneEmpty += OnZoneEmpty;
    }

    private void OnDisable()
    {
        _alarmZone.zoneEntered -= OnZoneEntered;
        _alarmZone.zoneEmpty -= OnZoneEmpty;
    }

    private void OnZoneEntered() =>
        _alarmSound.StartPlayAlarm();

    private void OnZoneEmpty() =>
        _alarmSound.StopPlayAlarm();
}
