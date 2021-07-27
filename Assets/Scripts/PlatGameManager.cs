using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlatGameManager : MonoBehaviour
{
    // 게임 전체 점수
    public int totalPoint;

    // 해당 스테이지 점수
    public int stagePoint;

    // 스테이지 인덱스
    public int stageIndex;

    // platplayer HP
    public int Hp = 10;

    // player 변수
    public PlatPlayerMove PlatPlayer;
    
   // 새 스테이지
   public void NextStage()
    {
        // 스테이지 인덱스 늘림
        stageIndex++;
        
        // 전체 점수에 현 스테이지에서 얻음 점수 추가
        totalPoint += stagePoint;
        stagePoint = 0; // 새로운 스테이지에서의 점수 0으로 초기화
    }

    // Hp 감소의 경우
    public void HpDown()
    {
        // Hp 0이상일 경우, Hp -1시킴
        if (Hp > 1)
            Hp--;

        // Hp 0일 경우
        else
        {
            // Player Die 호출
            PlatPlayer.OnDie();

            // 결과 출력 UI
            Debug.Log("죽었습니다!");
        }

    }

    // 낙하시 Hp -1됨
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Hp가 1이상일 경우
            if (Hp > 1)
            {
                // 낙하시 player 위치 변경
                collision.attachedRigidbody.velocity = Vector2.zero; // 낙하속도 0으로
                collision.transform.position = new Vector3(0, 0, 0); // (0,0,0) 위치로 이동
            }

            // 체력 1남았는데 떨어질 경우 Hp 1감소 후 원위치 되돌리기 실행하지 않음
            HpDown();
        }
    }
}
