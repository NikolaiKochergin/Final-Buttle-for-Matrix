using System.Collections.Generic;
using UnityEngine;

public class TargetSearchState : IUnitState
{
    private readonly Unit _unit;
    private IReadOnlyList<Unit> _targets;

    public TargetSearchState(Unit unit, IReadOnlyList<Unit> targets)
    {
        _unit = unit;
        _targets = targets;
    }

    public void Enter()
    {
        Unit target = FindTarget();
        _unit.Set(target);
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }

    private Unit FindTarget()
    {
        Unit nearestTarget = null;
        float distanceToNearestTarget = float.MaxValue;

        for (int i = 0; i < _targets.Count; i++)
        {
            if(_targets[i].IsAlive == false)
                continue;

            float distanceToTarget = Vector3.Distance(_unit.transform.position, _targets[i].transform.position);
            if (distanceToTarget < distanceToNearestTarget)
            {
                nearestTarget = _targets[i];
                distanceToNearestTarget = distanceToTarget;
            }
        }
        return nearestTarget;
    }
}
