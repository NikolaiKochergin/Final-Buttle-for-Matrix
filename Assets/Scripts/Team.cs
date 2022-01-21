using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    [SerializeField] private Team _enemyTeam;
    [SerializeField] private List<Unit> _units;    

    public IReadOnlyList<Unit> Units => _units;

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

    public void StartBattle()
    {
        foreach (var unit in _units)
        {
            unit.Initialize(_enemyTeam.Units);
            unit.StartBattle();
        }
    }

    public void ExecuteTeam()
    {
        foreach(var unit in _units)
            unit.SetDie();

        foreach(var unit in _enemyTeam.Units)
            unit.SetWaiting();
    }
}
