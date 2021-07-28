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

    // 최대 체력 변수
    public int maxHp = 10;

    // platplayer HP
    public int Hp = 10;

    // 슬라이더 바
    public Slider hpSlider;

    // player 변수
    public PlatPlayerMove PlatPlayer;

    //스테이지 1,2 배열
    public GameObject[] Stages;

    // UI : 현재 맵, 득점 포인트 출력
    public Text UIPoint;
    public Text UIMAP;


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

    // 애니메이션 변수
    Animator anim;


    // 오디오 소스 컴포넌트
    AudioSource audioSource;

    // 오디오 클립 변수
    public AudioClip ready;
    public AudioClip start;
    public AudioClip gameClear;
    public AudioClip gameOver;

    // UI 텍스트 변수
    public Text stateLabel;

    void Awake()
    {      
        audioSource = GetComponent<AudioSource>();
              
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        // 체력 변수 초기화
        Hp = maxHp;

        // 초기 게임 상태는 준비 상태로 설정한다.
        gState = GameState.Ready;

        // 게임 시작 코루틴 함수를 실행한다.
        StartCoroutine(GameStart());
    }
    IEnumerator GameStart()
    {
        // Ready...  라는 문구를 표시한다.
        stateLabel.text = "Ready...";        
        audioSource.clip = ready;
        audioSource.Play();

        // Ready 문구의 색상을 주황색으로 표시한다.
        stateLabel.color = new Color32(233, 182, 12, 255);

        // 2초간 대기한다.
        yield return new WaitForSeconds(2.0f);

        // Start! 라는 문구로 변경한다.
        stateLabel.text = "Start!";
        audioSource.clip = start;
        audioSource.Play();

        // 0.5초간 대기한다.
        yield return new WaitForSeconds(0.5f);

        // Start 문구를 지운다.
        stateLabel.text = "";

        // 게임의 상태를 준비 상태에서 실행 상태로 전환한다.
        gState = GameState.Run;
    }


    void Update()
    {  
        // 슬라이더의 value를 체력 비율로 적용한다.
        hpSlider.value = (float)Hp / (float)maxHp;

        // 토탈 포인트 화면 표시
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }

    // 새 스테이지
    public void NextStage()
    {
        // 스테이지(Plat안의 맵 구성) 변경 : stageIndex에 따라 스테이지 활성화 여부 결정
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++; // 스테이지 인덱스 늘림
            Stages[stageIndex].SetActive(true);
            PlatPlayerReposition(); // player 위치 시작점으로 이동

            UIMAP.text = "MAP" + (stageIndex + 1);
        }

        else  // 스테이지 플랫폼 끝날 경우 (클리어)
        {
            Time.timeScale = 0; // 완주시 시간 멈춤
            Debug.Log("스테이지 플랫폼 클리어");

            // 성공 문구를 풀력한다.
            stateLabel.text = "Clear!";

            // 클리어 문구의 색상은 노란색으로 설정한다.
            stateLabel.color = new Color32(255, 255, 0, 255);

            // 게임 상태를 게임 클리어 상태로 전환한다.
            gState = GameState.GameClear;
        }

        // 전체 점수에 현 스테이지에서 얻음 점수 추가
        totalPoint += stagePoint;
        stagePoint = 0; // 새로운 스테이지에서의 점수 0으로 초기화
    }

    // Hp 감소의 경우
    public void HpDown()
    {
        // Hp 1이상일 경우, Hp -1시킴
        if (Hp >= 1)
            Hp--;

        // Hp 0일 경우
        else if(Hp == 0)
        {            
            // Player Die 호출
            PlatPlayer.OnDie();

            // 결과 로그 출력
            Debug.Log("죽었습니다!");

            // 게임 오버 문구를 출력한다.
            stateLabel.text = "Game Over...";

            // 게임 오버 문구의 색상은 붉은색으로 설정한다.
            stateLabel.color = new Color32(255, 0, 0, 255);

            // 게임 상태를 게임 오버 상태로 전환한다.
            gState = GameState.GameOver;
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
                PlatPlayerReposition();
            }

            // 체력 1남았는데 떨어질 경우 Hp 1감소 후 원위치 되돌리기 실행하지 않음
            HpDown();
        }
    }

    // 낙하시 player 위치 변경
    void PlatPlayerReposition()
    {
        PlatPlayer.transform.position = new Vector3(0, 0, 0); // (0,0,0) 위치로 이동
        PlatPlayer.VelocityZero(); // 낙하속도 0으로
    }

}
