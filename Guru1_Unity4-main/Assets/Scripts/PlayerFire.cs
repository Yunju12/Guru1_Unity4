using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // �Ѿ˰���
    public GameObject bulletFactory;

    // �߻�
    public GameObject firePosition;

    // ����� �ҽ� ������Ʈ
    private AudioSource audio;

    // ����� Ŭ�� ����
    public AudioClip clip;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ��Ѵ�.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // * ����
        // ���� ����ڰ� �߻��ư(Ctrl)�� ������
        // �Ѿ� ���忡�� �Ѿ��� �����, �Ѿ��� �߻�(�ѱ��� ��ġ)�Ѵ�.
        if (Input.GetButtonDown("Fire1"))
        {
            audio.PlayOneShot(clip);
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = firePosition.transform.position;
        }
    }
}
