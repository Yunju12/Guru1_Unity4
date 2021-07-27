using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 게임 상태 상수
    public enum GameState
    {
        Ready,
        Run,
        GameOver,
        GameClear
    }

    // 게임 상태 변수
    public GameState gState;

    // UI 텍스트 변수
    public Text stateLabel;

    // 플레이어 무브 컴포넌트 변수
    PlayerMove pm;

    // 보스 몬스터 컴포넌트 변수
    BossMonster bm;

    // 싱글턴
    public static GameManager gm;

    // 애니메이션 변수
    Animator ani;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    void Start()
    {
        // 초기 게임 상태는 준비 상태로 설정한다.
        gState = GameState.Ready;

        // 플레이어 애니메이션 컴포넌트를 받아온다.
        ani = GetComponent<Animator>();

        // 게임 시작 코루틴 함수를 실행한다.
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        // Ready...  라는 문구를 표시한다.
        stateLabel.text = "Ready...";

        // Ready 문구의 색상을 주황색으로 표시한다.
        stateLabel.color = new Color32(233, 182, 12, 255);

        // 2초간 대기한다.
        yield return new WaitForSeconds(2.0f);

        // Start! 라는 문구로 변경한다.
        stateLabel.text = "Start!";

        // 0.5초간 대기한다.
        yield return new WaitForSeconds(0.5f);

        // Start 문구를 지운다.
        stateLabel.text = "";

        // 게임의 상태를 준비 상태에서 실행 상태로 전환한다.
        gState = GameState.Run;
    }

    void Update()
    {
        // 플레이어 오브젝트를 검색
        PlayerMove pm = GameObject.Find("Player").GetComponent<PlayerMove>();

        // 보스몬스터 오브젝트를 검색
        BossMonster bm = GameObject.Find("BossMonster").GetComponent<BossMonster>();

        // 만약 플레이어의 hp가 0 이하로 떨어지면
        if (PlayerMove.playerHp <= 0)
        {
            // 게임 오버 문구를 출력한다.
            stateLabel.text = "Game Over...";

            // 게임 오버 문구의 색상은 붉은색으로 설정한다.
            stateLabel.color = new Color32(255, 0, 0, 255);

            // 게임 상태를 게임 오버 상태로 전환한다.
            //gState = GameState.GameOver;
        }

        // 만약 보스의 hp가 0 이하로 떨어지면
        else if (bm.bossHp <= 0)
        {
            // 성공 문구를 풀력한다.
            stateLabel.text = "Clear!";

            // 클리어 문구의 색상은 노란색으로 설정한다.
            stateLabel.color = new Color32(255, 255, 0, 255);

            // 게임 상태를 게임 클리어 상태로 전환한다.
            gState = GameState.GameClear;
        }
    }
}
