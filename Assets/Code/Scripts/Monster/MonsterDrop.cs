using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DropType { Gold, Exp }

[RequireComponent(typeof(SpriteRenderer))]  // SpriteRenderer 자동 추가
public class MonsterDrop : MonoBehaviour
{
    public DropType dropType;
    public int amount;

    private float dTime = 60f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // ? = 조건에 충족 못해도 에러 대신 null 반환
    }

    private void Update()
    {
        if (player != null)
        {
            float dist = Vector2.Distance(transform.position, player.position); // 플레이어와의 거리 측정
            if (dist < 1.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, 1f * Time.deltaTime); // 목표 위치로 일정 속도로 이동
            }
        }

        if (dTime > 0)
        {
            dTime -= Time.deltaTime;
        }
        else if (dTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Init(int _amount, DropType _dropType)
    {
        amount = _amount;
        dropType = _dropType;

        AddRigidbodyCollider();

        Vector2 randomPosition = Random.insideUnitCircle.normalized * Random.Range(0f, 0.5f);   // Random.insideUnitCircle = 반지름 1의 원 안에서 랜덤한 점 생성, normalized를 이용하여 방향만 추출
        transform.position += new Vector3(randomPosition.x, randomPosition.y, 0f);

        StartCoroutine(JumpEffect());   // StartCoroutine = Coroutine 함수를 실행시켜 줌
    }

    private void AddRigidbodyCollider()     // Rigidbody2D 및 CircleCollider2D 자동 추가
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();       // GetComponent = 현재 오브젝트에 적용되어 있으면 적용 없으면 null
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = true;                            // 물리 설정
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col == null)
        {
            CircleCollider2D circle = gameObject.AddComponent<CircleCollider2D>();
            circle.isTrigger = true;
            circle.radius = 0.15f;
        }
    }

    private IEnumerator JumpEffect()    // IEnumerator = Coroutine 의 함수 / 드롭 시 잠시 위로 뜨도록 구현
    {
        Vector3 jumpStart = transform.position;
        Vector3 jumpPeak = jumpStart + new Vector3(0f, 0.4f, 0f);

        float duration = 0.3f;
        float timer = 0f;

        // Up
        while (timer < duration)
        {
            transform.position = Vector3.Lerp(jumpStart, jumpPeak, timer / (duration)); // Lerp(1, 2, 3) = 1에서 2까지 3으로 이동
            timer += Time.deltaTime;
            yield return null;  // yield = Coroutine 안에서 잠시 정지 / 한 프레임 뒤 실행
        }

        // Down
        timer = 0f;
        while (timer < duration)
        {
            transform.position = Vector3.Lerp(jumpPeak, jumpStart, timer / (duration));
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = jumpStart;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                if (dropType == DropType.Gold)
                {
                    player.AddGold(amount);
                }
                else if (dropType == DropType.Exp)
                {
                    player.AddExp(amount);
                }
            }
            Destroy(gameObject);
        }
    }
}
