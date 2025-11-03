using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;       // 총알 프리팹
    public Transform firePoint;           // 총구 위치
    public float bulletSpeed = 10f;       // 총알 속도
    public float fireRate = 0.2f;         // 발사 간격
    private float nextFireTime = 0f;

    [Header("Ammo Settings")]
    public int maxAmmo = 100;             // 전체 탄약 수
    public int magSize = 10;              // 한 탄창 용량
    private int currentAmmo;              // 현재 남은 탄약
    private int currentMagAmmo;           // 현재 탄창 안의 탄약

    [Header("Reload Settings")]
    public float reloadTime = 1.5f;       // 장전 시간(초)
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
        currentMagAmmo = magSize;
    }

    void Update()
    {
        // 🔫 발사
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && Time.time >= nextFireTime)
        {
            Shoot();
        }

        // ♻️ 재장전 (오른쪽 클릭)
        if (Input.GetMouseButtonDown(1) && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        if (isReloading) return;
        if (currentMagAmmo <= 0)
        {
            Debug.Log("탄창 비었습니다!");
            return;
        }
        if (bulletPrefab == null || firePoint == null) return;

        nextFireTime = Time.time + fireRate;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Rigidbody2D velocity로 발사
            rb.linearVelocity = firePoint.up * bulletSpeed;

            // 총알 회전도 방향에 맞춤
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }

        Destroy(bullet, 3f);

        currentMagAmmo--;
        currentAmmo--;

        Debug.Log($"발사! 남은 탄창: {currentMagAmmo}/{magSize}, 전체 탄약: {currentAmmo}/{maxAmmo}");
    }



    System.Collections.IEnumerator Reload()
    {
        // 남은 탄약이 없으면 재장전 불가
        if (currentAmmo <= 0 && currentMagAmmo <= 0)
        {
            Debug.Log("❌ 남은 총알이 없습니다!");
            yield break;
        }

        isReloading = true;
        Debug.Log("🔄 장전 중...");

        yield return new WaitForSeconds(reloadTime);

        int needed = magSize - currentMagAmmo; // 채워야 할 탄창 수
        int loadAmount = Mathf.Min(needed, currentAmmo); // 남은 탄약에서 가져옴

        currentMagAmmo += loadAmount;
        isReloading = false;

        Debug.Log($"✅ 장전 완료! 현재 탄창: {currentMagAmmo}/{magSize}, 전체 탄약: {currentAmmo}/{maxAmmo}");
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo)
            currentAmmo = maxAmmo; // 최대 총알 수 제한

        Debug.Log($"💊 탄약 아이템 획득! 전체 탄약: {currentAmmo}/{maxAmmo}");
    }
}
