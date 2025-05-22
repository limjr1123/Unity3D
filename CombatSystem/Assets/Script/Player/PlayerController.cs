using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 velocity;

    [SerializeField] float rotationSpeed = 500f;

    [Header("Ground Check")]
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] LayerMask groundLayer;

    Quaternion targetRotation;  // 목표 회전값(이동 방향)
    CameraController cameraController;  // 카메라 컨트롤러 참조
    Animator animator;  
    CharacterController characterController;    // 캐릭터 컨트롤러 컴포넌트 참조
    MeleeFighter meleeFighter;

    bool isGround;
    float ySpeed;

    private float lerpTime;
    private float accelerationTime;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        meleeFighter = GetComponent<MeleeFighter>();
    }

    void Update()
    {
        // 공격 중에는 이동/회전/애니메이션 입력을 막음
        if (meleeFighter.inAction)
        {
            animator.SetFloat("forwardSpeed", 0);
            return;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));

        var moveInput = (new Vector3(h, 0, v)).normalized;

        var moveDir = cameraController.PlanarRotation * moveInput;

        GroundCheck();

        if (isGround)
        {
            ySpeed = -0.5f; // 바닥에 닿아있을 때 ySpeed를 0으로 설정
        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }


        velocity = moveDir * moveSpeed;
        velocity.y = ySpeed;


        if (moveAmount > 0)
        {
            // 이동 처리
            characterController.Move(velocity * Time.deltaTime);
            // 회전 처리
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        // 부드러운 회전 처리
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // 애니메이션 처리
        animator.SetFloat("forwardSpeed", moveAmount, 0.15f, Time.deltaTime);
    }

    void GroundCheck()
    {
        isGround = Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }
}
