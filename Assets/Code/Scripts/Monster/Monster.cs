using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //MonsterSO ScriptableObject �� monsterSO ������ ���� �޴´� (Inspector ���� �־���� ��)
    [SerializeField] MonsterSO monsterSO;

    [Header("Attack Effect")]
    public GameObject attackPrefab;

    [Header("Monster Stat")]
    public int damage;
    public int hp;
    public float attackSpeed;
    public float moveSpeed;

    [Header("Drop Stat")]
    public int ex;
    public int gold;

    // Awake
    private void Awake()
    {
        //ScriptableObject ���� �޾ƿ� ���� ������ �ִ� �������� �����Ѵ�
        damage = monsterSO.damageSO;
        hp = monsterSO.hpSO;
        attackSpeed = monsterSO.attackSpeedSO;
        ex = monsterSO.exSO;
        gold = monsterSO.goldSO;
    }

    // Start
    void Start()
    {
        
    }

    // Update
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        Transform monsterTransform = transform;
        GameObject obj = Instantiate(attackPrefab, monsterTransform.position, Quaternion.identity);

        MonsterAttack monsterAttack = obj.GetComponent<MonsterAttack>();
        // obj ������ �������� �־��ָ� ������ ����
        // obj�� ������ �������� ����ֱ� ������ �� �ȿ� �ִ� ��ũ��Ʈ�� ã�� monsterAttack�� ����
        if (monsterAttack != null)
        {
            monsterAttack.Init(damage);
        }
    }

    public void MonsterHpM(int Damage)
    {
        hp -= Damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾����� �����
        }
    }
}
