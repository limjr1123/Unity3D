using UnityEngine;


//<T> 상태를 제네릭으로 사용하여 다양한 타입의 오너를 가질 수 있는 상태 클래스입니다.
public class State<T> : MonoBehaviour
{
    public virtual void Enter(T owner)
    {

    }

    public virtual void Execute()
    {

    }
    public virtual void Exit()
    {

    }
}
