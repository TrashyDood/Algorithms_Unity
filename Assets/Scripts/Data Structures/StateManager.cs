using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class CallbackStateManager : StateManager<CallbackState> { }

[Serializable]
public class StateManager : StateManager<IState> { }

[Serializable]
public class StateManager<T> where T : IState
{
    [SerializeField]
    List<T> states = new();

    T currentState;
    int currentStateIndex;

    public T CurrentState => currentState;

    public int CurrentStateIndex
    {
        get => CurrentStateIndex;
        set => SetState(value);
    }

    public T this[int stateIndex] =>
        GetState(stateIndex);

    public T this[Enum stateEnum] =>
        GetState(stateEnum);

    public void Initialize(Enum startEnum) =>
        Initialize(Convert.ToInt32(startEnum));

    public void Initialize(int startIndex = 0)
    {
        foreach (var state in states)
            state.Initialize();

        SetState(startIndex);
    }

    public void AddStates(IEnumerable<T> states) =>
        AddState(states.ToArray());

    public void AddState(params T[] states)
    {
        foreach (var state in states)
        {
            this.states.Add(state);
            state.Initialize();
        }
    }

    public bool SetState(Enum stateEnum) =>
        SetState(Convert.ToInt32(stateEnum));

    public bool SetState(int stateIndex)
    {
        if (stateIndex > states.Count - 1 || stateIndex < 0)
            return false;

        currentStateIndex = stateIndex;

        currentState?.OnExit();
        currentState = states[stateIndex];
        currentState.OnEnter();

        return true;
    }

    public T GetState(Enum stateEnum) =>
        GetState(Convert.ToInt32(stateEnum));

    public T GetState(int stateIndex)
    {
        if (stateIndex > states.Count - 1 || stateIndex < 0)
            return default;

        return states[stateIndex];
    }

    public void Evaluate(float deltaTime)
    {
        currentState.OnEvaluate(deltaTime);
    }
}

public interface IState
{
    public void Initialize();
    public void OnEnter();
    public void OnEvaluate(float deltaTime);
    public void OnExit();
}

[Serializable]
public class CallbackState : IState
{
    [SerializeField]
    UnityEvent onInitializedEvent,
        onEnterEvent,
        onExitEvent;

    public Action onInitializedCallback,
        onEnterCallback,
        onExitCallback;

    public void Initialize()
    {
        onInitializedCallback?.Invoke();
        onInitializedEvent.Invoke();
    }

    public void OnEnter()
    {
        onEnterCallback?.Invoke();
        onEnterEvent.Invoke();
    }

    public void OnEvaluate(float deltaTime) { }

    public void OnExit()
    {
        onExitCallback?.Invoke();
        onExitEvent.Invoke();
    }
}
