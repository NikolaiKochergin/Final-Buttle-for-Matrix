using UnityEngine;

public class MovementState : IUnitState
{
    private readonly Unit _unit;

    public MovementState(Unit unit)
    {
        _unit = unit;
    }

    public void Enter()
    {
        _unit.transform.LookAt(_unit.Target.transform.position);
        _unit.Agent.enabled = true;
        _unit.Animator.SetTrigger(UnitAnimator.Run);
    }

    public void Exit()
    {
        _unit.Agent.enabled = false;
        _unit.Animator.ResetTrigger(UnitAnimator.Run);
    }

    public void Update()
    {
        _unit.Agent.SetDestination(_unit.Target.transform.position);
        if (Vector3.Distance(_unit.transform.position, _unit.Target.transform.position) < _unit.HitDistance)
            _unit.StartFighting();
    }
}
