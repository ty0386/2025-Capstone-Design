using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public enum State { Idle, Patrol, Investigate, Alert, Chase, Attack, Dead }
    public State state = State.Patrol;

    [Header("Perception")]
    public float viewDistance = 8f;
    public float viewAngle = 90f;
    public LayerMask viewMask;
    public Transform eyes;

    [Header("Sound")]
    public float soundRadius = 6f;

    [Header("Movement")]
    public float walkSpeed = 2f;          // Patrol 속도
    public float chaseSpeed = 3.5f;       // Chase 속도 (추격 속도)
    public Transform[] patrolPoints;
    int patrolIndex = 0;
    public float inspectDuration = 2f;

    [Header("Combat")]
    public bool hasRangedWeapon = false;
    public GameObject bulletPrefab;       // 적 총알 프리팹
    public Transform firePoint;           // 적 총구 위치
    public float attackRange = 1.2f;
    public float shootRange = 6f;
    public float attackCooldown = 0.6f;
    float lastAttackTime = -10f;
    public int hp = 1;
    public float projectileSpeed = 10f;   // 적 총알 속도

    Transform player;
    Vector3 lastHeardPosition;
    Vector3 lastSeenPosition;

    Rigidbody2D rb2d;
    Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (state == State.Dead) return;

        if (player != null && CanSeePlayer())
        {
            lastSeenPosition = player.position;
            state = State.Chase;
        }

        switch (state)
        {
            case State.Patrol: PatrolUpdate(); break;
            case State.Investigate: InvestigateUpdate(); break;
            case State.Chase: ChaseUpdate(); break;
            case State.Attack: AttackUpdate(); break;
            case State.Idle: break;
        }
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;
        Vector3 dir = (player.position - eyes.position);
        float dist = dir.magnitude;
        if (dist > viewDistance) return false;

        float angle = Vector3.Angle(transform.up, dir);
        if (angle > viewAngle * 0.5f) return false;

        RaycastHit2D hit = Physics2D.Raycast(eyes.position, dir.normalized, viewDistance, viewMask | (1 << LayerMask.NameToLayer("Player")));
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            return true;
        }
        return false;
    }

    void PatrolUpdate()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;
        Transform target = patrolPoints[patrolIndex];
        MoveTowards(target.position, walkSpeed);

        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }

    void InvestigateUpdate()
    {
        MoveTowards(lastHeardPosition, walkSpeed);
        if (Vector2.Distance(transform.position, lastHeardPosition) < 0.5f)
        {
            StartCoroutine(InspectCoroutine());
        }
    }

    IEnumerator InspectCoroutine()
    {
        state = State.Idle;
        yield return new WaitForSeconds(inspectDuration);
        if (state != State.Chase) state = State.Patrol;
    }

    void ChaseUpdate()
    {
        if (player == null) return;
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (hasRangedWeapon && distToPlayer <= shootRange)
        {
            state = State.Attack;
            return;
        }
        if (!hasRangedWeapon && distToPlayer <= attackRange)
        {
            state = State.Attack;
            return;
        }

        // Chase 속도로 이동
        MoveTowards(player.position, chaseSpeed);
    }

    void AttackUpdate()
    {
        if (player == null) return;
        if (Time.time - lastAttackTime < attackCooldown) return;
        lastAttackTime = Time.time;

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (hasRangedWeapon && distToPlayer <= shootRange)
        {
            ShootAtPlayer();
        }
        else if (!hasRangedWeapon && distToPlayer <= attackRange)
        {
            MeleeHit();
        }
        else
        {
            state = State.Chase;
        }
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 dir = (target - transform.position).normalized;
        rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);
    }

    void ShootAtPlayer()
    {
        if (bulletPrefab == null || firePoint == null || player == null) return;

        // 플레이어 위치 향하도록 방향 계산
        Vector2 dir = (player.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = dir * projectileSpeed; // velocity 사용
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }

        Destroy(bullet, 3f);
    }

    void MeleeHit()
    {
        Debug.Log("Enemy Melee Hit Player!");
        // 필요시 플레이어 데미지 호출
    }

    public void OnSoundHeard(Vector3 position, float volumeRadius)
    {
        if (Vector2.Distance(transform.position, position) <= soundRadius + volumeRadius)
        {
            lastHeardPosition = position;
            if (state != State.Chase) state = State.Investigate;
        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0) Die();
    }

    void Die()
    {
        state = State.Dead;
        Destroy(gameObject, 0.1f);
    }
}
