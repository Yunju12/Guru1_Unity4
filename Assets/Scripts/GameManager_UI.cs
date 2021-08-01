using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_UI : MonoBehaviour
{
    //옵션 메뉴 UI 오브젝트
    public GameObject optionUI;

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

    //스테이지 게임 나가기(성공)
    public void Clear()
    {
        //씬 전환
        SceneManager.LoadScene("BoardGame");
    }

    //현재 게임 재시작(옵션)
    public void Restart()
    {
        //시간을 원래대로 돌린다
        Time.timeScale = 1.0f;

        //옵션 메뉴 창을 끈다
        optionUI.SetActive(false);

        //알 하나 차감
        EggHp.eggHp--;

        //현재 씬 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //게임 시작 전 공지
    public void GoNotice1()
    {
        //씬 전환
        SceneManager.LoadScene("Notice1");
    }

    public void GoNotice2()
    {
        //씬 전환
        SceneManager.LoadScene("Notice2");
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
