using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //MonsterSO ScriptableObject 를 monsterSO 변수에 선언 받는다 (Inspector 에서 넣어줘야 함)
    [SerializeField] MonsterSO monsterSO;

    private MonsterMoveType moveType;

    [Header("Attack")]
    public GameObject attackPrefab;
    public float attackCooldown;
    private float attackTimer;
    public float attackSpeed;
    public float distance;

    [Header("Drop Stat")]
    [SerializeField] GameObject goldPrefab;
    [SerializeField] GameObject expPrefab;
    public int gold;
    public int exp;

    [Header("Monster Stat")]
    public int damage;
    public int hp;
    public float moveSpeed;

    private bool monsterDie = false;

    private Transform player;

    private Vector2 moveDirection;
    private float moveChange = 2f;
    private float moveChangeTimer = 0f;
    private Rigidbody2D rb;

    // Awake
    private void Awake()
    {
        //ScriptableObject 에서 받아온 값을 기존에 있는 변수값에 선언한다
        moveType = monsterSO.moveType;

        attackCooldown = monsterSO.attackCooldownSO;
        attackSpeed = monsterSO.attackSpeedSO;
        distance = monsterSO.distanceSO;

        damage = monsterSO.damageSO;
        hp = monsterSO.hpSO;
        moveSpeed = monsterSO.moveSpeedSO;

        gold = monsterSO.goldSO;
        exp = monsterSO.expSO;
    }

    // Start
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;   // 회전 고정
        MoveChangeDirection();
    }

    // Update
    void Update()
    {
        if (player == null) // player가 null이면 Update 종료
        {
            return;
        }

        if (moveType == MonsterMoveType.MoveT)
        {
            if (moveChangeTimer > 0f)
            {
                moveChangeTimer -= Time.deltaTime;
            }
            else if (moveChangeTimer <= 0f)
            {
                MoveChangeDirection();
            }
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);  // MovePosition = Unity 에서 Rigidnody2D 를 물리 기반으로 이동
        }
        else if (moveType == MonsterMoveType.MoveF)
        {
            // 고정형 몬스터기에 이동 X
        }

        float distancePlayer = Vector2.Distance(transform.position, player.position);   // Distance(1, 2) 1과 2 사이 직선 거리를 계산

        if (distancePlayer < distance)    // 플레이어가 범위 안에 들어오면
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0f;
                Attack();
            }
        }
        else
        {
            if (attackTimer <= attackCooldown)
            {
                attackTimer += Time.deltaTime;
            }
        }
    }

    private void MoveChangeDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized;
        moveChangeTimer = moveChange;
    }

    private void Drop()
    {
        if (goldPrefab == null || expPrefab == null)
        {
            Debug.LogWarning($"Drop Prefab is null on {gameObject.name}");
            return;
        }

        GameObject goldObj = Instantiate(goldPrefab, transform.position, Quaternion.identity);
        goldObj.GetComponent<MonsterDrop>().Init(gold, DropType.Gold);

        GameObject expObj = Instantiate(expPrefab, transform.position, Quaternion.identity);
        expObj.GetComponent<MonsterDrop>().Init(exp, DropType.Exp);
    }

    public void Attack()
    {
        switch (monsterSO.attackType)
        {
            case MonsterAttackType.TargetL:
                AttackTargetL();
                break;
            case MonsterAttackType.TargetC:
                Debug.LogWarning("TargetC는 현재 존재하지 않는 공격 방식입니다.");
                break;
            case MonsterAttackType.Spread:
                AttackSpread();
                break;
        }
    }

    private void AttackTargetL()
    {
        if (GameObject.FindGameObjectWithTag("Player") is GameObject player)
        {
            Vector2 dir = (player.transform.position - transform.position).normalized;
            GameObject obj = Instantiate(attackPrefab, transform.position, Quaternion.identity);
            MonsterAttack monsterAttack = obj.GetComponent<MonsterAttack>();
            // obj 변수에 프리팹을 넣어주며 프리팹 생성
            // obj에 생성된 프리팹이 들어있기 때문에 그 안에 있는 스크립트를 찾아 monsterAttack에 넣음
            if (monsterAttack != null)
            {
                monsterAttack.Init(damage, dir, attackSpeed);
            }
        }
    }

    private void AttackSpread()
    {
        Vector2[] directions = new Vector2[]
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
            new Vector2(-1, 1).normalized,   // ↖
            new Vector2(1, 1).normalized,    // ↗
            new Vector2(-1, -1).normalized,  // ↙
            new Vector2(1, -1).normalized    // ↘
        };

        foreach (Vector2 dir in directions)
        {
            GameObject obj = Instantiate(attackPrefab, transform.position, Quaternion.identity);
            MonsterAttack monsterAttack = obj.GetComponent<MonsterAttack>();
            if (monsterAttack != null)
            {
                monsterAttack.Init(damage, dir, attackSpeed);
            }
        }
    }

    public void MonsterHpM(int Damage)
    {
        hp -= Damage;
        if (hp <= 0 && !monsterDie)
        {
            monsterDie = true;
            Drop();
            if (gameObject.tag == "Boss")
            {
                Boss boss = GetComponent<Boss>();
                if (boss != null)
                {
                    boss.BossDie();
                    return; // 밑에 있는 Destroy 방지 / BossDie에 Destroy 존재
                }
            }
            Destroy(gameObject, 0.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어한테 대미지
        }
    }
}
