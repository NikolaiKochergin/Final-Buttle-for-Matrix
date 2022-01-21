public class FightingState : IUnitState
{
    private Unit _unit;

    public FightingState(Unit unit)
    {
        _unit = unit;
    }

    public void Enter()
    {
        _unit.Animator.SetTrigger(UnitAnimator.Attack);
    }

    public void Exit()
    {
        _unit.Animator.ResetTrigger(UnitAnimator.Attack);
    }

    public void Update()
    {
    }
}
