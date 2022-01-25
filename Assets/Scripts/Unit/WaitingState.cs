public class WaitingState : IUnitState
{
    private readonly Unit _unit;

    public WaitingState(Unit unit)
    {
        _unit = unit;
    }

    public void Enter()
    {
        _unit.Animator.SetTrigger(UnitAnimator.Taunt);
    }

    public void Exit()
    {
        _unit.Animator.ResetTrigger(UnitAnimator.Taunt);
    }

    public void FixedUpdate()
    {
    }
}
