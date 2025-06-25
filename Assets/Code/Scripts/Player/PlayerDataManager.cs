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
            DontDestroyOnLoad(gameObject);  // ���� �̵��ص� ������ ����
            LoadPlayerData();
        }
        else
        {
            Destroy(gameObject);    // �ߺ� ����
        }
    }

    //  Load ���� -> ��ǻ�Ϳ� �÷��� ����� ������ �� ����� ������
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
            playerData = new PlayerData(); // �ʱⰪ ����
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