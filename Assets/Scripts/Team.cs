using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Team : MonoBehaviour
{
    [SerializeField] private Text _winText;
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

    private void Update()
    {
        if (CheckWin())
            _winText.gameObject.SetActive(true);

    }

    public void StartBattle()
    {
        foreach (var unit in _units)
            unit.StartBattle();
    }

    private bool CheckWin()
    {
        foreach(var unit in _enemyTeam.Units)
        {
            if(unit.IsAlive == true)
                return false;
        }
        return true;
    }
}
