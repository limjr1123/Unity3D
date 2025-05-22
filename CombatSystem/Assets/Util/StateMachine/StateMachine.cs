using NUnit.Framework;
using UnityEngine;


//제네릭 사용
public class StateMachine<T>
{
    public State<T> CurrentState { get; private set; }

    T _owner;

    // 상태 머신 생성자. 소유자 객체를 전달받아 저장합니다.
    public StateMachine(T owner)
    {
        _owner = owner;
    }

    // 상태를 변경합니다.
    public void ChangeState(State<T> newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter(_owner);
    }

    public void Execute()
    {
        CurrentState?.Execute();
    }

}


