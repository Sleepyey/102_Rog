using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Game 이라는 선택 항목이 추가되며 그 안에 Monster 라는 선택 항목 추가
// Monster 를 선택하면 MonsterStat 이라는 이름과 밑의 변수들을 가진 SO 생성
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
