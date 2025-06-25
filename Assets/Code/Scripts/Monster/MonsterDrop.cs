using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DropType { Gold, Exp }

[RequireComponent(typeof(SpriteRenderer))]  // SpriteRenderer �ڵ� �߰�
public class MonsterDrop : MonoBehaviour
{
    public DropType dropType;
    public int amount;

    private float dTime = 60f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // ? = ���ǿ� ���� ���ص� ���� ��� null ��ȯ
    }

    private void Update()
    {
        if (player != null)
        {
            float dist = Vector2.Distance(transform.position, player.position); // �÷��̾���� �Ÿ� ����
            if (dist < 1.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, 1f * Time.deltaTime); // ��ǥ ��ġ�� ���� �ӵ��� �̵�
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

        Vector2 randomPosition = Random.insideUnitCircle.normalized * Random.Range(0f, 0.5f);   // Random.insideUnitCircle = ������ 1�� �� �ȿ��� ������ �� ����, normalized�� �̿��Ͽ� ���⸸ ����
        transform.position += new Vector3(randomPosition.x, randomPosition.y, 0f);

        StartCoroutine(JumpEffect());   // StartCoroutine = Coroutine �Լ��� ������� ��
    }

    private void AddRigidbodyCollider()     // Rigidbody2D �� CircleCollider2D �ڵ� �߰�
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();       // GetComponent = ���� ������Ʈ�� ����Ǿ� ������ ���� ������ null
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = true;                            // ���� ����
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col == null)
        {
            CircleCollider2D circle = gameObject.AddComponent<CircleCollider2D>();
            circle.isTrigger = true;
            circle.radius = 0.15f;
        }
    }

    private IEnumerator JumpEffect()    // IEnumerator = Coroutine �� �Լ� / ��� �� ��� ���� �ߵ��� ����
    {
        Vector3 jumpStart = transform.position;
        Vector3 jumpPeak = jumpStart + new Vector3(0f, 0.4f, 0f);

        float duration = 0.3f;
        float timer = 0f;

        // Up
        while (timer < duration)
        {
            transform.position = Vector3.Lerp(jumpStart, jumpPeak, timer / (duration)); // Lerp(1, 2, 3) = 1���� 2���� 3���� �̵�
            timer += Time.deltaTime;
            yield return null;  // yield = Coroutine �ȿ��� ��� ���� / �� ������ �� ����
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
