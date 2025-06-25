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
        if (direction == Vector2.zero)  // direction �� ���°� ����
        {
            Debug.LogWarning($"{gameObject.name} : direction�� �������� �ʾҽ��ϴ�.");
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

    // Init �� ���� �ش� Ŭ������ ������Ʈ�� �ܺο��� �ʱ� �������� �� ��� = ���� �޾ƿ��� �޼���
    public void Init(int _damage, Vector2 _dir, float _moveSpeed)   // _dagame <- _�� ����� ������ this �� ������� �ʱ� ����
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
