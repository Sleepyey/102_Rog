using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] string sceneLevel = "Level_2";

    // Awake
    private void Awake()
    {
        // BoxCollider2D �ڵ� �߰� �� Ʈ���� ����
        BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
        if (col == null)
        {
            col = gameObject.AddComponent<BoxCollider2D>();
        }
        col.isTrigger = true;

        if (sceneLevel == "Main")
        {
            gameObject.tag = "Portal_Main";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.SaveCurrentStatToData(); // �� �̵� �� �÷��̾� ���� ���� ����
            }
            SceneManager.LoadScene(sceneLevel);
        }
    }
}
