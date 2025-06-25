using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float moveSpeed = 10f;
    private float dTime = 3f;

    private Vector2 moveD;
    private int damage;

    // Update
    void Update()
    {
        transform.Translate(moveD * moveSpeed * Time.deltaTime);

        if (dTime > 0)
        {
            dTime -= Time.deltaTime;
        }
        else if (dTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Init(int _damage, Vector2 direction)
    {
        damage = _damage;
        moveD = direction.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") || collision.CompareTag("Boss"))
        {
            Monster monster = collision.GetComponent<Monster>();
            if (monster != null)
            {
                monster.MonsterHpM(damage);
                Destroy(gameObject);
            }
        }
    }
}