using UnityEngine;

public class FightingState : IUnitState
{
    private readonly Unit _unit;
    private float _timeToAttack;

    public FightingState(Unit unit)
    {
        _unit = unit;
    }

    public void Enter()
    {
        _timeToAttack = _unit.AttackDuration;
        _unit.transform.LookAt(_unit.Target.transform.position);
        _unit.Animator.SetTrigger(UnitAnimator.Attack);
    }

    public void Exit()
    {
        _unit.Animator.ResetTrigger(UnitAnimator.Attack);
    }

    public void Update()
    {
        if (_timeToAttack <= 0)
        {
            _unit.HitTarget();
            _timeToAttack = _unit.AttackDuration;
        }
        _timeToAttack -= Time.deltaTime;
    }
}
