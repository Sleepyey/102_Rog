using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game �̶�� ���� �׸��� �߰��Ǹ� �� �ȿ� Monster ��� ���� �׸� �߰�
// Monster �� �����ϸ� MonsterStat �̶�� �̸��� ���� �������� ���� SO ����
[CreateAssetMenu(menuName = "Game/Monster", fileName = "MonsterStat")]
public class MonsterSO : ScriptableObject
{
    [Header("Stat")]
    public int damageSO;
    public int hpSO;
    public int attackSpeedSO;
    public int exSO;
    public int goldSO;
}
