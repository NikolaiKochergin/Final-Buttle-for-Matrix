using UnityEngine;

public class MovementState : IUnitState
{
    private Unit _unit;

    public MovementState(Unit unit)
    {
        _unit = unit;
    }

    public void Enter()
    {
        _unit.Agent.enabled = true;
    }

    public void Exit()
    {
        _unit.Agent.enabled = false;
    }

    public void Update()
    {
        _unit.Agent.SetDestination(_unit.Target.transform.position);
        if (Vector3.Distance(_unit.transform.position, _unit.Target.transform.position) < _unit.HitDistance)
            _unit.StartFighting();
    }
}
