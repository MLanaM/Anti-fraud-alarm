using System;
using UnityEngine;

public class AlarmZone : MonoBehaviour
{
    [SerializeField] private AlarmSound _alarmSound;

    private int _buglarCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        _buglarCount++;

        if (_alarmSound == null)
            return;

        _alarmSound.StartPlayAlarm();
    }
    private void OnTriggerExit()
    {
        _buglarCount--;

        if (_alarmSound == null || _buglarCount > 0)
            return;

        _alarmSound.StopPlayAlarm();
    }
}
