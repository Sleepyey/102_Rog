using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    [Header("Gold")]
    [SerializeField] private TextMeshProUGUI totalGoldText;

    [Header("Stat UI")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI speedText;

    private void Start()
    {
        var data = PlayerDataManager.instance.playerData;

        hpText.text = $"HP: {data.hpData}";
        damageText.text = $"Damage: {data.damageData}";
        speedText.text = $"Speed: {data.moveSpeedData}";
    }

    private void Update()
    {
        totalGoldText.text = $"Gold: {PlayerDataManager.instance.playerData.goldData}";
    }

    public void UpgradeHP()
    {
        var data = PlayerDataManager.instance.playerData;
        if (data.goldData >= 10)
        {
            data.hpData += 2;
            data.goldData -= 10;
            hpText.text = $"HP: {data.hpData}";
            totalGoldText.text = $"Gold: {data.goldData}";
            PlayerDataManager.instance.SavePlayerData();
        }
    }

    public void UpgradeDamage()
    {
        var data = PlayerDataManager.instance.playerData;
        if (data.goldData >= 10)
        {
            data.damageData += 1;
            data.goldData -= 10;
            damageText.text = $"Damage: {data.damageData}";
            totalGoldText.text = $"Gold: {data.goldData}";
            PlayerDataManager.instance.SavePlayerData();
        }
    }

    public void UpgradeSpeed()
    {
        var data = PlayerDataManager.instance.playerData;
        if (data.goldData >= 10)
        {
            data.moveSpeedData += 0.5f;
            data.goldData -= 10;
            speedText.text = $"Speed: {data.moveSpeedData}";
            totalGoldText.text = $"Gold: {data.goldData}";
            PlayerDataManager.instance.SavePlayerData();
        }
    }
}