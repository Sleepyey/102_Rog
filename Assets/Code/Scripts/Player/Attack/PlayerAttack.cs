using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float moveX;
    private float moveY;
    private float dTime = 4;

    private int damage;

    // Start
    void Start()
    {
        Player player = FindObjectOfType<Player>();
        moveX = player.attackMoveX * 0.05f;
        moveY = player.attackMoveY * 0.05f;
    }

    // Update
    void Update()
    {
        transform.Translate(moveX, moveY, 0);

        if (dTime > 0)
        {
            dTime -= Time.deltaTime;
        }
        else if (dTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Init(int _damage)
    {
        damage = _damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Monster monster = collision.GetComponent<Monster>();
            if (monster != null)
            {
                monster.MonsterHpM(damage);
            }
        }
    }
}