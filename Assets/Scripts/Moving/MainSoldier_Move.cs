using UnityEngine;

public class MainSoldier_Move : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public float moveSpeed = 3f;
    public float jumpForce = 300f;
    public GameObject objSoldier;


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
    void Update()
    {
        //좌우 이동 항목
        float h = Input.GetAxis("Horizontal");
        if (h < 0) { transform.localScale = new Vector3(-1, 1, 1); }
        else if (h > 0) { transform.localScale = new Vector3(1, 1, 1); }
        transform.Translate(new Vector3(h, 0, 0) * moveSpeed * Time.deltaTime);

        //점프 조작 항목
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        { Jump(); }

        float currunetVelocity = Mathf.Abs(Input.GetAxis("Horizontal"));


        animator.SetFloat("Velocity", currunetVelocity);
        //이동속도 설정
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce);
    }
    /*추가로 레이캐스트 등 이용해서 지면에 있을때만 점프할 수 있도록 바꿔야 함.
    지면 체크, 이벤트 트리거 설정시 플랫포머 이동 스크립트는 끝날 것임. 이후로는 맵 생성이 남음.*/

}
