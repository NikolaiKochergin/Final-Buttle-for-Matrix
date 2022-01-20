using System;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDuration;
    [SerializeField] private float _hitDistance;

    private int _currentHealth;
    private Unit _target;

    public bool IsAlive { get; private set; }
    public float AttackDuration => _attackDuration;
    public float HitDistance => _hitDistance;
    public Unit Target => _target;
    public NavMeshAgent Agent => _agent;

    public event Action Died;
    public event Action TargetSearching;
    public event Action TargetAssigned;
    public event Action Fight;
    public event Action Waiting;

    private void Awake()
    {
        _currentHealth = _health;
    }

    private void OnEnable()
    {
        IsAlive = true;
    }

    private void OnDisable()
    {
        IsAlive = false;
    }

    public void TakeDamage(int damage)
    {
        if (damage < _currentHealth)
        {
            _currentHealth -= damage;
        }
        else
        {
            IsAlive = false;
            Died?.Invoke();

            gameObject.SetActive(false); // Для теста, потом удалить или включить метод обработки "умерания"(Анимация и тд)
        }
    }

    public void HitTarget()
    {
        if (_target != null)
            _target.TakeDamage(_damage);
        _particleSystem.Play();
    }

    public void Set(Unit target)
    {
        _target = target;
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

    private void OnTargetDied()
    {
        _target.Died -= OnTargetDied;
        TargetSearching?.Invoke();
    }
}
