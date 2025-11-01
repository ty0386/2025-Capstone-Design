using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Collision")]
    public LayerMask wallLayer; // "Wall" 레이어 지정
    public float rayDistance = 0.1f; // 플레이어 앞 레이 길이

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    void Update()
    {
        // 입력 수집
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
    }

    void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        Vector2 moveDelta = moveInput * moveSpeed * Time.fixedDeltaTime;

        // 이동할 방향으로 레이 캐스트 체크
        if (moveInput.sqrMagnitude > 0.01f)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentPos, moveInput, moveDelta.magnitude + rayDistance, wallLayer);

            if (hit.collider == null)
            {
                // 이동 가능하면 이동
                rb.MovePosition(currentPos + moveDelta);
            }

            // 회전
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        }
    }

    // 디버그용: 레이 시각화
    void OnDrawGizmos()
    {
        if (rb == null) return;
        Gizmos.color = Color.red;
        Vector2 start = rb.position;
        Vector2 dir = moveInput;
        Gizmos.DrawLine(start, start + dir * (moveInput.magnitude * moveSpeed * Time.fixedDeltaTime + rayDistance));
    }
}
