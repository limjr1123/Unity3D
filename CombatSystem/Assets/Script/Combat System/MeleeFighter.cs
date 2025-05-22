using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AttackState 열거 형 데이터로 정의
public enum EnumAttackState
{
    Idle,      // 대기 상태
    Windup,    // 공격 준비(선딜레이)
    Impact,    // 타격 판정 구간
    Cooldown   // 후딜레이
}

/// <summary>
/// 근접 전투 캐릭터의 공격 동작을 제어하는 컴포넌트입니다.
/// 애니메이터를 통해 공격 애니메이션을 재생하고, 공격 중에는 추가 입력을 막습니다.
/// </summary>
public class MeleeFighter : MonoBehaviour
{
    // 공격 애니메이션과 관련된 데이터
    [SerializeField] List<AttackData> attacks;
    [SerializeField] GameObject sword;

    // 공격에 사용할 콜라이더들
    BoxCollider swordCollider;
    SphereCollider leftHandCollider, rightHandCollider, leftFootCollider, rightFootCollider;

    // 캐릭터의 애니메이터 컴포넌트
    Animator animator;

    // 현재 공격 동작(액션) 중인지 여부를 나타냅니다.
    public bool inAction { get; set; } = false;

    public EnumAttackState attackState;


    private void Awake()
    {
        // 컴포넌트가 활성화될 때 애니메이터를 초기화합니다.
        animator = GetComponent<Animator>();
    }


    private void Start()
    {
        if (sword != null)
        {
            swordCollider = sword.GetComponent<BoxCollider>();
            leftHandCollider = animator.GetBoneTransform(HumanBodyBones.LeftHand).GetComponent<SphereCollider>();
            rightHandCollider = animator.GetBoneTransform(HumanBodyBones.RightHand).GetComponent<SphereCollider>();
            leftFootCollider = animator.GetBoneTransform(HumanBodyBones.LeftFoot).GetComponent<SphereCollider>();
            rightFootCollider = animator.GetBoneTransform(HumanBodyBones.RightFoot).GetComponent<SphereCollider>();
            DisableAllCollider();
        }
    }

    public EnumAttackState eumAttackState { get; private set; }
    bool doCombo;
    int comboCounter = 0;

    // 공격 중이 아닐 때만 Attack 코루틴을 시작합니다.
    public void TryToAttack()
    {
        if (!inAction)
        {
            StartCoroutine(Attack());
        }
        else if (attackState == EnumAttackState.Impact || attackState == EnumAttackState.Cooldown)
        {
            doCombo = true;
        }
    }

    // 공격 애니메이션을 재생하고, 애니메이션이 끝날 때까지 대기합니다.
    IEnumerator Attack()
    {
        inAction = true;
        attackState = EnumAttackState.Windup;

        // animator의 Attack 애니메이션을 재생합니다. 블랜딩이 필요할 경우사용합니다.
        // 이전 동작에서 변경되는 애니메이션으로 부드럽게 전환합니다.
        animator.CrossFade(attacks[comboCounter].animName, 0.2f);
        yield return null;  // 프레임 대기하여 애니메이션 정보를 확인

        //GetNextAnimatorStateInfo 애니매이션 상태 정보를 가져옵니다.
        var animState = animator.GetNextAnimatorStateInfo(1);

        float timer = 0f;
        while (timer <= animState.length)
        {
            // normalizedTime을 스킬 실행 시간 백분율로 사용합니다.
            // attacks[comboCounter].impactStartTime은 백분율로 표현됩니다.
            timer += Time.deltaTime;
            float normalizedTime = timer / animState.length;

            if (attackState == EnumAttackState.Windup)
            {
                if (normalizedTime >= attacks[comboCounter].impactStartTime)
                {
                    attackState = EnumAttackState.Impact;
                    //콜라이더 켜기
                    EnableHitbox(attacks[comboCounter]);
                }
            }
            else if (attackState == EnumAttackState.Impact)
            {
                if (normalizedTime >= attacks[comboCounter].impactEndTime)
                {
                    attackState = EnumAttackState.Cooldown;
                    //콜라이더 끄기
                    DisableAllCollider();
                }
            }
            else if (attackState == EnumAttackState.Cooldown)
            {
                if (doCombo)
                {
                    doCombo = false;
                    comboCounter = (comboCounter + 1) % attacks.Count;
                    StartCoroutine(Attack());
                    yield break;
                }
            }

            yield return null;
        }
        attackState = EnumAttackState.Idle;
        comboCounter = 0;
        inAction = false;
    }

    void EnableHitbox(AttackData attack)
    {
        switch (attack.hitboxToUse)
        {
            case AttackHitbox.LeftHand:
                leftHandCollider.enabled = true;
                break;
            case AttackHitbox.RightHand:
                rightHandCollider.enabled = true;
                break;
            case AttackHitbox.LeftFoot:
                leftFootCollider.enabled = true;
                break;
            case AttackHitbox.RightFoot:
                rightFootCollider.enabled = true;
                break;
            case AttackHitbox.Sword:
                swordCollider.enabled = true;
                break;
            default:
                break;
        }
    }

    void DisableAllCollider()
    {
        // 초기에는 콜라이더를 비활성화합니다.
        swordCollider.enabled = false;
        if (leftFootCollider != null)
            leftHandCollider.enabled = false;
        if (rightFootCollider != null)
            rightHandCollider.enabled = false;
        if (leftFootCollider != null)
            leftFootCollider.enabled = false;
        if (rightFootCollider != null)
            rightFootCollider.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HitBox") && !inAction)
        {
            StartCoroutine(PlayerHitReaction());
        }
    }

    // 공격 애니메이션을 재생하고, 애니메이션이 끝날 때까지 대기합니다.
    IEnumerator PlayerHitReaction()
    {
        inAction = true;
        animator.CrossFade("SwordImpact", 0.2f);
        yield return null;  // 프레임 대기하여 애니메이션 정보를 확인

        //GetNextAnimatorClipInfo는 애니메이션이 끝나는 시간을 알 수 있습니다.
        var animState = animator.GetNextAnimatorStateInfo(1);
        yield return new WaitForSeconds(animState.length * 0.8f);
        inAction = false;
    }

    public List<AttackData> Attacks => attacks;
}
