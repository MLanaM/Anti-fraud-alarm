using System;
using UnityEngine;

public class AlarmZone : MonoBehaviour
{
    public Action zoneEntered;
    public Action zoneEmpty;

    private int _buglarCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        _buglarCount++;

        if (_buglarCount > 1)
            return;

        zoneEntered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        _buglarCount = Mathf.Max(0, _buglarCount - 1);

        if (_buglarCount == 0)
            zoneEmpty?.Invoke();
    }
}
