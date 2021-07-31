using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public bool isDelay;
    public float delayTime = 3.0f;
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
            //audio.Stop();
            //audio.PlayOneShot(gameOver);

            player.GetComponent<PlayerMove>().Die();

            //게임 오버 옵션 메뉴 창을 활성화한다
            GameOverUI.SetActive(true);

            // 게임 상태를 게임 오버 상태로 전환한다.
            gState = GameState.GameOver;
        }

        // 만약 보스의 hp가 0 이하로 떨어지면
        else if (BossMonster.bossHp <= 0)
        {
            //audio.Stop();
            //audio.PlayOneShot(gameClear);

            totalPoint += bossPoint;

            //클리어 옵션 메뉴 창을 활성화한다
            ClearUI.SetActive(true);

            // 게임 상태를 게임 클리어 상태로 전환한다.
            gState = GameState.GameClear;
        }

        if (isDelay)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= delayTime)
            {
                currentTime = 0;
                isDelay = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isDelay == true || PlayerMove.playerHp == 10)
            {
                Debug.Log("아직 포션을 사용할 수 없습니다.");
                return;
            }
            else
            {
                // isDelay를 true로 전환 및 체력 회복
                isDelay = true;
                Heal();
                //egg.("potion" + potionCount).SetActive(false);   
            }
        }

        
    }

    void Heal()
    {
        PlayerMove.playerHp += 5;

        if (PlayerMove.playerHp > player.GetComponent<PlayerMove>().maxHp)
        {
            PlayerMove.playerHp = player.GetComponent<PlayerMove>().maxHp;
        }

        Debug.Log("hp 회복");
    }
}