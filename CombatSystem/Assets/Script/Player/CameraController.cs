using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 카메라가 따라갈 대상(플레이어)의 Transform
    [SerializeField] Transform followTarget;

    // 마우스 입력에 따른 카메라 회전 속도
    [SerializeField] float rotationSpeed = 2f;

    // 타겟과 카메라 사이의 거리
    [SerializeField] float distance = 5f;

    // 카메라의 수직 회전(X축) 제한 각도
    [SerializeField] float minVerticalAngle = -45f;
    [SerializeField] float maxVerticalAngle = 45f;

    // 카메라 초점 위치의 X, Y 오프셋 (화면 프레임 조정)
    [SerializeField] Vector2 framingOffset;

    // 마우스 입력의 X, Y축 반전 여부
    [SerializeField] bool invertX = false;
    [SerializeField] bool invertY = false;

    // 현재 카메라의 수직(X) 및 수평(Y) 회전 각도
    float rotationX;
    float rotationY;

    // 반전 여부에 따라 곱해지는 값 (1 또는 -1)
    float invertXVal;
    float invertYVal;

    // 게임 시작 시 호출: 커서를 숨기고 잠금
    private void Start()
    {
        Cursor.visible = false; // 커서를 화면에서 숨김
        Cursor.lockState = CursorLockMode.Locked; // 커서를 화면 중앙에 고정
    }

    // 매 프레임 호출: 카메라 회전 및 위치 업데이트
    private void Update()
    {
        // 마우스 입력 반전 설정: invertX/Y가 true면 -1, false면 1
        invertXVal = invertX ? -1 : 1;
        invertYVal = invertY ? -1 : 1;

        // 마우스 Y축 입력으로 수직 회전(X축) 업데이트 -> X 축 회전
        rotationX += Input.GetAxis("Mouse Y") * invertYVal * rotationSpeed;
        // 수직 회전을 최소/최대 각도로 제한
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        // 마우스 X축 입력으로 수평 회전(Y축) 업데이트 -> Y 축 회전
        rotationY += Input.GetAxis("Mouse X") * invertXVal * rotationSpeed;

        // 목표 회전 Quaternion 생성 (X축: 수직, Y축: 수평, Z축: 0)
        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        // 타겟 위치에 오프셋을 적용하여 카메라 초점 위치 계산
        var focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y, 0);
         
        // 카메라 위치 계산: 초점 위치에서 회전과 거리를 적용해 뒤로 이동
        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);

        // 카메라 회전 설정
        transform.rotation = targetRotation;
    }

    // 수평 회전(Y축)만 포함된 Quaternion 반환 (플레이어 이동 방향 동기화용)
    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}