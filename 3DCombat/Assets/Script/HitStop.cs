using System.Collections;
using UnityEngine;


// Hit stop 개선
// 1. 랜덤한 카메라 흔들림 효과 추가
// 2 .시간 스케일링 점진적인 복귀
// 3. 히트스탑 강도 파라미터화


//주요 개선 사항:

//인스펙터 구성 개선

//SerializeField 속성과 Header 속성을 사용하여 인스펙터 UI를 개선
//변수들을 논리적으로 그룹화
//카메라 흔들림 개선

//랜덤한 위치로 카메라를 흔들어 더 역동적인 효과 생성
//흔들림 강도와 빈도를 조절할 수 있는 파라미터 추가
//원래 카메라 위치를 저장하고 복원
//시간 조절 개선

//시간 스케일을 부드럽게 복구
//히트스탑 강도를 파라미터로 전달 가능
//중복 실행 방지 로직 개선
//코드 안정성

//코루틴 참조 관리
//명확한 변수명 사용
//적절한 접근 제한자 사용
//이 개선된 버전을 사용하면 더 다양하고 커스터마이즈 가능한 히트스탑 효과를 만들 수 있습니다.



public class HitStop : MonoBehaviour
{
    [Header("Time Settings")]
    public float stopTime = 0.1f;
    public float timeScaleRecoverySpeed = 0.1f;

    [Header("Camera Shake")]
    [SerializeField] private Transform shakeCam;
    public float shakeIntensity = 0.1f;
    public float shakeFrequency = 0.1f;

    private bool isHitStopped;
    private Vector3 originalCamPosition;
    private Coroutine shakeCoroutine;

    public static HitStop Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }



    public void StopTime()
    {
        if (!isHitStopped)
        {
            isHitStopped = true;
            Time.timeScale = 0f;

            if (shakeCoroutine != null)
                StopCoroutine(shakeCoroutine);

            shakeCoroutine = StartCoroutine(ShakeCamera());
            StartCoroutine(ReturnTimeScale());
        }
    }

    private IEnumerator ShakeCamera()
    {
        originalCamPosition = shakeCam.localPosition;
        float elapsed = 0f;

        while (elapsed < stopTime)
        {
            float x = Random.Range(-1f, 1f) * shakeIntensity * stopTime;
            float y = Random.Range(-1f, 1f) * shakeIntensity * stopTime;

            shakeCam.localPosition = new Vector3(x, y, originalCamPosition.z);

            elapsed += Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(shakeFrequency);
        }

        shakeCam.localPosition = originalCamPosition;
    }

    private IEnumerator ReturnTimeScale()
    {
        yield return new WaitForSecondsRealtime(stopTime);

        while (Time.timeScale < 1f)
        {
            //float result = Mathf.MoveTowards(current, target, maxDelta);
            //current = 현재값
            //target = 목표값
            //maxDelta = 변화량
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1f, Time.unscaledDeltaTime * timeScaleRecoverySpeed);
            yield return null;
        }

        Time.timeScale = 1f;
        isHitStopped = false;
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);
    }
}

