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

    // Init �� ���� �ش� Ŭ������ ������Ʈ�� �ܺο��� �ʱ� �������� �� ��� = ���� �޾ƿ��� �޼���
    public void Init(int _damage)   // _dagame <- _�� ����� ������ this �� ������� �ʱ� ����
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
