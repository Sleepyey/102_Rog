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

        // ���� ���� �� �÷��̾� �����͸� PlayerData���� �ҷ�����
        hp = playerData.hpData;
        damage = playerData.damageData;
        moveSpeed = playerData.moveSpeedData;
        exp = playerData.expData;
        ifExp = playerData.ifExpData;
        level = playerData.levelData;
        gold = 0;   // �ΰ��� ��常 ǥ���ϱ� ����

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
        LoadHpUI();     // ���� Hp ǥ��
        LoadGoldUI();   // ���� Gold ǥ��
        LoadLevelUI();  // ���� Level ǥ��
        ExpL();         // ���� ����ġ�� ������ �����ϸ� ���� ��

        input.x = Input.GetAxisRaw("Horizontal");   // ��� or AD Input �޾Ƽ� �¿� �̵�
        input.y = Input.GetAxisRaw("Vertical");     // ��� or WS Input �޾Ƽ� ���� �̵�
        velocity = input.normalized * moveSpeed;    // �آ֢ע� �밢�� �̵�

        if (Input.GetKeyDown(KeyCode.K))            // K ������ ���� ( ��Ÿ�� �߰� ���� )
        {
            PlayerAttack();
        }

        if (input.sqrMagnitude > 0.01f)                     // Sprite �� �̵� ���⿡ ���� ����
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

    // ���� ��� ǥ��
    public void LoadGoldUI()
    {
        uiGold.text = $"Gold: {gold}";
    }

    public void LoadLevelUI()
    {
        uiLevel.text = $"Level: {level}";
    }

    // ���� HP ǥ��
    // �ִ� ü�� 60 ����
    public void LoadHpUI()
    {
        int fullHeart = hp / 2;
        bool halfHeart = hp % 2 != 0;

        string line1 = "";
        string line2 = "";
        string line3 = "";

        for (int i = 0; i < Mathf.Min(fullHeart, 10); i++)
        {
            line1 += "��";
        }
        for (int i = 10; i < Mathf.Min(fullHeart, 20); i++)
        {
            line2 += "��";
        }
        for (int i = 20; i < Mathf.Min(fullHeart, 30); i++)
        {
            line3 += "��";
        }

        if (halfHeart)
        {
            if (fullHeart < 10)
            {
                line1 += "��";
            }
            else if (fullHeart < 20)
            {
                line2 += "��";
            }
            else
            {
                line3 += "��";
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
        Debug.Log($"[�����] ToTal Gold: {PlayerDataManager.instance.playerData.goldData}");
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
            level += 1;         // ���� 1 ����
            ifExp *= 2;         // ������ �� �Ҽ��� �ʿ� ����ġ 2�� ����
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
    //    level += 1;         // ���� 1 ����
    //    ifExp *= 2;         // ������ �� �Ҽ��� �ʿ� ����ġ 2�� ����
    //    Debug.Log($"Level Up / Level {level}");

    //    PlayerLevelStatUI.instance.LevelUpOpen();
    //}

    private void ExpBarUpdate()
    {
        if (expBarFill != null)
        {
            expBarFill.fillAmount = (float)exp / ifExp; // float �� ���� ���� = int / int = �Ҽ��� ����
        }
    }

    public void PlayerAttack()
    {

        Vector2 attackD = new Vector2(input.x, input.y);
        if (attackD == Vector2.zero)                        // x, y ���� 0�� ���
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

    // �÷��̾� ü�� ����
    public void PlayerHpM(int hpDamage)
    {
        hp -= hpDamage;
        if (hp <= 0)
        {
            Debug.Log("Player Die");
            SaveGoldToData();   // ��� = ���� ���� -> ��� ����
            Destroy(gameObject);
            SceneManager.LoadScene("Main");
        }
    }

    public void SaveCurrentStatToData() // �� �Ѿ�� ���� ���� ���� ����
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
            SaveGoldToData();   // ��� ����
            Destroy(gameObject);    // �÷��̾� ����
            SceneManager.LoadScene("Main"); // �������� �̵�
        }
    }
}