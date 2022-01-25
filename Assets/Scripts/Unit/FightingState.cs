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
        _timeToAttack = 0.2f;
        _unit.transform.LookAt(_unit.Target.transform.position);
        _unit.Animator.SetTrigger(UnitAnimator.Fight);
    }

    public void Exit()
    {
        _unit.Animator.ResetTrigger(UnitAnimator.Fight);
    }

    public void FixedUpdate()
    {
        if (_timeToAttack <= 0)
        {
            _unit.HitTarget();
            _timeToAttack = _unit.AttackDuration;
        }
        _timeToAttack -= Time.deltaTime;
    }
}
