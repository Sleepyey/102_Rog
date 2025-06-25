using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance;
    public PlayerData playerData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 씬을 이동해도 데이터 유지
            LoadPlayerData();
        }
        else
        {
            Destroy(gameObject);    // 중복 방지
        }
    }

    //  Load 기준 -> 컴퓨터에 플레이 기록이 있으면 그 기록을 가져옴
    public void SavePlayerData()
    {
        string path = Application.persistentDataPath + "/playerdata.json";
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(path, json);
        Debug.Log($"[Save Data] {path}");
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/playerdata.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            playerData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("[Load Data]");
        }
        else
        {
            playerData = new PlayerData(); // 초기값 설정
            playerData.hpData = 10;
            playerData.damageData = 1;
            playerData.moveSpeedData = 2f;
            playerData.expData = 0;
            playerData.ifExpData = 4;
            playerData.levelData = 1;
            playerData.goldData = 0;
            Debug.Log("[New Data]");
        }
    }
}

[Serializable]
public class PlayerData
{
    public int hpData;
    public int damageData;
    public float moveSpeedData;
    public int expData;
    public int ifExpData;
    public int levelData;
    public int goldData;
}