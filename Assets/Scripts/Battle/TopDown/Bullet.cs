using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float lifeTime = 3f;
    public int damage = 1;
    public bool isPlayerBullet = true; // true = 플레이어 총알, false = 적 총알

    void Start()
    {
        Destroy(gameObject, lifeTime); // 일정 시간 후 자동 파괴
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 벽 충돌
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
            return;
        }

        if (isPlayerBullet)
        {
            // 적에게 데미지
            if (collision.CompareTag("Enemy"))
            {
                EnemyAI enemy = collision.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    enemy.TakeDamage(1);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            // 플레이어에게 데미지
            if (collision.CompareTag("Player"))
            {
                TopDownMove player = collision.GetComponent<TopDownMove>();
                if (player != null)
                {
                    player.TakeDamage(1);
                }
                Destroy(gameObject);
            }
        }
    }
}
