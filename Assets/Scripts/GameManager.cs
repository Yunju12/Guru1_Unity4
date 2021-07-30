using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ���� ��ü ����
    public int totalPoint = 0;

    public int bossPoint = 500;

    public Text UIPoint;

    // ���� ���� ���
    public enum GameState
    {
        Ready,
        Run,
        GameOver,
        GameClear
    }

    // ���� ���� ����
    public GameState gState;

    // UI �ؽ�Ʈ ����
    public Text stateLabel;

    // �÷��̾� ���� ������Ʈ ����
    PlayerMove pm;

    // ���� ���� ������Ʈ ����
    BossMonster bm;

    // �̱���
    public static GameManager gm;

    // �ִϸ��̼� ����
    Animator ani;

    // ����� �ҽ� ������Ʈ
    AudioSource audio_gm;

    // ����� Ŭ�� ����
    public AudioClip ready;

    // ����� Ŭ�� ����
    public AudioClip start;

    public AudioClip enemyBGM;

    public AudioClip bossBGM;

    public AudioClip gameClear;

    public AudioClip gameOver;

    public GameObject player;

    //���� ��� UI
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
        // �ʱ� ���� ���´� �غ� ���·� �����Ѵ�.
        gState = GameState.Ready;

        // �÷��̾� �ִϸ��̼� ������Ʈ�� �޾ƿ´�.
        ani = GetComponent<Animator>();

        pm = GetComponent<PlayerMove>();

        //�������� ������Ʈ�� �˻�
        bm = GetComponent<BossMonster>();

        // ���� ���� �ڷ�ƾ �Լ��� �����Ѵ�.
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        // Ready...  ��� ������ ǥ���Ѵ�.
        stateLabel.text = "Ready...";
        audio_gm.PlayOneShot(ready);

        // Ready ������ ������ ��Ȳ������ ǥ���Ѵ�.
        stateLabel.color = new Color32(233, 182, 12, 255);

        // 2�ʰ� ����Ѵ�.
        yield return new WaitForSeconds(2.0f);

        // Start! ��� ������ �����Ѵ�.
        stateLabel.text = "Start!";
        audio_gm.PlayOneShot(start);

        // 0.5�ʰ� ����Ѵ�.
        yield return new WaitForSeconds(0.5f);

        // Start ������ �����.
        stateLabel.text = "";

        // ������ ���¸� �غ� ���¿��� ���� ���·� ��ȯ�Ѵ�.
        gState = GameState.Run;
        audio_gm.clip = enemyBGM;
        audio_gm.Play();

        totalPoint = 0;
    }

    void Update()
    {
        UIPoint.text = (totalPoint).ToString();

        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ��� �ߴ�
        if (gm.gState != GameState.Run)
        {
            return;
        }

        // ���� �÷��̾��� hp�� 0 ���Ϸ� ��������
        if (PlayerMove.playerHp <= 0)
        {
            //audio.Stop();
            //audio.PlayOneShot(gameOver);

            player.GetComponent<PlayerMove>().Die();

            //���� ���� �ɼ� �޴� â�� Ȱ��ȭ�Ѵ�
            GameOverUI.SetActive(true);

            // ���� ���¸� ���� ���� ���·� ��ȯ�Ѵ�.
            gState = GameState.GameOver;
        }

        // ���� ������ hp�� 0 ���Ϸ� ��������
        else if (BossMonster.bossHp <= 0)
        {
            //audio.Stop();
            //audio.PlayOneShot(gameClear);

            totalPoint += bossPoint;

            //Ŭ���� �ɼ� �޴� â�� Ȱ��ȭ�Ѵ�
            ClearUI.SetActive(true);

            // ���� ���¸� ���� Ŭ���� ���·� ��ȯ�Ѵ�.
            gState = GameState.GameClear;
        }
    }

    
}