using UnityEngine;

public class TopDownMove : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Player Stats")]
    public int hp = 3; // 플레이어 체력

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        RotateToMouse();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    void RotateToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    // ✅ 플레이어 데미지 처리 함수 추가
    public void TakeDamage(int amount)
    {
        hp -= amount;
        Debug.Log($"플레이어가 {amount} 피해를 입었습니다. 남은 HP: {hp}");
        if (hp <= 0)
        {
            Debug.Log("플레이어 사망!");
            gameObject.SetActive(false);
            // 필요하면 죽음 처리(예: 게임오버, 리스폰) 예정*
        }
    }
}

