using System;
using UnityEngine;

public class AlarmZone : MonoBehaviour
{
    private int _burglarCount = 0;

    public event Action ZoneEntered;
    public event Action ZoneEmpty;

    private void OnTriggerEnter(Collider other)
    {
        _burglarCount++;

        if (_burglarCount > 1)
            return;

        ZoneEntered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        _burglarCount = Mathf.Max(0, _burglarCount - 1);

        if (_burglarCount == 0)
            ZoneEmpty?.Invoke();
    }
}
