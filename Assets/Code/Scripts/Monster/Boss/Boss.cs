using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject portalPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BossDie()
    {
        Instantiate(portalPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
