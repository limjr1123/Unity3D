using UnityEngine;

[CreateAssetMenu(menuName = "Combat System/Create a new Attack")]
public class AttackData : ScriptableObject
{
    [field:SerializeField] public string animName { get; private set; }
    [field:SerializeField] public AttackHitbox hitboxToUse { get; private set; }
    [field:SerializeField] public float impactStartTime { get; private set; }
    [field:SerializeField] public float impactEndTime { get; private set; }


}

// 공격 시 사용할 히트박스 종류를 정의하는 열거형입니다.
public enum AttackHitbox { LeftHand, RightHand, LeftFoot, RightFoot, Sword}