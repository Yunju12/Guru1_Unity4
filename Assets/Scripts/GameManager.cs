using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 게임 전체 점수
    public int totalPoint = 0;

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
    PlayerMove pm;

    // 보스 몬스터 컴포넌트 변수
    BossMonster bm;

    // 싱글턴
    public static GameManager gm;

    // 애니메이션 변수
    Animator ani;

    // 오디오 소스 컴포넌트
    AudioSource audio_gm;

    // 오디오 클립 변수
    public AudioClip ready;

    // 오디오 클립 변수
    public AudioClip start;

    public AudioClip enemyBGM;

    public AudioClip bossBGM;

    public AudioClip gameClear;

    public AudioClip gameOver;

    public GameObject player;

    // 포션 딜레이 bool 변수
    public bool isDelay;

    // 딜레이 시간 변수
    public float delayTime = 3.0f;

    // 현재 시간 변수
    public float currentTime;

    //게임 결과 UI
    public GameObject GameOverUI;
    public GameObject ClearUI;

    private void Awake()
    {
        

        if (gm == null)
        {
            gm = this;
        }

        audio_gm = GetComponent<AudioSource>();
    }

    void Start()
    {
        // 초기 게임 상태는 준비 상태로 설정한다.
        gState = GameState.Ready;

        // 플레이어 애니메이션 컴포넌트를 받아온다.
        ani = GetComponent<Animator>();

        pm = GetComponent<PlayerMove>();

        //보스몬스터 오브젝트를 검색
        bm = GetComponent<BossMonster>();

        // 게임 시작 코루틴 함수를 실행한다.
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        // Ready...  라는 문구를 표시한다.
        stateLabel.text = "Ready...";
        audio_gm.PlayOneShot(ready);

        // Ready 문구의 색상을 주황색으로 표시한다.
        stateLabel.color = new Color32(233, 182, 12, 255);

        // 2초간 대기한다.
        yield return new WaitForSeconds(2.0f);

        // Start! 라는 문구로 변경한다.
        stateLabel.text = "Start!";
        audio_gm.PlayOneShot(start);

        // 0.5초간 대기한다.
        yield return new WaitForSeconds(0.5f);

        // Start 문구를 지운다.
        stateLabel.text = "";

        // 게임의 상태를 준비 상태에서 실행 상태로 전환한다.
        gState = GameState.Run;
        audio_gm.clip = enemyBGM;
        audio_gm.Play();

        totalPoint = 0;
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
        if (PlayerMove.playerHp <= 0)
        {
            if (EggHp.eggHp <= 0)
            {
                //씬 전환
                SceneManager.LoadScene("BadEnding_2");
            }
            else
            {
                //audio.Stop();
                //audio.PlayOneShot(gameOver);

                player.GetComponent<PlayerMove>().Die();

                Enemy.enemyDeath = 0;

                //게임 오버 옵션 메뉴 창을 활성화한다
                GameOverUI.SetActive(true);

                // 게임 상태를 게임 오버 상태로 전환한다.
                gState = GameState.GameOver;
            }
        }

        // 만약 보스의 hp가 0 이하로 떨어지면
        else if (BossMonster.bossHp <= 0)
        {
            totalPoint += bossPoint;
            Board_PlayerMove.totalScore += totalPoint;

            Enemy.enemyDeath = 0;

            //클리어 옵션 메뉴 창을 활성화한다
            ClearUI.SetActive(true);

            // 게임 상태를 게임 클리어 상태로 전환한다.
            gState = GameState.GameClear;
        }

        // 만약 포션 딜레이 bool 변수가 참이면,
        if (isDelay)
        {
            // 현재 시간 변수의 값이 1초가 흐를 때마다 1 만큼 증가한다.
            currentTime += Time.deltaTime;

            // 만약 현재 시간 변수가 딜레이 시간 변수와 같거나 그보다 크다면,
            if (currentTime >= delayTime)
            {
                // 현재 시간 변수의 값을 0 으로 하고 포션 딜레이 bool 변수의 값을 거짓으로 한다.
                currentTime = 0;
                isDelay = false;
            }
        }

        // 만약 A 키를 누르면,
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 포션 아이템을 갖고 있을 경우,
            if (EggHp.potionCount > 0)
            {
                // 포션 딜레이 bool 변수가 거짓이거나 플레이어의 체력이 10 이하 라면,
                if (isDelay == false && PlayerMove.playerHp < 10)
                {
                    // isDelay의 값을 참으로 전환하고, 포션 아이템을 하나 소모하고, 체력을 회복한다.
                    isDelay = true;
                    EggHp.potionCount--;
                    Heal();
                }
                // 그렇지 않다면(포션 딜레이 함수가 참이거나 플레이어의 체력이 10 이라면),
                else
                {
                    // 값을 반환한다.
                    return;
                }
            }
        }
    }

    // 체력 회복 함수
    void Heal()
    {
        // 플레이어의 체력을 5만큼 회복한다.
        PlayerMove.playerHp += 5;

        // 만약 플레이어의 체력이 maxHp 를 초과하면,
        if (PlayerMove.playerHp > player.GetComponent<PlayerMove>().maxHp)
        {
            // 플레이어의 체력을 maxHp 만큼으로 바꾼다.
            PlayerMove.playerHp = player.GetComponent<PlayerMove>().maxHp;
        }
    }
}