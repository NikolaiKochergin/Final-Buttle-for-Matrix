using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Team _enemyTeam;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDuration;
    [SerializeField] private float _hitDistance;

    private int _currentHealth;
    private IReadOnlyList<Unit> _targetList;
    private Unit _target;

    public bool IsAlive { get; private set; }
    public float AttackDuration => _attackDuration;
    public float HitDistance => _hitDistance;
    public Animator Animator => _animator;
    public Unit Target => _target;
    public NavMeshAgent Agent => _agent;

    public event Action Waiting;
    public event Action TargetSearching;
    public event Action TargetAssigned;
    public event Action Fight;
    public event Action Died;

    private void Awake()
    {
        _currentHealth = _health;
        _targetList = _enemyTeam.Units;
    }

    private void OnEnable()
    {
        IsAlive = true;
    }

    private void OnDisable()
    {
        IsAlive = false;
        if (_target != null)
            _target.Died -= OnTargetDied;
    }

    public void Take(int damage)
    {
        if (damage < _currentHealth)
        {
            _currentHealth -= damage;
        }
        else
        {
            IsAlive = false;
            Died?.Invoke();

            _animator.SetTrigger(UnitAnimator.Die);
        }
    }

    public void HitTarget()
    {
        if (_target != null)
            _target.Take(_damage);
    }

    public void SetTarget()
    {
        _target = FindTarget();
        if(_target == null)
        {
            Waiting?.Invoke();
            return;
        }
        _target.Died += OnTargetDied;
        TargetAssigned?.Invoke();
    }

    public void StartFighting()
    {
        Fight?.Invoke();
    }

    public void StartBattle()
    {
        TargetSearching?.Invoke();
    }

    private Unit FindTarget()
    {
        Unit nearestTarget = null;
        float distanceToNearestTarget = float.MaxValue;

        for (int i = 0; i < _targetList.Count; i++)
        {
            if (_targetList[i].IsAlive == false)
                continue;

            float distanceToTarget = Vector3.Distance(transform.position, _targetList[i].transform.position);
            if (distanceToTarget < distanceToNearestTarget)
            {
                nearestTarget = _targetList[i];
                distanceToNearestTarget = distanceToTarget;
            }
        }
        return nearestTarget;
    }

    private void OnTargetDied()
    {
        _target.Died -= OnTargetDied;
        TargetSearching?.Invoke();
    }

    private void HandleDieAnimation()
    {
        gameObject.SetActive(false);
    }
}
