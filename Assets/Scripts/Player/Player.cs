using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] Sprite spriteUp;
    [SerializeField] Sprite spriteDown;
    [SerializeField] Sprite spriteLeft;
    [SerializeField] Sprite spriteRight;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI uiGold;
    [SerializeField] private TextMeshProUGUI uiHp;

    [Header("Stat")]
    public int damage;
    public int hp;
    public float attackSpeed;
    public float moveSpeed;
    public int ex;
    public int ifEx;
    public int gold;

    Rigidbody2D rb;
    SpriteRenderer sR;

    Vector2 input;
    Vector2 velocity;

    // Awake
    private void Awake()
    {
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
        LoadHpUI(); // 현재 HP 표시

        input.x = Input.GetAxisRaw("Horizontal");   // ←→ or AD Input 받아서 좌우 이동
        input.y = Input.GetAxisRaw("Vertical");     // ↑↓ or WS Input 받아서 상하 이동

        velocity = input.normalized * moveSpeed;    // ↖↗↙↘ 대각선 이동

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
            else
            {
                if (input.y > 0)
                {
                    sR.sprite = spriteUp;
                }
                else if(input.y < 0)
                {
                    sR.sprite = spriteDown;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    // 현재 HP 표시
    public void LoadHpUI()
    {
        if (hp == 1)
        {
            uiHp.text = "♡";
        }
        else if (hp == 2)
        {
            uiHp.text = "♥";
        }
        else if (hp == 3)
        {
            uiHp.text = "♥♡";
        }
        else if (hp == 4)
        {
            uiHp.text = "♥♥";
        }
        else if (hp == 5)
        {
            uiHp.text = "♥♥♡";
        }
        else if (hp == 6)
        {
            uiHp.text = "♥♥♥";
        }
        else if (hp == 7)
        {
            uiHp.text = "♥♥♥♡";
        }
        else if (hp == 8)
        {
            uiHp.text = "♥♥♥♥";
        }
        else if (hp == 9)
        {
            uiHp.text = "♥♥♥♥♡";
        }
        else if (hp == 10)
        {
            uiHp.text = "♥♥♥♥♥";
        }
        else if (hp == 11)
        {
            uiHp.text = "♥♥♥♥♥♡";
        }
        else if (hp == 12)
        {
            uiHp.text = "♥♥♥♥♥♥";
        }
        else if (hp == 13)
        {
            uiHp.text = "♥♥♥♥♥♥♡";
        }
        else if (hp == 14)
        {
            uiHp.text = "♥♥♥♥♥♥♥";
        }
        else if (hp == 15)
        {
            uiHp.text = "♥♥♥♥♥♥♥♡";
        }
        else if (hp == 16)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥";
        }
        else if (hp == 17)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♡";
        }
        else if (hp == 18)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥";
        }
        else if (hp == 19)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♡";
        }
        else if (hp == 20)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥";
        }
        else if (hp == 21)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♡";
        }
        else if (hp == 22)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥";
        }
        else if (hp == 23)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♡";
        }
        else if (hp == 24)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥";
        }
        else if (hp == 25)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♡";
        }
        else if (hp == 26)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥";
        }
        else if (hp == 27)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♡";
        }
        else if (hp == 28)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥";
        }
        else if (hp == 29)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♡";
        }
        else if (hp == 30)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥";
        }
        else if (hp == 31)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♡";
        }
        else if (hp == 32)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥";
        }
        else if (hp == 33)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♡";
        }
        else if (hp == 34)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥";
        }
        else if (hp == 35)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♡";
        }
        else if (hp == 36)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥";
        }
        else if (hp == 37)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♡";
        }
        else if (hp == 38)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥";
        }
        else if (hp == 39)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♡";
        }
        else if (hp == 40)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥";
        }
        else if (hp == 41)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♡";
        }
        else if (hp == 42)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥";
        }
        else if (hp == 43)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♡";
        }
        else if (hp == 44)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥";
        }
        else if (hp == 45)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♡";
        }
        else if (hp == 46)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥";
        }
        else if (hp == 47)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♡";
        }
        else if (hp == 48)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥";
        }
        else if (hp == 49)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♡";
        }
        else if (hp == 50)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥";
        }
        else if (hp == 51)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♡";
        }
        else if (hp == 52)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥";
        }
        else if (hp == 53)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♡";
        }
        else if (hp == 54)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥";
        }
        else if (hp == 55)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♡";
        }
        else if (hp == 56)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥";
        }
        else if (hp == 57)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♡";
        }
        else if (hp == 58)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥";
        }
        else if (hp == 59)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♡";
        }
        else if (hp == 60)
        {
            uiHp.text = "♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥" + "\n♥♥♥♥♥♥♥♥♥♥";
        }
    }

    // 플레이어 체력 감소
    public void PlayerHpM(int hpDamage)
    {
        hp -= hpDamage;
    }

    // OnTrigger Collider / collision.CompareTag
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster_Attack"))
        {

        }
    }
}