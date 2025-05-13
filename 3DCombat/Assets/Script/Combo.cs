using UnityEngine;

public class Combo : MonoBehaviour
{
    Animator playerAnim;        // 플레이어 애니메이션
    bool comboPossible;         // 콤보 가능 여부
    public int comboStep;       // 콤보 단계
    bool inputSmash;            // 스매시 입력 여부

    public GameObject hitBox;   // 히트박스

    void Start()
    {
        playerAnim = GetComponent<Animator>(); // 애니메이션 컴포넌트 가져오기 
    }

    public void ComboPossible()
    {
        comboPossible = true;
    }
   

    public void NextAtk()
    {
        if (!inputSmash)
        {
            if (comboStep == 2)
            {
                //기본공격
                playerAnim.Play("ARPG_Samurai_Attack_Combo3"); // 일반 공격 애니메이션 재생
            }
            if (comboStep == 3)
            {
                playerAnim.Play("ARPG_Samurai_Attack_Combo4"); // 일반 공격 애니메이션 재생
            }
        }

        if (inputSmash)
        {
            if (comboStep == 1)
            {

            }
            if (comboStep == 2)
            {
            }
            if (comboStep == 3)
            {
            }
        }
    }

    public void ResetCombo()
    {
        comboStep = 0;
        comboPossible = false;
        inputSmash = false;
    }

    void NormalAttack()
    {
        if (comboStep == 0)
        {
            playerAnim.Play("ARPG_Samurai_Attack_Combo2"); // 일반 공격 애니메이션 재생
            comboStep = 1; // 콤보 단계 1
            return;
        }

        if (comboStep != 0)
        {
            if (comboPossible)
            {
                comboPossible = false; // 콤보 불가능
                comboStep += 1; // 콤보 단계 증가
            }
        }
    }

    void SmashAttack()
    {
        if (comboPossible)
        {
            comboPossible = false;
            inputSmash = true;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            NormalAttack(); // 일반 공격
        }

        if (Input.GetMouseButtonDown(1)) // 마우스 오른쪽 버튼 클릭
        {
            SmashAttack(); // 스매시 공격
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //방어
        }
    }

    void ChageTag(string t)
    {
        hitBox.tag = t; // 히트박스 태그 변경
    }
}

