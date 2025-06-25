using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterAttackType { TargetL, TargetC, Spread }
public enum MonsterMoveType { MoveT, MoveF }

// Game �̶�� ���� �׸��� �߰��Ǹ� �� �ȿ� Monster ��� ���� �׸� �߰�
// Monster �� �����ϸ� MonsterStat �̶�� �̸��� ���� �������� ���� SO ����
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
