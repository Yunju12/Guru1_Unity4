using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 총알공장
    public GameObject bulletFactory;

    // 발사
    public GameObject firePosition;

    // 오디오 소스 컴포넌트
    private AudioSource audio_pf;

    // 오디오 클립 변수
    public AudioClip clip;

    GameManager gm2;

    void Start()
    {
        audio_pf = GetComponent<AudioSource>();

        gm2 = GetComponent<GameManager>();
    }

    void Update()
    {
        // 게임 상태가 게임 중 상태가 아니면 업데이트 함수를 중단한다.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // * 공격
        // 만약 사용자가 발사버튼(Ctrl)을 누르면
        // 총알 공장에서 총알을 만들고, 총알을 발사(총구에 배치)한다.
        if (Input.GetButtonDown("Fire1"))
        {
            audio_pf.PlayOneShot(clip);
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = firePosition.transform.position;
        }
    }
}
