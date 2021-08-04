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
            // ���� �غ� �ڷ�ƾ ȣ��
            StartCoroutine(PrepareVideo());
        }
    }

    protected IEnumerator PrepareVideo()
    {
        // ���� �غ�
        mVideoPlayer.Prepare();

        // ������ �غ�Ǵ� ���� ��ٸ�
        while (!mVideoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(0.5f);
        }

        // VideoPlayer�� ��� texture�� RawImage�� texture�� �����Ѵ�
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
    // ���� ���
    //mVideoPlayer.Play();
    //}
    //}

    //ù ���� ����
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

    //��ü ���� �����
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
