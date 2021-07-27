using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // �Ѿ˰���
    public GameObject bulletFactory;

    // �߻�
    public GameObject firePosition;

    // ���� ����
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ��Ѵ�.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // ����ڰ� �߻��ư(Ctrl)�� ������ �Ѿ� �߻��ϱ�
        // ���� ����ڰ� �߻��ư�� ������
        // �Ѿ� ���忡�� �Ѿ��� �����, �Ѿ��� �߻�(�ѱ��� ��ġ)�ϰ� �ʹ�.
        if (Input.GetButtonDown("Fire1"))
        {
            audioSource.Play();
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = firePosition.transform.position;
        }
    }
}
