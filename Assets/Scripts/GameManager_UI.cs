using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_UI : MonoBehaviour
{
    //옵션 메뉴 UI 오브젝트
    public GameObject optionUI;

    //게임 시작 UI 오브젝트
    public GameObject startUI;

    //클리어 실패 옵션 UI 오브젝트
    public GameObject reOptionUI;

    //목숨(계란)
    //public static int eggHp = 5;

    /*private void Awake()
    {
        if(gmU == null)
        {
            gmU = this;
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        //플레이어 오브젝트를 검색
        //player = GameObject.Find("Player");

        //playerM = Player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        //만일 플레이어의 hp가 0 이하로 떨어진다면
        /*if(PlayerM.hp <= 0)
        {
            //게임 오버 문구를 출력한다
            stateLabel.text = "Lose";

            //게임 오버 문구의 색상은 붉은색으로 설정한다
            stateLabel.color = new Color32(255, 0, 0, 255);

            //게임 상태를 게임 오버 상태로 전환한다
            gState = GameState.Lose;
        }*/
    }

    //옵션 메뉴 켜기
    public void OpenOptionWindow()
    {
        //시간을 멈춘다
        Time.timeScale = 0;

        //옵션 메뉴 창을 활성화한다
        optionUI.SetActive(true);
    }

    //옵션 메뉴 끄기
    public void CloseOptionWindow()
    {
        //시간을 원래대로 돌린다
        Time.timeScale = 1.0f;

        //옵션 메뉴 창을 끈다
        optionUI.SetActive(false);
    }

    //스테이지 게임 나가기
    public void Lose()
    {
        //알 하나 차감
        EggHp.eggHp--;

        //씬 전환
        SceneManager.LoadScene("BoardGame");
    }

    //현재 게임 재시작(옵션)
    public void Restart()
    {
        //알 하나 차감
        EggHp.eggHp--;

        //현재 씬 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //전체 게임 시작
    public void StartGame()
    {
        //씬 전환
        SceneManager.LoadScene("BoardGame");
    }

    //전체 게임 종료
    public void Quit()
    {
        //게임 종료
        Application.Quit();
    }

    //현재 게임 재시작(실패)
    public void RestartGame()
    {
        //알 하나 차감
        EggHp.eggHp--;

        //현재 씬 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //전체 게임 재시작
    public void GameReStart()
    {
        //씬 전환
        SceneManager.LoadScene("Prologue");
    }
}
