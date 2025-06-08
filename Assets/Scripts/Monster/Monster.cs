using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //MonsterSO ScriptableObject 를 monsterSO 변수에 선언 받는다 (Inspector 에서 넣어줘야 함)
    [SerializeField] MonsterSO monsterSO;

    [Header("Monster Stat")]
    public int damage;
    public int hp;
    public float attackSpeed;
    public float moveSpeed;

    [Header("Drop Stat")]
    public int ex;
    public int gold;

    // Player 스크립트를 player 변수에 선언
    private Player player;

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
        
    }

    // Plyaer HP 를 몬스터의 대미지만큼 감소하기 위한 함수
    public void PlayerGiveDamage()
    {
        player.PlayerHpM(damage);   //Player 스크립트의 PlayerHpM 함수 사용 ( Player HP 를 damage 만큼 감소 )
    }
}
