using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLevelStatUI : MonoBehaviour
{
    public static PlayerLevelStatUI instance;

    [Header("Panel")]
    [SerializeField] private GameObject panel;

    [Header("Stat Text")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI speedText;

    private Player player;

    public bool isOpen => panel.activeSelf;

    // Awake
    private void Awake()
    {
        instance = this;
        panel.SetActive(false);
    }

    // Start
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void LevelUpOpen()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;    // 일시 정지
        UpdateStatUI();
    }

    public void LevelUpClose()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;    // 재개

        if (player != null && player.HaslevelUps())
        {
            LevelUpOpen();
        }
    }

    public void OnClickUpgradeHp()
    {
        player.hp += 2;
        player.MlevelUps();
        LevelUpClose();
    }

    public void OnClickUpgradeDamage()
    {
        player.damage += 1;
        player.MlevelUps();
        LevelUpClose();
    }

    public void OnClickUpgradeSpeed()
    {
        player.moveSpeed += 0.5f;
        player.MlevelUps();
        LevelUpClose();
    }

    private void UpdateStatUI()
    {
        hpText.text = $"HP: {player.hp}";
        damageText.text = $"Damage: {player.damage}";
        speedText.text = $"Speed: {player.moveSpeed}";
    }
}
