using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 총알공장
    public GameObject bulletFactory;

    // 발사
    public GameObject firePosition;

    // 사운드 변수
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 게임 상태가 게임 중 상태가 아니면 업데이트 함수를 중단한다.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // 사용자가 발사버튼(Ctrl)을 누르면 총알 발사하기
        // 만약 사용자가 발사버튼을 누르면
        // 총알 공장에서 총알을 만들고, 총알을 발사(총구에 배치)하고 싶다.
        if (Input.GetButtonDown("Fire1"))
        {
            audioSource.Play();
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = firePosition.transform.position;
        }
    }
}
