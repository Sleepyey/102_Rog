using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //MonsterSO ScriptableObject �� monsterSO ������ ���� �޴´� (Inspector ���� �־���� ��)
    [SerializeField] MonsterSO monsterSO;

    [Header("Monster Stat")]
    public int damage;
    public int hp;
    public float attackSpeed;
    public float moveSpeed;

    [Header("Drop Stat")]
    public int ex;
    public int gold;

    // Player ��ũ��Ʈ�� player ������ ����
    private Player player;

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
        
    }

    // Plyaer HP �� ������ �������ŭ �����ϱ� ���� �Լ�
    public void PlayerGiveDamage()
    {
        player.PlayerHpM(damage);   //Player ��ũ��Ʈ�� PlayerHpM �Լ� ��� ( Player HP �� damage ��ŭ ���� )
    }
}
