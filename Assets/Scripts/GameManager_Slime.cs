using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Slime : MonoBehaviour
{
    // 게임 전체 점수
    public int totalPoint;

    public int bossPoint = 500;

    public Text UIPoint;

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
    PlayerMove_Slime pms;

    // 보스 몬스터 컴포넌트 변수
    BossMonster_Slime bms;

    // 싱글턴
    public static GameManager_Slime gm;

    // 애니메이션 변수
    Animator ani;

    // 오디오 소스 컴포넌트
    AudioSource audio_gms;

    // 오디오 클립 변수
    public AudioClip ready;

    // 오디오 클립 변수
    public AudioClip start;

    public AudioClip enemyBGM;

    public AudioClip bossBGM;

    public AudioClip gameClear;

    public AudioClip gameOver;

    public GameObject player;

    //게임 결과 UI
    public GameObject GameOverUI;
    public GameObject ClearUI;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }

        audio_gms = GetComponent<AudioSource>();
    }

    void Start()
    {
        // 초기 게임 상태는 준비 상태로 설정한다.
        gState = GameState.Ready;

        // 플레이어 애니메이션 컴포넌트를 받아온다.
        ani = GetComponent<Animator>();

        pms = GetComponent<PlayerMove_Slime>();

        //보스몬스터 오브젝트를 검색
        bms = GetComponent<BossMonster_Slime>();

        // 게임 시작 코루틴 함수를 실행한다.
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        // Ready...  라는 문구를 표시한다.
        stateLabel.text = "Ready...";
        audio_gms.PlayOneShot(ready);

        // Ready 문구의 색상을 주황색으로 표시한다.
        stateLabel.color = new Color32(233, 182, 12, 255);

        // 2초간 대기한다.
        yield return new WaitForSeconds(2.0f);

        // Start! 라는 문구로 변경한다.
        stateLabel.text = "Start!";
        audio_gms.PlayOneShot(start);

        // 0.5초간 대기한다.
        yield return new WaitForSeconds(0.5f);

        // Start 문구를 지운다.
        stateLabel.text = "";

        // 게임의 상태를 준비 상태에서 실행 상태로 전환한다.
        gState = GameState.Run;
        audio_gms.clip = enemyBGM;
        audio_gms.Play();
    }

    void Update()
    {
        UIPoint.text = (totalPoint).ToString();

        // 게임 상태가 게임 중 상태가 아니면 업데이트 함수를 중단
        if (gm.gState != GameState.Run)
        {
            return;
        }

        // 만약 플레이어의 hp가 0 이하로 떨어지면
        if (PlayerMove_Slime.playerHp <= 0)
        {
            //audio.Stop();
            //audio.PlayOneShot(gameOver);

            player.GetComponent<PlayerMove_Slime>().Die();

            //게임 오버 옵션 메뉴 창을 활성화한다
            GameOverUI.SetActive(true);

            // 게임 상태를 게임 오버 상태로 전환한다.
            gState = GameState.GameOver;
        }

        // 만약 보스의 hp가 0 이하로 떨어지면
        else if (BossMonster_Slime.bossHp <= 0)
        {
            totalPoint += bossPoint;

            //audio.Stop();
            //audio.PlayOneShot(gameClear);

            //클리어 옵션 메뉴 창을 활성화한다
            ClearUI.SetActive(true);

            // 게임 상태를 게임 클리어 상태로 전환한다.
            gState = GameState.GameClear;
        }
    }
}