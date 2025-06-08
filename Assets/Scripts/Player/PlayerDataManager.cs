using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
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
