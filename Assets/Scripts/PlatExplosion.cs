using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatExplosion : MonoBehaviour
{
    // ���� �ð�
    float currentTime = 0;

    // ���� �Ŵ��� ����
    public PlatGameManager PlatGameManager;

    void Start()
    {

    }

    void Update()
    { 
        // ������ ���κ��� 1�ʰ� ����Ǹ� �������.
        if (currentTime >= 0.8f)
        {
            Destroy(gameObject);
        }
        currentTime += Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� Enemy �� �ε�����, ����Ʈ 100���� ��� �ε��� Enemy �� �״´�.
        if (collision.CompareTag("Enemy"))
        {
            //PlatGameManager.stagePoint += 100;
            collision.GetComponent<PlatEnemyMove>().OnDamaged();
        }
    }
}
