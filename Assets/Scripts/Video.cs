using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Video : MonoBehaviour
{
    public GameObject restartPanel;
    public GameObject skipButton;

    public RawImage mScreen = null;
    public VideoPlayer mVideoPlayer = null;

    void Start()
    {
        if (mScreen != null && mVideoPlayer != null)
        {
            // 비디오 준비 코루틴 호출
            StartCoroutine(PrepareVideo());
        }
    }

    protected IEnumerator PrepareVideo()
    {
        // 비디오 준비
        mVideoPlayer.Prepare();

        // 비디오가 준비되는 것을 기다림
        while (!mVideoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(0.5f);
        }

        // VideoPlayer의 출력 texture를 RawImage의 texture로 설정한다
        mScreen.texture = mVideoPlayer.texture;

        if (mVideoPlayer.isPlaying == false)
        {
            yield return null;
            GameStart();
        }
    }

    //public void PlayVideo()
    //{
    //if (mVideoPlayer != null && mVideoPlayer.isPrepared)
    //{
    // 비디오 재생
    //mVideoPlayer.Play();
    //}
    //}

    //첫 게임 시작
    public void GameStart()
    {
        if (mVideoPlayer != null && mVideoPlayer.isPrepared)
        {
            EggHp.eggHp = 5;
            EggHp.potionCount = 0;
            EggHp.bombCount = 0;
            Board_PlayerMove.totalScore = 0;

            Board_PlayerMove.posx = 0;
            SceneManager.LoadScene("Start");
        }
    }

    //전체 게임 재시작
    public void GameReStart()
    {
        if (mVideoPlayer != null && mVideoPlayer.isPrepared)
        {
            mVideoPlayer.Stop();
            skipButton.SetActive(false);
            restartPanel.SetActive(true);
        }
    }
}
