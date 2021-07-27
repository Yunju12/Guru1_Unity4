using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlatGameManager : MonoBehaviour
{
    // ���� ��ü ����
    public int totalPoint;

    // �ش� �������� ����
    public int stagePoint;

    // �������� �ε���
    public int stageIndex;

    // platplayer HP
    public int Hp = 10;

    // player ����
    public PlatPlayerMove PlatPlayer;
    
   // �� ��������
   public void NextStage()
    {
        // �������� �ε��� �ø�
        stageIndex++;
        
        // ��ü ������ �� ������������ ���� ���� �߰�
        totalPoint += stagePoint;
        stagePoint = 0; // ���ο� �������������� ���� 0���� �ʱ�ȭ
    }

    // Hp ������ ���
    public void HpDown()
    {
        // Hp 0�̻��� ���, Hp -1��Ŵ
        if (Hp > 1)
            Hp--;

        // Hp 0�� ���
        else
        {
            // Player Die ȣ��
            PlatPlayer.OnDie();

            // ��� ��� UI
            Debug.Log("�׾����ϴ�!");
        }

    }

    // ���Ͻ� Hp -1��
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Hp�� 1�̻��� ���
            if (Hp > 1)
            {
                // ���Ͻ� player ��ġ ����
                collision.attachedRigidbody.velocity = Vector2.zero; // ���ϼӵ� 0����
                collision.transform.position = new Vector3(0, 0, 0); // (0,0,0) ��ġ�� �̵�
            }

            // ü�� 1���Ҵµ� ������ ��� Hp 1���� �� ����ġ �ǵ����� �������� ����
            HpDown();
        }
    }
}
