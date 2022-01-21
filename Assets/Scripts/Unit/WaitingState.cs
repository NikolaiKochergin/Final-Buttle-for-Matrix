using UnityEngine;

public class WaitingState : IUnitState
{
    private Unit _unit;

    public WaitingState(Unit unit)
    {
        _unit = unit;
    }

    public void Enter()
    {
        _unit.Animator.SetTrigger(UnitAnimator.Idle);
    }

    public void Exit()
    {
        _unit.Animator.ResetTrigger(UnitAnimator.Idle);
    }

    public void Update()
    {
    }
}
