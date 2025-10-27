using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MainSoldier_Move : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public float moveSpeed = 3f;
    public float jumpForce = 300f;
    public GameObject objSoldier;

    //Groudn check settings, 지면 확인용 변수
    public Transform groundCheck; //오브젝트 연결 변수
    public float groundCheckRadius = 0.2f; // 지면을 감지하는 원의 반지름
    public LayerMask whatIsGround; //groudn 레이어 선택 변수
    private bool isGrounded; //지면인지 확인하는 실시간으로 저장하는 변수
    void Start()
    {
        objSoldier.transform.position = new Vector3(-8, 0, 0); // Vector3 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D 없음");
        }
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    void Update()
    {
        //좌우 이동 항목
        float h = Input.GetAxis("Horizontal");
        if (h < 0) { transform.localScale = new Vector3(-1, 1, 1); }
        else if (h > 0) { transform.localScale = new Vector3(1, 1, 1); }
        transform.Translate(new Vector3(h, 0, 0) * moveSpeed * Time.deltaTime);




        float currunetVelocity = Mathf.Abs(Input.GetAxis("Horizontal"));

        animator.SetFloat("Velocity", currunetVelocity); //이동속도 설정
        //점프 조작 항목
        //점프시 조건을 설정, 땅에 닿아있을 때 점프가 실행됨. and연산으로 화살표 위쪽 및 땅에 닿아있을때만 점프
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce);
    }
    void OnDrawGizmosSelected() //씬에서 지면을 감지하는 범위를 보여줌, 불필요할시 비활성화
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
    /*추가로 레이캐스트 등 이용해서 지면에 있을때만 점프할 수 있도록 바꿔야 함.
    지면 체크, 이벤트 트리거 설정시 플랫포머 이동 스크립트는 끝날 것임. 이후로는 맵 생성이 남음.*/

}
