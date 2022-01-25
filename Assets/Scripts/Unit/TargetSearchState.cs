public class TargetSearchState : IUnitState
{
    private readonly Unit _unit;

    public TargetSearchState(Unit unit)
    {
        _unit = unit;
    }

    public void Enter()
    {
        _unit.SetTarget();
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
    }
}
