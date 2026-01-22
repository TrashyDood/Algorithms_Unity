using System;
using UnityEngine;
using UnityEngine.Events;

public class XPManager : MonoBehaviour
{
    [SerializeField]
    AnimationCurve _XPReqScaling;
    [SerializeField]
    UnityEvent<int, int> _onXPGained,
        _onLevelGained;

    public event Action<int, int> OnXPGainedEvent,
        OnLevelGainedEvent;

    int _currentXP,
        _currentLevel,
        _currentXPReq;

    public int CurrentXP
    {
        get => _currentXP;
        set => AddXP(Mathf.Max(0, value));
    }

    public int CurrentLevel => _currentLevel;

    public void AddXP(int value)
    {

    }
}
