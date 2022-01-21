public class DiedState : IUnitState
{
    private Unit _unit;

    public DiedState(Unit unit)
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
