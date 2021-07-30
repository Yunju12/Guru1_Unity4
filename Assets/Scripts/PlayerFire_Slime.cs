using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire_Slime : MonoBehaviour
{
    // �Ѿ˰���
    public GameObject bulletFactory;

    // �߻�
    public GameObject firePosition;

    // ����� �ҽ� ������Ʈ
    private AudioSource audio_pfs;

    // ����� Ŭ�� ����
    public AudioClip clip;

    GameManager_Slime gm2;

    void Start()
    {
        audio_pfs = GetComponent<AudioSource>();

        gm2 = GetComponent<GameManager_Slime>();
    }

    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ��Ѵ�.
        if (GameManager_Slime.gm.gState != GameManager_Slime.GameState.Run)
        {
            return;
        }

        // * ����
        // ���� ����ڰ� �߻��ư(Ctrl)�� ������
        // �Ѿ� ���忡�� �Ѿ��� �����, �Ѿ��� �߻�(�ѱ��� ��ġ)�Ѵ�.
        if (Input.GetButtonDown("Fire1"))
        {
            audio_pfs.PlayOneShot(clip);
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = firePosition.transform.position;
        }
    }
}
