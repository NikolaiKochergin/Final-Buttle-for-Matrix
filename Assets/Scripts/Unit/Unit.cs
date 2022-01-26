using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator _jenkinsAnimator;
    [SerializeField] private Animator _neoAnimator;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private NavMeshObstacle _meshObstacle;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDuration;
    [SerializeField] private float _hitDistance;

    private int _currentHealth;
    private IReadOnlyList<Unit> _targetList;
    private Unit _target;
    private Animator _currentAnimator;

    public bool IsAlive { get; private set; }
    public float AttackDuration => _attackDuration;
    public float HitDistance => _hitDistance;
    public Animator Animator => _currentAnimator;
    public Unit Target => _target;
    public NavMeshAgent Agent => _agent;

    public event Action Waiting;
    public event Action TargetSearching;
    public event Action TargetAssigned;
    public event Action Fight;
    public event Action Died;

    public void Initialization(IReadOnlyList<Unit> enemies)
    {
        if (enemies == null)
            throw new ArgumentNullException("Unit is not initialized by enemies.");
        _currentHealth = _health;
        _targetList = enemies;
    }

    private void Awake()
    {
        SetNeoAvatar();
    }

    private void OnEnable()
    {
        IsAlive = true;
    }

    private void OnDisable()
    {
        IsAlive = false;
        _meshObstacle.enabled = true;
        if (_target != null)
            _target.Died -= OnTargetDied;
    }

    public void SetNeoAvatar()
    {
        _currentAnimator = _neoAnimator;
        _jenkinsAnimator.gameObject.SetActive(false);
        _neoAnimator.gameObject.SetActive(true);
    }

    public void SetJenkinsAvatar()
    {
        _currentAnimator = _jenkinsAnimator;
        _neoAnimator.gameObject.SetActive(false);
        _jenkinsAnimator.gameObject.SetActive(true);
    }

    public void Take(int damage)
    {
        _currentAnimator.SetTrigger(UnitAnimator.TakeDamage);
        if (damage < _currentHealth)
        {
            _currentHealth -= damage;
        }
        else
        {
            IsAlive = false;
            _meshObstacle.enabled = false;
            if (_target != null)
                _target.Died -= OnTargetDied;
            Died?.Invoke();
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
        if (_target == null)
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
        var distanceToNearestTarget = float.MaxValue;

        foreach (var target in _targetList)
        {
            if (target.IsAlive == false)
                continue;

            var distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget < distanceToNearestTarget)
            {
                nearestTarget = target;
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
}