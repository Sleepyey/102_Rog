using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Init 은 보통 해당 클래스나 오브젝트를 외부에서 초기 설정해줄 때 사용 = 값을 받아오는 메서드
    public void Init(int _damage)   // _dagame <- _를 사용한 이유는 this 를 사용하지 않기 위해
    {
        damage = _damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.PlayerHpM(damage);
            }
        }
    }
}
