using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterAttackType { TargetL, TargetC, Spread }
public enum MonsterMoveType { MoveT, MoveF }

// Game 이라는 선택 항목이 추가되며 그 안에 Monster 라는 선택 항목 추가
// Monster 를 선택하면 MonsterStat 이라는 이름과 밑의 변수들을 가진 SO 생성
[CreateAssetMenu(menuName = "Game/Monster", fileName = "MonsterStat")]
public class MonsterSO : ScriptableObject
{
    [Header("Attack")]
    public MonsterAttackType attackType;
    public MonsterMoveType moveType;
    public float attackCooldownSO;
    public float attackSpeedSO;
    public float distanceSO;

    [Header("Stat")]
    public int damageSO;
    public int hpSO;
    public float moveSpeedSO;
    public int goldSO;
    public int expSO;
}
