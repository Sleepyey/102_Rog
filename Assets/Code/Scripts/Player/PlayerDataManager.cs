using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public List<string> statData = new List<string>();

    public int hpData;
    public int damageData;
    public float attackSpeedData;
    public float moveSpeedData;
    public int exData;
    public int ifExData;
    public int goldData;
}

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance;
    public PlayerData playerData;
}
