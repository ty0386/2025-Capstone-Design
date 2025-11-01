using UnityEngine;

public class AddBullet : MonoBehaviour//아이템 획득 시 총알추가
{
    public int ammoAmount = 20; // 이 아이템을 먹으면 증가할 총알 수

    void OnTriggerEnter2D(Collider2D other)
    {
        TopDownShooting playerShooting = other.GetComponent<TopDownShooting>();
        if (playerShooting != null)
        {
            playerShooting.AddAmmo(ammoAmount);
            Destroy(gameObject); // 아이템 사라짐
        }
    }
}
