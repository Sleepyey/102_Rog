using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //MonsterSO ScriptableObject �� monsterSO ������ ���� �޴´� (Inspector ���� �־���� ��)
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
        //ScriptableObject ���� �޾ƿ� ���� ������ �ִ� �������� �����Ѵ�
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
        rb.freezeRotation = true;   // ȸ�� ����
        MoveChangeDirection();
    }

    // Update
    void Update()
    {
        if (player == null) // player�� null�̸� Update ����
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
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);  // MovePosition = Unity ���� Rigidnody2D �� ���� ������� �̵�
        }
        else if (moveType == MonsterMoveType.MoveF)
        {
            // ������ ���ͱ⿡ �̵� X
        }

        float distancePlayer = Vector2.Distance(transform.position, player.position);   // Distance(1, 2) 1�� 2 ���� ���� �Ÿ��� ���

        if (distancePlayer < distance)    // �÷��̾ ���� �ȿ� ������
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
                Debug.LogWarning("TargetC�� ���� �������� �ʴ� ���� ����Դϴ�.");
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
            // obj ������ �������� �־��ָ� ������ ����
            // obj�� ������ �������� ����ֱ� ������ �� �ȿ� �ִ� ��ũ��Ʈ�� ã�� monsterAttack�� ����
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
            new Vector2(-1, 1).normalized,   // ��
            new Vector2(1, 1).normalized,    // ��
            new Vector2(-1, -1).normalized,  // ��
            new Vector2(1, -1).normalized    // ��
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
                    return; // �ؿ� �ִ� Destroy ���� / BossDie�� Destroy ����
                }
            }
            Destroy(gameObject, 0.05f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾����� �����
        }
    }
}
