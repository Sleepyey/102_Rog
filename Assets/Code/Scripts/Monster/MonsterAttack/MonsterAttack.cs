using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private int damage;
    private float moveSpeed;
    private float dTime = 3f;
    private Vector2 direction;

    // Update
    private void Update()
    {
        if (direction == Vector2.zero)  // direction 이 없는걸 방지
        {
            Debug.LogWarning($"{gameObject.name} : direction이 설정되지 않았습니다.");
            Destroy(gameObject);
            return;
        }

        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (dTime > 0)
        {
            dTime -= Time.deltaTime;
        }
        else if (dTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Init 은 보통 해당 클래스나 오브젝트를 외부에서 초기 설정해줄 때 사용 = 값을 받아오는 메서드
    public void Init(int _damage, Vector2 _dir, float _moveSpeed)   // _dagame <- _를 사용한 이유는 this 를 사용하지 않기 위해
    {
        damage = _damage;
        direction = _dir;
        moveSpeed = _moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.PlayerHpM(damage);
                Destroy(gameObject);
            }
        }
    }
}
