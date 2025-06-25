using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private Sprite spriteLeft;
    [SerializeField] private Sprite spriteRight;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI uiGold;
    [SerializeField] private TextMeshProUGUI uiHp;
    [SerializeField] private TextMeshProUGUI uiLevel;
    [SerializeField] private Image expBarFill;

    [Header("Attack Effect")]
    public GameObject attackPrefab;

    [Header("Stat")]
    public int damage;
    public int hp;
    public float moveSpeed;
    public int exp;
    public int ifExp;
    public int level;
    public int gold;

    [Header("Attack Direction")]
    public float attackMoveX;
    public float attackMoveY;

    private PlayerData playerData;

    Rigidbody2D rb;
    SpriteRenderer sR;

    Vector2 input;
    Vector2 velocity;

    // Awake
    private void Awake()
    {
        playerData = PlayerDataManager.instance.playerData;

        // 게임 시작 시 플레이어 데이터를 PlayerData에서 불러오기
        hp = playerData.hpData;
        damage = playerData.damageData;
        moveSpeed = playerData.moveSpeedData;
        exp = playerData.expData;
        ifExp = playerData.ifExpData;
        level = playerData.levelData;
        gold = 0;   // 인게임 골드만 표시하기 위함

        rb = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // Start
    private void Start()
    {

    }

    // Update
    private void Update()
    {
        LoadHpUI();     // 현재 Hp 표시
        LoadGoldUI();   // 현재 Gold 표시
        LoadLevelUI();  // 현재 Level 표시
        ExpL();         // 현재 경험치가 조건을 충족하면 레벨 업

        input.x = Input.GetAxisRaw("Horizontal");   // ←→ or AD Input 받아서 좌우 이동
        input.y = Input.GetAxisRaw("Vertical");     // ↑↓ or WS Input 받아서 상하 이동
        velocity = input.normalized * moveSpeed;    // ↖↗↙↘ 대각선 이동

        if (Input.GetKeyDown(KeyCode.K))            // K 누르면 공격 ( 쿨타임 추가 예정 )
        {
            PlayerAttack();
        }

        if (input.sqrMagnitude > 0.01f)                     // Sprite 를 이동 방향에 따라 변경
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                if (input.x > 0)
                {
                    sR.sprite = spriteRight;
                }
                else if (input.x < 0)
                {
                    sR.sprite = spriteLeft;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    // 현재 골드 표시
    public void LoadGoldUI()
    {
        uiGold.text = $"Gold: {gold}";
    }

    public void LoadLevelUI()
    {
        uiLevel.text = $"Level: {level}";
    }

    // 현재 HP 표시
    // 최대 체력 60 기준
    public void LoadHpUI()
    {
        int fullHeart = hp / 2;
        bool halfHeart = hp % 2 != 0;

        string line1 = "";
        string line2 = "";
        string line3 = "";

        for (int i = 0; i < Mathf.Min(fullHeart, 10); i++)
        {
            line1 += "♥";
        }
        for (int i = 10; i < Mathf.Min(fullHeart, 20); i++)
        {
            line2 += "♥";
        }
        for (int i = 20; i < Mathf.Min(fullHeart, 30); i++)
        {
            line3 += "♥";
        }

        if (halfHeart)
        {
            if (fullHeart < 10)
            {
                line1 += "♡";
            }
            else if (fullHeart < 20)
            {
                line2 += "♡";
            }
            else
            {
                line3 += "♡";
            }
        }

        uiHp.text = line1 + "\n" + line2 + "\n" + line3;
    }

    public void AddGold(int value)
    {
        gold += value;
        LoadGoldUI();
        Debug.Log("Gold: " + (gold - value) + " -> " + $"Gold: {playerData.goldData} (+{value})");
    }

    public void SaveGoldToData()
    {
        PlayerDataManager.instance.playerData.goldData += gold;
        Debug.Log($"[저장됨] ToTal Gold: {PlayerDataManager.instance.playerData.goldData}");
    }

    public void AddExp(int value)
    {
        exp += value;
        ExpL();
    }

    private int levelUps = 0;

    public void ExpL()
    {
        while (exp >= ifExp)
        {
            exp -= ifExp;
            level += 1;         // 레벨 1 증가
            ifExp *= 2;         // 레벨을 업 할수록 필요 경험치 2배 증가
            levelUps++;
            Debug.Log($"Level Up / Level {level}");
        }
        ExpBarUpdate();
        if (levelUps > 0 && !PlayerLevelStatUI.instance.isOpen)
        {
            PlayerLevelStatUI.instance.LevelUpOpen();
        }
    }

    public bool HaslevelUps()
    {
        return levelUps > 0;
    }

    public void MlevelUps()
    {
        levelUps--;
    }

    //public void LevelUp()
    //{
    //    level += 1;         // 레벨 1 증가
    //    ifExp *= 2;         // 레벨을 업 할수록 필요 경험치 2배 증가
    //    Debug.Log($"Level Up / Level {level}");

    //    PlayerLevelStatUI.instance.LevelUpOpen();
    //}

    private void ExpBarUpdate()
    {
        if (expBarFill != null)
        {
            expBarFill.fillAmount = (float)exp / ifExp; // float 로 변경 이유 = int / int = 소수점 무시
        }
    }

    public void PlayerAttack()
    {

        Vector2 attackD = new Vector2(input.x, input.y);
        if (attackD == Vector2.zero)                        // x, y 전부 0일 경우
        {
            return;
        }

        GameObject obj = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        PlayerAttack playerAttack = obj.GetComponent<PlayerAttack>();
        if (playerAttack != null)
        {
            playerAttack.Init(damage, attackD);
        }
    }

    // 플레이어 체력 감소
    public void PlayerHpM(int hpDamage)
    {
        hp -= hpDamage;
        if (hp <= 0)
        {
            Debug.Log("Player Die");
            SaveGoldToData();   // 사망 = 게임 종료 -> 골드 저장
            Destroy(gameObject);
            SceneManager.LoadScene("Main");
        }
    }

    public void SaveCurrentStatToData() // 씬 넘어가기 전에 현재 스텟 저장
    {
        PlayerData data = PlayerDataManager.instance.playerData;
        data.hpData = hp;
        data.damageData = damage;
        data.moveSpeedData = moveSpeed;
        data.expData = exp;
        data.ifExpData = ifExp;
        data.levelData = level;
    }

    // OnTrigger Collider / collision.CompareTag
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Portal_Main"))
        {
            SaveGoldToData();   // 골드 저장
            Destroy(gameObject);    // 플레이어 삭제
            SceneManager.LoadScene("Main"); // 메인으로 이동
        }
    }
}