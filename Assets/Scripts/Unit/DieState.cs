public class DieState : IUnitState
{
    private readonly Unit _unit;

    public DieState(Unit unit)
    {
        _unit = unit;
    }

    public void Enter()
    {
        _unit.Animator.SetTrigger(UnitAnimator.Die);
    }

    public void Exit()
    {
        _unit.Animator.ResetTrigger(UnitAnimator.Die);
    }

    public void Update()
    {
    }
}