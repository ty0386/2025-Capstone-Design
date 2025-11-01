using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float lifeTime = 3f;   // 자동 파괴 시간
    public float damage = 1f;     // 필요 시 공격력

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
        GetComponent<SpriteRenderer>().sortingOrder = 10;
        Destroy(gameObject, lifeTime);
        // 일정 시간 후 자동 파괴 (성능 관리)
        Destroy(gameObject, lifeTime);
    }

    // 다른 오브젝트와 충돌했을 때
    void OnTriggerEnter2D(Collider2D collision)
    {
        //벽 레이어가 "Wall"일 경우
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject); // 총알 파괴
        }
    }
}
