using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //MonsterSO ScriptableObject 를 monsterSO 변수에 선언 받는다 (Inspector 에서 넣어줘야 함)
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
        //ScriptableObject 에서 받아온 값을 기존에 있는 변수값에 선언한다
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
        // obj 변수에 프리팹을 넣어주며 프리팹 생성
        // obj에 생성된 프리팹이 들어있기 때문에 그 안에 있는 스크립트를 찾아 monsterAttack에 넣음
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
            // 플레이어한테 대미지
        }
    }
}
