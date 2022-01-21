using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _hitDistance;

    private IReadOnlyList<Unit> _targets;
    private Unit _target;

    public float HitDistance => _hitDistance;
    public Animator Animator => _animator;
    public Unit Target => _target;
    public NavMeshAgent Agent => _agent;

    public event Action Waiting;
    public event Action TargetSearching;
    public event Action TargetAssigned;
    public event Action Fight;
    public event Action Died;

    public void Initialize(IReadOnlyList<Unit> units)
    {
        _targets = units;
    }

    private void OnDisable()
    {
        if (_target != null)
            _target.Died -= OnTargetDied;
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

    public void SetDie()
    {
        Died?.Invoke();
    }

    public void SetWaiting()
    {
        Waiting?.Invoke();
    }

    private Unit FindTarget()
    {
        Unit nearestTarget = null;
        float distanceToNearestTarget = float.MaxValue;

        for (int i = 0; i < _targets.Count; i++)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _targets[i].transform.position);
            if (distanceToTarget < distanceToNearestTarget)
            {
                nearestTarget = _targets[i];
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
