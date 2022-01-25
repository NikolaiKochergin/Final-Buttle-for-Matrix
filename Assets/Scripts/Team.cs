using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;

    public IReadOnlyList<Unit> Units => _units;

#if UNITY_EDITOR
    private void OnValidate()
    {
        _units.Clear();
        _units.AddRange(GetComponentsInChildren<Unit>());
    }

    private void Reset()
    {
        _units.Clear();
        _units.AddRange(GetComponentsInChildren<Unit>());
    }
#endif

    public bool CheckLose()
    {
        foreach (var unit in _units)
        {
            if (unit.IsAlive)
                return false;
        }

        return true;
    }
}