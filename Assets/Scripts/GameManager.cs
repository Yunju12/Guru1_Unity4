using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    // ���� ������ bool ����
    public bool isDelay;

    // ������ �ð� ����
    public float delayTime = 3.0f;

    // ���� �ð� ����
    public float currentTime;

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
            if (EggHp.eggHp <= 0)
            {
                //�� ��ȯ
                SceneManager.LoadScene("BadEnding_2");
            }
            else
            {
                //audio.Stop();
                //audio.PlayOneShot(gameOver);

                player.GetComponent<PlayerMove>().Die();

                Enemy.enemyDeath = 0;

                //���� ���� �ɼ� �޴� â�� Ȱ��ȭ�Ѵ�
                GameOverUI.SetActive(true);

                // ���� ���¸� ���� ���� ���·� ��ȯ�Ѵ�.
                gState = GameState.GameOver;
            }
        }

        // ���� ������ hp�� 0 ���Ϸ� ��������
        else if (BossMonster.bossHp <= 0)
        {
            totalPoint += bossPoint;
            Board_PlayerMove.totalScore += totalPoint;

            Enemy.enemyDeath = 0;

            //Ŭ���� �ɼ� �޴� â�� Ȱ��ȭ�Ѵ�
            ClearUI.SetActive(true);

            // ���� ���¸� ���� Ŭ���� ���·� ��ȯ�Ѵ�.
            gState = GameState.GameClear;
        }

        // ���� ���� ������ bool ������ ���̸�,
        if (isDelay)
        {
            // ���� �ð� ������ ���� 1�ʰ� �带 ������ 1 ��ŭ �����Ѵ�.
            currentTime += Time.deltaTime;

            // ���� ���� �ð� ������ ������ �ð� ������ ���ų� �׺��� ũ�ٸ�,
            if (currentTime >= delayTime)
            {
                // ���� �ð� ������ ���� 0 ���� �ϰ� ���� ������ bool ������ ���� �������� �Ѵ�.
                currentTime = 0;
                isDelay = false;
            }
        }

        // ���� A Ű�� ������,
        if (Input.GetKeyDown(KeyCode.A))
        {
            // ���� �������� ���� ���� ���,
            if (EggHp.potionCount > 0)
            {
                // ���� ������ bool ������ �����̰ų� �÷��̾��� ü���� 10 ���� ���,
                if (isDelay == false && PlayerMove.playerHp < 10)
                {
                    // isDelay�� ���� ������ ��ȯ�ϰ�, ���� �������� �ϳ� �Ҹ��ϰ�, ü���� ȸ���Ѵ�.
                    isDelay = true;
                    EggHp.potionCount--;
                    Heal();
                }
                // �׷��� �ʴٸ�(���� ������ �Լ��� ���̰ų� �÷��̾��� ü���� 10 �̶��),
                else
                {
                    // ���� ��ȯ�Ѵ�.
                    return;
                }
            }
        }
    }

    // ü�� ȸ�� �Լ�
    void Heal()
    {
        // �÷��̾��� ü���� 5��ŭ ȸ���Ѵ�.
        PlayerMove.playerHp += 5;

        // ���� �÷��̾��� ü���� maxHp �� �ʰ��ϸ�,
        if (PlayerMove.playerHp > player.GetComponent<PlayerMove>().maxHp)
        {
            // �÷��̾��� ü���� maxHp ��ŭ���� �ٲ۴�.
            PlayerMove.playerHp = player.GetComponent<PlayerMove>().maxHp;
        }
    }
}