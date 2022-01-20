using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachine : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    private Dictionary<Type, IUnitState> _statesMap;
    private IUnitState _currentState;

    private void Awake()
    {
        InitStates();
        SetStateByDefault();
    }

    private void OnEnable()
    {
        _unit.Waiting += SetWaitingState;
        _unit.TargetSearching += SetTargetSearchState;
        _unit.TargetAssigned += SetMovementState;
        _unit.Fight += SetFightingState;
    }

    private void OnDisable()
    {
        _unit.Waiting -= SetWaitingState;
        _unit.TargetSearching -= SetTargetSearchState;
        _unit.TargetAssigned -= SetMovementState;
        _unit.Fight -= SetFightingState;
    }

    private void InitStates()
    {
        _statesMap = new Dictionary<Type, IUnitState>();

        _statesMap[typeof(WaitingState)] = new WaitingState(_unit);
        _statesMap[typeof(TargetSearchState)] = new TargetSearchState(_unit);
        _statesMap[typeof(MovementState)] = new MovementState(_unit);
        _statesMap[typeof(FightingState)] = new FightingState(_unit);
    }

    public void SetWaitingState()
    {
        var state = GetState<WaitingState>();
        Set(state);
    }

    public void SetTargetSearchState()
    {
        var state = GetState<TargetSearchState>();
        Set(state);
    }

    public void SetMovementState()
    {
        var state = GetState<MovementState>();
        Set(state);
    }

    public void SetFightingState()
    {
        var state = GetState<FightingState>();
        Set(state);
    }

    private void SetStateByDefault()
    {
        SetWaitingState();
    }

    private void Set(IUnitState newState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = newState;
        _currentState.Enter();
    }

    private IUnitState GetState<T>() where T : IUnitState
    {
        var type = typeof(T);
        return _statesMap[type];
    }

    private void Update()
    {
        if (_currentState != null)
            _currentState.Update();
    }
}
