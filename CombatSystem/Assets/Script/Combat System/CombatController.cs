using UnityEngine;

// 플레이어의 전투 입력을 처리하는 컨트롤러입니다.
public class CombatController : MonoBehaviour
{
    // MeleeFighter 컴포넌트 참조
    MeleeFighter meleeFighter;

    private void Awake()
    {
        // MeleeFighter 컴포넌트 가져오기
        meleeFighter = GetComponent<MeleeFighter>();
    }

    private void Update()
    {
        // 마우스 왼쪽 버튼 클릭 시 공격 시도
        if (Input.GetMouseButtonDown(0))
        {
            meleeFighter.TryToAttack();
        }
    }
}
